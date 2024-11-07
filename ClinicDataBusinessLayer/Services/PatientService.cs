using AutoMapper;
using ClinicDataAccessLayer.Data;
using ClinicDataAccessLayer.Entities;
using ClinicDataAccessLayer.Entities.Contracts;
using ClinicDataAccessLayer.Extensions;
using ClinicDataBusinessLayer.DTOs.Patient.Contracts;
using ClinicDataBusinessLayer.Extensions;
using ClinicDataBusinessLayer.Services.Contracts;
using ClinicDataBusinessLayer.Services.ServiceResults;
using ClinicDataBusinessLayer.Services.ServiceResults.Extensions;
using ClinicDataBusinessLayer.Services.ServiceResults.Factories;
using ClinicDataBusinessLayer.Services.ServiceResults.Messages.ServiceMessages;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ClinicDataBusinessLayer.Services
{
    public class PatientService : ServiceBase, IPatientService, IScopedService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public PatientService(AppDbContext context,
               ServiceResultHandlerFactory serviceResultHandlerFactory,
               IMapper mapper,
               ILogger<PatientService> logger) : base(serviceResultHandlerFactory, mapper, logger)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<IServiceResult<IEnumerable<TDtoResult>>> GetAll<TDtoResult>()
                    where TDtoResult : class, IPatientDto
        {
            return await ExecuteOperationAsync(async serviceResult =>
            {
                var patients = await _context.Patients
                                            .NotDeleted()
                                            .OrderBy(p => p.Id)
                                            .ToDtosAsync<TDtoResult>(_mapper.ConfigurationProvider);

                return serviceResult.Success(patients);

            }, nameof(GetAll));
        }
        public async Task<IServiceResult<TDtoResult>> GetById<TDtoResult>(int Id)
            where TDtoResult : class, IPatientDto
        {
            return await ExecuteOperationAsync<TDtoResult, PatientServiceMessages>(async serviceResult =>
            {
                var patient = await _context.Patients
                                           .NotDeleted()
                                           .Where(p => p.Id == Id)
                                           .ToDtoAsync<TDtoResult>(_mapper.ConfigurationProvider);

                if (patient == null)
                    return serviceResult.NotFound<TDtoResult>(Id);

                return serviceResult.Success(patient);

            }, nameof(GetById));
        }
        public async Task<IServiceResult<TDtoResult>> Add<TDtoResult, TDtoAdd>(TDtoAdd dtoAdd)
            where TDtoResult : class, IPatientDto
            where TDtoAdd : class, IPatientDtoAdd
        {
            return await ExecuteOperationAsync(async serviceResult =>
            {
                var patient = (await _context.AddAsync(_mapper.Map<Patient>(dtoAdd))).Entity;

                await _context.SaveChangesAsync();

                return AreDtoToEntryPathsCompatible<Patient, TDtoAdd, TDtoResult>() ?
                        serviceResult.Success(_mapper.Map<TDtoResult>(patient)) :
                        await GetById<TDtoResult>(patient.Id);

            }, nameof(Add));
        }
        public async Task<IServiceResult<TDtoResult>> Update<TDtoResult, TDtoUpdate>(TDtoUpdate dtoUpdae)
             where TDtoResult : class, IPatientDto
             where TDtoUpdate : class, IPatientDtoUpdate
        {
            return await ExecuteOperationAsync<TDtoResult, PatientServiceMessages>(async serviceResult =>
            {
                var patient = await _context.Patients
                                           .NotDeleted()
                                           .ApplyDtoToEntryIncludes(typeof(TDtoUpdate), _mapper.ConfigurationProvider)
                                           .FirstOrDefaultAsync(p => p.Id == dtoUpdae.Id);

                if (patient == null)
                    return serviceResult.NotFound<TDtoResult>(dtoUpdae.Id);

                _mapper.Map(dtoUpdae, patient);

                await _context.SaveChangesAsync();

                return AreDtoToEntryPathsCompatible<Patient, TDtoUpdate, TDtoResult>() ?
                        serviceResult.Success(_mapper.Map<TDtoResult>(patient)) :
                        await GetById<TDtoResult>(patient.Id);

            }, nameof(Update));
        }

        public async Task<IServiceResult> Delete(int Id)
        {
            return await ExecuteOperationAsync<PatientServiceMessages>(async serviceResult =>
            {
                var patient = await _context.Patients
                                            .NotDeleted()
                                            .FirstOrDefaultAsync(p => p.Id == Id);

                if (patient == null)
                    return serviceResult.NotFound(Id);

                _context.Remove(patient);

                await _context.SaveChangesAsync();

                return serviceResult.Success();

            }, nameof(Delete));
        }
        public async Task<IServiceResult> UndoDelete(int Id)
        {
            return await ExecuteOperationAsync<PatientServiceMessages>(async serviceResult =>
            {
                var patient = await _context.Patients
                                            .FirstOrDefaultAsync(p => p.Id == Id) as ISoftDeleteable;

                if (patient == null)
                    return serviceResult.NotFound(Id);

                if (!patient.IsDeleted)
                    return serviceResult.NotDeleted(Id);

                patient.UndoDelete();

                await _context.SaveChangesAsync();

                return serviceResult.Success();

            }, nameof(Delete));
        }


    }
}
