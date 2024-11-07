using AutoMapper;
using ClinicDataAccessLayer.Data;
using ClinicDataAccessLayer.Entities;
using ClinicDataBusinessLayer.DTOs.Prescription.Contracts;
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
    public class PrescriptionService : ServiceBase, IPrescriptionService, IScopedService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public PrescriptionService(AppDbContext context,
               ServiceResultHandlerFactory serviceResultHandlerFactory,
               IMapper mapper,
               ILogger<PrescriptionService> logger) : base(serviceResultHandlerFactory, mapper, logger)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<IServiceResult<IEnumerable<TDtoResult>>> GetAll<TDtoResult>()
            where TDtoResult : class, IPrescriptionDto
        {
            return await ExecuteOperationAsync(async serviceResult =>
            {
                var prescriptions = await _context.Prescriptions
                                            .OrderBy(p => p.Id)
                                            .ToDtosAsync<TDtoResult>(_mapper.ConfigurationProvider);

                return serviceResult.Success(prescriptions);

            }, nameof(GetAll));
        }
        public async Task<IServiceResult<TDtoResult>> GetById<TDtoResult>(int Id)
            where TDtoResult : class, IPrescriptionDto
        {

            return await ExecuteOperationAsync<TDtoResult, PrescriptionServiceMessages>(async serviceResult =>
            {
                var prescription = await _context.Prescriptions
                                           .Where(p => p.Id == Id)
                                           .ToDtoAsync<TDtoResult>(_mapper.ConfigurationProvider);

                if (prescription == null)
                    return serviceResult.NotFound<TDtoResult>(Id);

                return serviceResult.Success(prescription);

            }, nameof(GetById));
        }
        public async Task<IServiceResult<TDtoResult>> Add<TDtoResult, TDtoAdd>(TDtoAdd dtoAdd)
            where TDtoResult : class, IPrescriptionDto
            where TDtoAdd : class, IPrescriptionDtoAdd
        {
            return await ExecuteOperationAsync(async serviceResult =>
            {
                var prescription = (await _context.AddAsync(_mapper.Map<Prescription>(dtoAdd))).Entity;

                await _context.SaveChangesAsync();

                return AreDtoToEntryPathsCompatible<Prescription, TDtoAdd, TDtoResult>() ?
                        serviceResult.Success(_mapper.Map<TDtoResult>(prescription)) :
                        await GetById<TDtoResult>(prescription.Id);

            }, nameof(Add));
        }
        public async Task<IServiceResult<TDtoResult>> Update<TDtoResult, TDtoUpdate>(TDtoUpdate dtoUpdae)
             where TDtoResult : class, IPrescriptionDto
             where TDtoUpdate : class, IPrescriptionDtoUpdate
        {
            return await ExecuteOperationAsync<TDtoResult, PrescriptionServiceMessages>(async serviceResult =>
            {
                var prescription = await _context.Prescriptions
                                           .ApplyDtoToEntryIncludes(typeof(TDtoUpdate), _mapper.ConfigurationProvider)
                                           .FirstOrDefaultAsync(p => p.Id == dtoUpdae.Id);

                if (prescription == null)
                    return serviceResult.NotFound<TDtoResult>(dtoUpdae.Id);

                _mapper.Map(dtoUpdae, prescription);

                await _context.SaveChangesAsync();

                return AreDtoToEntryPathsCompatible<Prescription, TDtoUpdate, TDtoResult>() ?
                        serviceResult.Success(_mapper.Map<TDtoResult>(prescription)) :
                        await GetById<TDtoResult>(prescription.Id);

            }, nameof(Update));
        }

        public async Task<IServiceResult> Delete(int Id)
        {
            return await ExecuteOperationAsync<PrescriptionServiceMessages>(async serviceResult =>
            {
                var prescription = await _context.Prescriptions.FirstOrDefaultAsync(d => d.Id == Id);

                if (prescription == null)
                    return serviceResult.NotFound(Id);

                _context.Remove(prescription);

                await _context.SaveChangesAsync();

                return serviceResult.Success();

            }, nameof(Delete));
        }
    }
}
