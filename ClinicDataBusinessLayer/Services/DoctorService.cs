using AutoMapper;
using ClinicDataAccessLayer.Data;
using ClinicDataAccessLayer.Entities;
using ClinicDataAccessLayer.Entities.Contracts;
using ClinicDataAccessLayer.Extensions;
using ClinicDataBusinessLayer.DTOs.Doctor.Contracts;
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
    public class DoctorService : ServiceBase, IDoctorService, IScopedService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public DoctorService(AppDbContext context,
               ServiceResultHandlerFactory serviceResultHandlerFactory,
               IMapper mapper,
               ILogger<AppointmentService> logger) : base( serviceResultHandlerFactory, mapper, logger)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<IServiceResult<IEnumerable<TDtoResult>>> GetAll<TDtoResult>()
                    where TDtoResult : class, IDoctorDto
        {
            return await ExecuteOperationAsync(async serviceResult =>
            {
                var doctors = await _context.Doctors
                                            .NotDeleted()
                                            .OrderBy(d => d.Id)
                                            .ToDtosAsync<TDtoResult>(_mapper.ConfigurationProvider);

                return serviceResult.Success(doctors);

            }, nameof(GetAll));
        }
        public async Task<IServiceResult<TDtoResult>> GetById<TDtoResult>(int Id)
            where TDtoResult : class, IDoctorDto
        {

            return await ExecuteOperationAsync<TDtoResult, DoctorServiceMessages>(async serviceResult =>
            {
                var doctor = await _context.Doctors
                                           .NotDeleted()
                                           .Where(d => d.Id == Id)
                                           .ToDtoAsync<TDtoResult>(_mapper.ConfigurationProvider);

                if (doctor == null)
                    return serviceResult.NotFound<TDtoResult>(Id);

                return serviceResult.Success(doctor);

            }, nameof(GetById));
        }
        public async Task<IServiceResult<TDtoResult>> Add<TDtoResult, TDtoAdd>(TDtoAdd dtoAdd)
            where TDtoResult : class, IDoctorDto
            where TDtoAdd : class, IDoctorDtoAdd
        {
            return await ExecuteOperationAsync(async serviceResult =>
            {
                var doctor = (await _context.AddAsync(_mapper.Map<Doctor>(dtoAdd))).Entity;

                await _context.SaveChangesAsync();

                return AreDtoToEntryPathsCompatible<Doctor, TDtoAdd, TDtoResult>() ?
                        serviceResult.Success(_mapper.Map<TDtoResult>(doctor)) :
                        await GetById<TDtoResult>(doctor.Id);

            }, nameof(Add));
        }
        public async Task<IServiceResult<TDtoResult>> Update<TDtoResult, TDtoUpdate>(TDtoUpdate dtoUpdae)
             where TDtoResult : class, IDoctorDto
             where TDtoUpdate : class, IDoctorDtoUpdate
        {
            return await ExecuteOperationAsync<TDtoResult, DoctorServiceMessages>(async serviceResult =>
            {
                var doctor = await _context.Doctors
                                           .NotDeleted()
                                           .ApplyDtoToEntryIncludes(typeof(TDtoUpdate), _mapper.ConfigurationProvider)
                                           .FirstOrDefaultAsync(a => a.Id == dtoUpdae.Id);

                if (doctor == null)
                    return serviceResult.NotFound<TDtoResult>(dtoUpdae.Id);

                _mapper.Map(dtoUpdae, doctor);

                await _context.SaveChangesAsync();

                return AreDtoToEntryPathsCompatible<Doctor, TDtoUpdate, TDtoResult>() ?
                        serviceResult.Success(_mapper.Map<TDtoResult>(doctor)) :
                        await GetById<TDtoResult>(doctor.Id);

            }, nameof(Update));
        }

        public async Task<IServiceResult> Delete(int Id)
        {
            return await ExecuteOperationAsync<DoctorServiceMessages>(async serviceResult =>
            {
                var doctor = await _context.Doctors
                                           .NotDeleted()
                                           .FirstOrDefaultAsync(d => d.Id == Id);

                if (doctor == null)
                    return serviceResult.NotFound(Id);

                _context.Remove(doctor);

                await _context.SaveChangesAsync();

                return serviceResult.Success();

            }, nameof(Delete));
        }

        public async Task<IServiceResult> UndoDelete(int Id)
        {
            return await ExecuteOperationAsync<DoctorServiceMessages>(async serviceResult =>
            {
                var doctor = await _context.Doctors
                                           .FirstOrDefaultAsync(p => p.Id == Id) as ISoftDeleteable;

                if (doctor == null)
                    return serviceResult.NotFound(Id);

                if (!doctor.IsDeleted)
                    return serviceResult.NotDeleted(Id);

                doctor.UndoDelete();

                await _context.SaveChangesAsync();

                return serviceResult.Success();

            }, nameof(Delete));
        }
    }
}
