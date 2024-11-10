namespace ClinicDataBusinessLayer.Services;

public class DoctorService : ServiceBase, IDoctorService, IScopedService
{
    private readonly IMapper _mapper;
    private readonly AppDbContext _context;

    public DoctorService(AppDbContext context,
        ServiceResultHandlerFactory serviceResultHandlerFactory,
        IMapper mapper,
        ServerErrorMessages serverErrorMessages,
        ILogger<AppointmentService> logger) : base(serviceResultHandlerFactory, mapper, logger, serverErrorMessages)
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
                .ToDtoListAsync<TDtoResult>(_mapper.ConfigurationProvider);

            return serviceResult.Success(doctors);

        }, nameof(GetAll));
    }
    public async Task<IServiceResult<TDtoResult>> GetById<TDtoResult>(int id)
        where TDtoResult : class, IDoctorDto
    {

        return await ExecuteOperationAsync<TDtoResult, DoctorServiceErrorMessages>(async serviceResult =>
        {
            var doctor = await _context.Doctors
                .NotDeleted()
                .Where(d => d.Id == id)
                .ToDtoAsync<TDtoResult>(_mapper.ConfigurationProvider);

            return doctor is null ? 
                serviceResult.NotFound<TDtoResult>(id) : 
                serviceResult.Success(doctor);

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
    public async Task<IServiceResult<TDtoResult>> Update<TDtoResult, TDtoUpdate>(TDtoUpdate dtoUpdate)
        where TDtoResult : class, IDoctorDto
        where TDtoUpdate : class, IDoctorDtoUpdate
    {
        return await ExecuteOperationAsync<TDtoResult, DoctorServiceErrorMessages>(async serviceResult =>
        {
            var doctor = await _context.Doctors
                .NotDeleted()
                .ApplyDtoToEntryIncludes(typeof(TDtoUpdate), _mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(a => a.Id == dtoUpdate.Id);

            if (doctor is null)
                return serviceResult.NotFound<TDtoResult>(dtoUpdate.Id);

            _mapper.Map(dtoUpdate, doctor);

            await _context.SaveChangesAsync();

            return AreDtoToEntryPathsCompatible<Doctor, TDtoUpdate, TDtoResult>() ?
                serviceResult.Success(_mapper.Map<TDtoResult>(doctor)) :
                await GetById<TDtoResult>(doctor.Id);

        }, nameof(Update));
    }

    public async Task<IServiceResult> Delete(int id)
    {
        return await ExecuteOperationAsync<DoctorServiceErrorMessages>(async serviceResult =>
        {
            var doctor = await _context.Doctors
                .NotDeleted()
                .FirstOrDefaultAsync(d => d.Id == id);

            if (doctor is null)
                return serviceResult.NotFound(id);

            _context.Remove(doctor);

            await _context.SaveChangesAsync();

            return serviceResult.Success();

        }, nameof(Delete));
    }

    public async Task<IServiceResult> UndoDelete(int id)
    {
        return await ExecuteOperationAsync<DoctorServiceErrorMessages>(async serviceResult =>
        {
            ISoftDeleteable? doctor = await _context.Doctors
                .FirstOrDefaultAsync(p => p.Id == id);

            if (doctor is null)
                return serviceResult.NotFound(id);
            if (!doctor.IsDeleted)
                return serviceResult.NotDeleted(id);

            doctor.UndoDelete();

            await _context.SaveChangesAsync();

            return serviceResult.Success();

        }, nameof(Delete));
    }
}