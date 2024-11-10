namespace ClinicDataBusinessLayer.Services;

public class MedicationService : ServiceBase, IMedicationService, IScopedService
{
    private readonly IMapper _mapper;
    private readonly AppDbContext _context;

    public MedicationService(AppDbContext context,
        ServiceResultHandlerFactory serviceResultHandlerFactory,
        IMapper mapper,
        ServerErrorMessages serverErrorMessages,
        ILogger<MedicationService> logger) : base(serviceResultHandlerFactory, mapper, logger, serverErrorMessages)
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
                .ToDtoListAsync<TDtoResult>(_mapper.ConfigurationProvider);

            return serviceResult.Success(medications);

        }, nameof(GetAll));
    }
    public async Task<IServiceResult<TDtoResult>> GetById<TDtoResult>(int id)
        where TDtoResult : class, IMedicationDto
    {

        return await ExecuteOperationAsync<TDtoResult, MedicationServiceErrorMessages>(async serviceResult =>
        {
            var medication = await _context.Medications
                .Where(m => m.Id == id)
                .ToDtoAsync<TDtoResult>(_mapper.ConfigurationProvider);

            return medication is null ? 
                serviceResult.NotFound<TDtoResult>(id) : 
                serviceResult.Success(medication);

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
    public async Task<IServiceResult<TDtoResult>> Update<TDtoResult, TDtoUpdate>(TDtoUpdate dtoUpdate)
        where TDtoResult : class, IMedicationDto
        where TDtoUpdate : class, IMedicationDtoUpdate
    {
        return await ExecuteOperationAsync<TDtoResult, MedicationServiceErrorMessages>(async serviceResult =>
        {
            var medication = await _context.Medications
                .ApplyDtoToEntryIncludes(typeof(TDtoUpdate), _mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(a => a.Id == dtoUpdate.Id);

            if (medication is null)
                return serviceResult.NotFound<TDtoResult>(dtoUpdate.Id);

            _mapper.Map(dtoUpdate, medication);

            await _context.SaveChangesAsync();

            return AreDtoToEntryPathsCompatible<Medication, TDtoUpdate, TDtoResult>() ?
                serviceResult.Success(_mapper.Map<TDtoResult>(medication)) :
                await GetById<TDtoResult>(medication.Id);

        }, nameof(Update));
    }

    public async Task<IServiceResult> Delete(int id)
    {
        return await ExecuteOperationAsync<MedicationServiceErrorMessages>(async serviceResult =>
        {
            var medication = await _context.Medications.FirstOrDefaultAsync(d => d.Id == id);

            if (medication is null)
                return serviceResult.NotFound(id);

            _context.Remove(medication);

            await _context.SaveChangesAsync();

            return serviceResult.Success();

        }, nameof(Delete));
    }


}