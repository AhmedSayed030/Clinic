using AutoMapper;
using ClinicDataAccessLayer.Data;
using ClinicDataAccessLayer.Entities;
using ClinicDataBusinessLayer.DTOs.MedicalRecord.Contracts;
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
    public class MedicalRecordService : ServiceBase, IMedicalRecordService, IScopedService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public MedicalRecordService(AppDbContext context,
               ServiceResultHandlerFactory serviceResultHandlerFactory,
               IMapper mapper,
               ILogger<MedicalRecordService> logger) : base( serviceResultHandlerFactory, mapper, logger)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<IServiceResult<IEnumerable<TDtoResult>>> GetAll<TDtoResult>()
                    where TDtoResult : class, IMedicalRecordDto
        {
            return await ExecuteOperationAsync(async serviceResult =>
            {
                var medicalRecords = await _context.MedicalRecords
                                            .OrderBy(m => m.Id)
                                            .ToDtosAsync<TDtoResult>(_mapper.ConfigurationProvider);

                return serviceResult.Success(medicalRecords);

            }, nameof(GetAll));
        }
        public async Task<IServiceResult<TDtoResult>> GetById<TDtoResult>(int Id)
            where TDtoResult : class, IMedicalRecordDto
        {

            return await ExecuteOperationAsync<TDtoResult, MedicalRecordServiceMessages>(async serviceResult =>
            {
                var medicalRecord = await _context.MedicalRecords
                                           .Where(m => m.Id == Id)
                                           .ToDtoAsync<TDtoResult>(_mapper.ConfigurationProvider);

                if (medicalRecord == null)
                    return serviceResult.NotFound<TDtoResult>(Id);

                return serviceResult.Success(medicalRecord);

            }, nameof(GetById));
        }
        public async Task<IServiceResult<TDtoResult>> Add<TDtoResult, TDtoAdd>(TDtoAdd dtoAdd)
            where TDtoResult : class, IMedicalRecordDto
            where TDtoAdd : class, IMedicalRecordDtoAdd
        {
            return await ExecuteOperationAsync(async serviceResult =>
            {
                var medicalRecord = (await _context.AddAsync(_mapper.Map<MedicalRecord>(dtoAdd))).Entity;

                await _context.SaveChangesAsync();

                return AreDtoToEntryPathsCompatible<MedicalRecord, TDtoAdd, TDtoResult>() ?
                        serviceResult.Success(_mapper.Map<TDtoResult>(medicalRecord)) :
                        await GetById<TDtoResult>(medicalRecord.Id);

            }, nameof(Add));
        }
        public async Task<IServiceResult<TDtoResult>> Update<TDtoResult, TDtoUpdate>(TDtoUpdate dtoUpdae)
             where TDtoResult : class, IMedicalRecordDto
             where TDtoUpdate : class, IMedicalRecordDtoUpdate
        {
            return await ExecuteOperationAsync<TDtoResult, MedicalRecordServiceMessages>(async serviceResult =>
            {
                var medicalRecord = await _context.MedicalRecords
                                           .ApplyDtoToEntryIncludes(typeof(TDtoUpdate), _mapper.ConfigurationProvider)
                                           .FirstOrDefaultAsync(m => m.Id == dtoUpdae.Id);

                if (medicalRecord == null)
                    return serviceResult.NotFound<TDtoResult>(dtoUpdae.Id);

                _mapper.Map(dtoUpdae, medicalRecord);

                await _context.SaveChangesAsync();

                return AreDtoToEntryPathsCompatible<MedicalRecord, TDtoUpdate, TDtoResult>() ?
                        serviceResult.Success(_mapper.Map<TDtoResult>(medicalRecord)) :
                        await GetById<TDtoResult>(medicalRecord.Id);

            }, nameof(Update));
        }

        public async Task<IServiceResult> Delete(int Id)
        {
            return await ExecuteOperationAsync<MedicalRecordServiceMessages>(async serviceResult =>
            {
                var medicalRecord = await _context.MedicalRecords.FirstOrDefaultAsync(m => m.Id == Id);

                if (medicalRecord == null)
                    return serviceResult.NotFound(Id);

                _context.Remove(medicalRecord);

                await _context.SaveChangesAsync();

                return serviceResult.Success();

            }, nameof(Delete));
        }

    }
}
