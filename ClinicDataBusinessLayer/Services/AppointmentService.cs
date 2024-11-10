namespace ClinicDataBusinessLayer.Services;

public class AppointmentService : ServiceBase, IAppointmentService, IScopedService
{
    private readonly IMapper _mapper;
    private readonly AppDbContext _context;

    public AppointmentService(AppDbContext context,
        ServiceResultHandlerFactory serviceResultHandlerFactory,
        IMapper mapper,
        ServerErrorMessages serverErrorMessages,
        ILogger<AppointmentService> logger) : base(serviceResultHandlerFactory, mapper, logger, serverErrorMessages)
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
                .ToDtoListAsync<TDtoResult>(_mapper.ConfigurationProvider);

            return serviceResult.Success(appointments);

        }, nameof(GetAll));
    }
    public async Task<IServiceResult<IEnumerable<TDtoResult>>> GetByPatientId<TDtoResult>(int id)
        where TDtoResult : class, IAppointmentDto
    {

        return await ExecuteOperationAsync<IEnumerable<TDtoResult>, PatientServiceErrorMessages>(async serviceResult =>
        {
            if (await _context.Patients.IsNotExist(id))
                return serviceResult.NotFound<IEnumerable<TDtoResult>>(id);
            
            var appointment = await _context.Appointments
                .OrderBy(a => a.Id)
                .Where(a => a.PatientId == id)
                .ToDtoListAsync<TDtoResult>(_mapper.ConfigurationProvider);

            return serviceResult.Success(appointment);

        }, nameof(GetByPatientId));

    }
    public async Task<IServiceResult<IEnumerable<TDtoResult>>> GetByDoctorId<TDtoResult>(int id)
        where TDtoResult : class, IAppointmentDto
    {
        return await ExecuteOperationAsync<IEnumerable<TDtoResult>, DoctorServiceErrorMessages>(async serviceResult =>
        {
            if (await _context.Doctors.IsNotExist(id))
                return serviceResult.NotFound<IEnumerable<TDtoResult>>(id);

            var appointments = await _context.Appointments
                .OrderBy(a => a.Id)
                .Where(a => a.DoctorId == id)
                .ToDtoListAsync<TDtoResult>(_mapper.ConfigurationProvider);

            return serviceResult.Success(appointments);

        }, nameof(GetByDoctorId));
    }


    public async Task<IServiceResult<TDtoResult>> GetById<TDtoResult>(int id)
        where TDtoResult : class, IAppointmentDto
    {

        return await ExecuteOperationAsync<TDtoResult, AppointmentServiceErrorMessages>(async serviceResult =>
        {
            var appointments = await _context.Appointments
                .Where(a => a.Id == id)
                .ToDtoAsync<TDtoResult>(_mapper.ConfigurationProvider);

            return appointments is null ? 
                serviceResult.NotFound<TDtoResult>(id) : 
                serviceResult.Success(appointments);

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
        return await ExecuteOperationAsync<TDtoResult, AppointmentServiceErrorMessages>(async serviceResult =>
        {
            var appointment = await _context.Appointments
                .ApplyDtoToEntryIncludes(typeof(TDtoUpdate), _mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(a => a.Id == dtoUpdate.Id);

            if (appointment is null)
                return serviceResult.NotFound<TDtoResult>(dtoUpdate.Id);

            _mapper.Map(dtoUpdate, appointment);

            await _context.SaveChangesAsync();

            return AreDtoToEntryPathsCompatible<Appointment, TDtoUpdate, TDtoResult>() ?
                serviceResult.Success(_mapper.Map<TDtoResult>(appointment)) :
                await GetById<TDtoResult>(appointment.Id);

        }, nameof(Update));
    }

    public async Task<IServiceResult> Delete(int id)
    {
        return await ExecuteOperationAsync<AppointmentServiceErrorMessages>(async serviceResult =>
        {
            var appointment = await _context.Appointments.FirstOrDefaultAsync(a => a.Id == id);

            if (appointment is null)
                return serviceResult.NotFound(id);

            _context.Appointments.Remove(appointment);

            await _context.SaveChangesAsync();

            return serviceResult.Success();

        }, nameof(Delete));
    }

}