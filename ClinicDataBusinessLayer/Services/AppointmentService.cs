using AutoMapper;
using ClinicDataAccessLayer.Data;
using ClinicDataAccessLayer.Entities;
using ClinicDataAccessLayer.Extensions;
using ClinicDataBusinessLayer.DTOs.Appointment.Contracts;
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
    public class AppointmentService : ServiceBase, IAppointmentService, IScopedService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public AppointmentService(AppDbContext context,
               ServiceResultHandlerFactory serviceResultHandlerFactory, 
               IMapper mapper,
               ILogger<AppointmentService> logger) : base(serviceResultHandlerFactory, mapper, logger)
        {
            _context = context;
            _mapper = mapper;

        }

        public async Task<IServiceResult<IEnumerable<TDtoResult>>> GetAll<TDtoResult>()
            where TDtoResult : class, IAppointmentDto
        {
            return await ExecuteOperationAsync(async serviceResult =>
            {
                var appointments = await _context.Appointments
                                            .OrderBy(a => a.Id)
                                            .ToDtosAsync<TDtoResult>(_mapper.ConfigurationProvider);

               return serviceResult.Success(appointments);

            }, nameof(GetAll));
        }
        public async Task<IServiceResult<IEnumerable<TDtoResult>>> GetByPatientId<TDtoResult>(int Id)
            where TDtoResult : class, IAppointmentDto
        {

            return await ExecuteOperationAsync<IEnumerable<TDtoResult>, PatientServiceMessages>(async serviceResult =>
            {
                if (await _context.Patients.IsNotExest(Id))
                    return serviceResult.NotFound<IEnumerable<TDtoResult>>(Id);

                var appointment = await _context.Appointments
                                           .OrderBy(a => a.Id)
                                           .Where(a => a.PatientId == Id)
                                           .ToDtosAsync<TDtoResult>(_mapper.ConfigurationProvider);

                return serviceResult.Success(appointment);

            }, nameof(GetByPatientId));

        }
        public async Task<IServiceResult<IEnumerable<TDtoResult>>> GetByDoctorId<TDtoResult>(int Id)
             where TDtoResult : class, IAppointmentDto
        {
            return await ExecuteOperationAsync<IEnumerable<TDtoResult>, DoctorServiceMessages>(async serviceResult =>
            {
                if (await _context.Doctors.IsNotExest(Id))
                    return serviceResult.NotFound<IEnumerable<TDtoResult>>(Id);

                var appointments = await _context.Appointments
                                           .OrderBy(a => a.Id)
                                           .Where(a => a.DoctorId == Id)
                                           .ToDtosAsync<TDtoResult>(_mapper.ConfigurationProvider);

                return serviceResult.Success(appointments);

            }, nameof(GetByDoctorId));
        }


        public async Task<IServiceResult<TDtoResult>> GetById<TDtoResult>(int Id)
            where TDtoResult : class, IAppointmentDto
        {

            return await ExecuteOperationAsync<TDtoResult, AppointmentServiceMessages>(async serviceResult =>
            {
                var appointments = await _context.Appointments
                                           .Where(a => a.Id == Id)
                                           .ToDtoAsync<TDtoResult>(_mapper.ConfigurationProvider);

                if (appointments == null)
                    return serviceResult.NotFound<TDtoResult>(Id);

                return serviceResult.Success(appointments);

            }, nameof(GetById));
        }
        public async Task<IServiceResult<TDtoResult>> Add<TDtoResult, TDtoAdd>(TDtoAdd dtoAdd)
            where TDtoResult : class, IAppointmentDto
            where TDtoAdd : class, IAppointmentDtoAdd
        {
            return await ExecuteOperationAsync(async serviceResult =>
            {
                var appointment = (await _context.AddAsync(_mapper.Map<Appointment>(dtoAdd))).Entity;

                await _context.SaveChangesAsync();

                return AreDtoToEntryPathsCompatible<Appointment, TDtoAdd, TDtoResult>() ?
                        serviceResult.Success(_mapper.Map<TDtoResult>(appointment)) :
                        await GetById<TDtoResult>(appointment.Id);

            }, nameof(Add));
        }
        public async Task<IServiceResult<TDtoResult>> Update<TDtoResult, TDtoUpdate>(TDtoUpdate dtoUpdate)
             where TDtoResult : class, IAppointmentDto
             where TDtoUpdate : class, IAppointmentDtoUpdate
        {
            return await ExecuteOperationAsync<TDtoResult, AppointmentServiceMessages>(async serviceResult =>
            {
                var appointment = await _context.Appointments
                                                .ApplyDtoToEntryIncludes(typeof(TDtoUpdate), _mapper.ConfigurationProvider)
                                                .FirstOrDefaultAsync(a => a.Id == dtoUpdate.Id);

                if (appointment == null)
                    return serviceResult.NotFound<TDtoResult>(dtoUpdate.Id);

                _mapper.Map(dtoUpdate, appointment);

                await _context.SaveChangesAsync();

                return AreDtoToEntryPathsCompatible<Appointment, TDtoUpdate, TDtoResult>() ?
                        serviceResult.Success(_mapper.Map<TDtoResult>(appointment)) :
                        await GetById<TDtoResult>(appointment.Id);

            }, nameof(Update));
        }

        public async Task<IServiceResult> Delete(int Id)
        {
            return await ExecuteOperationAsync<AppointmentServiceMessages>(async serviceResult =>
            {
                var appointment = await _context.Appointments.FirstOrDefaultAsync(a => a.Id == Id);

                if (appointment == null)
                    return serviceResult.NotFound(Id);

                _context.Appointments.Remove(appointment);

                await _context.SaveChangesAsync();

                return serviceResult.Success();

            }, nameof(Delete));
        }

    }
}