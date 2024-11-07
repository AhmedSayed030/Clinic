using AutoMapper;
using ClinicDataAccessLayer.Data;
using ClinicDataAccessLayer.Entities;
using ClinicDataBusinessLayer.DTOs.Medication.Contracts;
using ClinicDataBusinessLayer.Extensions;
using ClinicDataBusinessLayer.Services.Contracts;
using ClinicDataBusinessLayer.Services.ServiceResults;
using ClinicDataBusinessLayer.Services.ServiceResults.Factories;
using ClinicDataBusinessLayer.Services.ServiceResults.Messages.ServiceMessages;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ClinicDataBusinessLayer.Services
{
    public class MedicationService : ServiceBase, IMedicationService, IScopedService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public MedicationService(AppDbContext context,
               ServiceResultHandlerFactory serviceResultHandlerFactory,
               IMapper mapper,
               ILogger<MedicationService> logger) : base( serviceResultHandlerFactory, mapper, logger)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<IServiceResult<IEnumerable<TDtoResult>>> GetAll<TDtoResult>()
                    where TDtoResult : class, IMedicationDto
        {
            return await ExecuteOperationAsync(async serviceResult =>
            {
                var medications = await _context.Medications
                                            .OrderBy(m => m.Id)
                                            .ToDtosAsync<TDtoResult>(_mapper.ConfigurationProvider);

                return serviceResult.Success(medications);

            }, nameof(GetAll));
        }
        public async Task<IServiceResult<TDtoResult>> GetById<TDtoResult>(int Id)
            where TDtoResult : class, IMedicationDto
        {

            return await ExecuteOperationAsync<TDtoResult, MedicationServiceMessages>(async serviceResult =>
            {
                var medication = await _context.Medications
                                           .Where(m => m.Id == Id)
                                           .ToDtoAsync<TDtoResult>(_mapper.ConfigurationProvider);

                if (medication == null)
                    return serviceResult.NotFound<TDtoResult>(Id);

                return serviceResult.Success(medication);

            }, nameof(GetById));
        }
        public async Task<IServiceResult<TDtoResult>> Add<TDtoResult, TDtoAdd>(TDtoAdd dtoAdd)
            where TDtoResult : class, IMedicationDto
            where TDtoAdd : class, IMedicationDtoAdd
        {
            return await ExecuteOperationAsync( async serviceResult =>
            {
                var medication = (await _context.AddAsync(_mapper.Map<Medication>(dtoAdd))).Entity;

                await _context.SaveChangesAsync();

                return AreDtoToEntryPathsCompatible<Medication, TDtoAdd, TDtoResult>() ?
                        serviceResult.Success(_mapper.Map<TDtoResult>(medication)) :
                        await GetById<TDtoResult>(medication.Id);

            }, nameof(Add));
        }
        public async Task<IServiceResult<TDtoResult>> Update<TDtoResult, TDtoUpdate>(TDtoUpdate dtoUpdae)
             where TDtoResult : class, IMedicationDto
             where TDtoUpdate : class, IMedicationDtoUpdate
        {
            return await ExecuteOperationAsync<TDtoResult, MedicationServiceMessages>(async serviceResult =>
            {
                var medication = await _context.Medications
                                           .ApplyDtoToEntryIncludes(typeof(TDtoUpdate), _mapper.ConfigurationProvider)
                                           .FirstOrDefaultAsync(a => a.Id == dtoUpdae.Id);

                if (medication == null)
                    return serviceResult.NotFound<TDtoResult>(dtoUpdae.Id);

                _mapper.Map(dtoUpdae, medication);

                await _context.SaveChangesAsync();

                return AreDtoToEntryPathsCompatible<Medication, TDtoUpdate, TDtoResult>() ?
                        serviceResult.Success(_mapper.Map<TDtoResult>(medication)) :
                        await GetById<TDtoResult>(medication.Id);

            }, nameof(Update));
        }

        public async Task<IServiceResult> Delete(int Id)
        {
            return await ExecuteOperationAsync<MedicationServiceMessages>(async serviceResult =>
            {
                var medication = await _context.Medications.FirstOrDefaultAsync(d => d.Id == Id);

                if (medication == null)
                    return serviceResult.NotFound(Id);

                _context.Remove(medication);

                await _context.SaveChangesAsync();

                return serviceResult.Success();

            }, nameof(Delete));
        }


    }
}
