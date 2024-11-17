namespace ClinicDataBusinessLayer.Services;

public class MedicalRecordService : ServiceBase, IMedicalRecordService, IScopedService
{
    private readonly IMapper _mapper;
    private readonly AppDbContext _context;

    public MedicalRecordService(AppDbContext context,
        ServiceResultHandlerFactory serviceResultHandlerFactory,
        IMapper mapper,
        ServerErrorMessages serverErrorMessages,
        ILogger<MedicalRecordService> logger) : base(serviceResultHandlerFactory, mapper, logger, serverErrorMessages)
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
                .ToDtoListAsync<TDtoResult>(_mapper.ConfigurationProvider);

            return serviceResult.Success(medicalRecords);

        }, nameof(GetAll));
    }
    public async Task<IServiceResult<TDtoResult>> GetById<TDtoResult>(int id)
        where TDtoResult : class, IMedicalRecordDto
    {

        return await ExecuteOperationAsync<TDtoResult, MedicalRecordServiceErrorMessages>(async serviceResult =>
        {
            var medicalRecord = await _context.MedicalRecords
                .Where(m => m.Id == id)
                .ToDtoAsync<TDtoResult>(_mapper.ConfigurationProvider);

            return medicalRecord is null 
                ? serviceResult.NotFound<TDtoResult>(id) 
                : serviceResult.Success(medicalRecord);

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

            return AreDtoToEntryPathsCompatible<MedicalRecord, TDtoAdd, TDtoResult>()
                ? serviceResult.Success(_mapper.Map<TDtoResult>(medicalRecord))
                : await GetById<TDtoResult>(medicalRecord.Id);

        }, nameof(Add));
    }
    public async Task<IServiceResult<TDtoResult>> Update<TDtoResult, TDtoUpdate>(TDtoUpdate dtoUpdate)
        where TDtoResult : class, IMedicalRecordDto
        where TDtoUpdate : class, IMedicalRecordDtoUpdate
    {
        return await ExecuteOperationAsync<TDtoResult, MedicalRecordServiceErrorMessages>(async serviceResult =>
        {
            var medicalRecord = await _context.MedicalRecords
                .ApplyDtoToEntryIncludes(typeof(TDtoUpdate), _mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(m => m.Id == dtoUpdate.Id);

            if (medicalRecord is null)
                return serviceResult.NotFound<TDtoResult>(dtoUpdate.Id);

            _mapper.Map(dtoUpdate, medicalRecord);

            await _context.SaveChangesAsync();

            return AreDtoToEntryPathsCompatible<MedicalRecord, TDtoUpdate, TDtoResult>()
                ? serviceResult.Success(_mapper.Map<TDtoResult>(medicalRecord))
                : await GetById<TDtoResult>(medicalRecord.Id);

        }, nameof(Update));
    }

    public async Task<IServiceResult> Delete(int id)
    {
        return await ExecuteOperationAsync<MedicalRecordServiceErrorMessages>(async serviceResult =>
        {
            var medicalRecord = await _context.MedicalRecords.FirstOrDefaultAsync(m => m.Id == id);

            if (medicalRecord is null)
                return serviceResult.NotFound(id);

            _context.Remove(medicalRecord);

            await _context.SaveChangesAsync();

            return serviceResult.Success();

        }, nameof(Delete));
    }

}