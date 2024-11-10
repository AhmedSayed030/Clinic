namespace ClinicDataBusinessLayer.Services;

public class PatientService : ServiceBase, IPatientService, IScopedService
{
    private readonly IMapper _mapper;
    private readonly AppDbContext _context;

    public PatientService(AppDbContext context,
        ServiceResultHandlerFactory serviceResultHandlerFactory,
        IMapper mapper,
        ServerErrorMessages serverErrorMessages,
        ILogger<PatientService> logger) : base(serviceResultHandlerFactory, mapper, logger, serverErrorMessages)
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
                .ToDtoListAsync<TDtoResult>(_mapper.ConfigurationProvider);

            return serviceResult.Success(patients);

        }, nameof(GetAll));
    }
    public async Task<IServiceResult<TDtoResult>> GetById<TDtoResult>(int id)
        where TDtoResult : class, IPatientDto
    {
        return await ExecuteOperationAsync<TDtoResult, PatientServiceErrorMessages>(async serviceResult =>
        {
            var patient = await _context.Patients
                .NotDeleted()
                .Where(p => p.Id == id)
                .ToDtoAsync<TDtoResult>(_mapper.ConfigurationProvider);

            return patient is null ? 
                serviceResult.NotFound<TDtoResult>(id) : 
                serviceResult.Success(patient);

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
    public async Task<IServiceResult<TDtoResult>> Update<TDtoResult, TDtoUpdate>(TDtoUpdate dtoUpdate)
        where TDtoResult : class, IPatientDto
        where TDtoUpdate : class, IPatientDtoUpdate
    {
        return await ExecuteOperationAsync<TDtoResult, PatientServiceErrorMessages>(async serviceResult =>
        {
            var patient = await _context.Patients
                .NotDeleted()
                .ApplyDtoToEntryIncludes(typeof(TDtoUpdate), _mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(p => p.Id == dtoUpdate.Id);

            if (patient is null)
                return serviceResult.NotFound<TDtoResult>(dtoUpdate.Id);

            _mapper.Map(dtoUpdate, patient);

            await _context.SaveChangesAsync();

            return AreDtoToEntryPathsCompatible<Patient, TDtoUpdate, TDtoResult>() ?
                serviceResult.Success(_mapper.Map<TDtoResult>(patient)) :
                await GetById<TDtoResult>(patient.Id);

        }, nameof(Update));
    }

    public async Task<IServiceResult> Delete(int id)
    {
        return await ExecuteOperationAsync<PatientServiceErrorMessages>(async serviceResult =>
        {
            var patient = await _context.Patients
                .NotDeleted()
                .FirstOrDefaultAsync(p => p.Id == id);

            if (patient is null)
                return serviceResult.NotFound(id);

            _context.Remove(patient);

            await _context.SaveChangesAsync();

            return serviceResult.Success();

        }, nameof(Delete));
    }
    public async Task<IServiceResult> UndoDelete(int id)
    {
        return await ExecuteOperationAsync<PatientServiceErrorMessages>(async serviceResult =>
        {
            ISoftDeleteable? patient = await _context.Patients
                .FirstOrDefaultAsync(p => p.Id == id);

            if (patient is null)
                return serviceResult.NotFound(id);

            if (!patient.IsDeleted)
                return serviceResult.NotDeleted(id);

            patient.UndoDelete();

            await _context.SaveChangesAsync();

            return serviceResult.Success();

        }, nameof(Delete));
    }


}