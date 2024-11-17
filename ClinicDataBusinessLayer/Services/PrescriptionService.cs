namespace ClinicDataBusinessLayer.Services;

public class PrescriptionService : ServiceBase, IScopedService, IPrescriptionService
{
    private readonly IMapper _mapper;
    private readonly AppDbContext _context;

    public PrescriptionService(AppDbContext context,
        ServiceResultHandlerFactory serviceResultHandlerFactory,
        IMapper mapper,
        ServerErrorMessages serverErrorMessages,
        ILogger<PrescriptionService> logger) : base(serviceResultHandlerFactory, mapper, logger, serverErrorMessages)
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
                .ToDtoListAsync<TDtoResult>(_mapper.ConfigurationProvider);

            return serviceResult.Success(prescriptions);

        }, nameof(GetAll));
    }
    public async Task<IServiceResult<TDtoResult>> GetById<TDtoResult>(int id)
        where TDtoResult : class, IPrescriptionDto
    {

        return await ExecuteOperationAsync<TDtoResult, PrescriptionServiceErrorMessages>(async serviceResult =>
        {
            var prescription = await _context.Prescriptions
                .Where(p => p.Id == id)
                .ToDtoAsync<TDtoResult>(_mapper.ConfigurationProvider);

            return prescription is null 
                ? serviceResult.NotFound<TDtoResult>(id) 
                : serviceResult.Success(prescription);

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

            return AreDtoToEntryPathsCompatible<Prescription, TDtoAdd, TDtoResult>()
                ? serviceResult.Success(_mapper.Map<TDtoResult>(prescription))
                : await GetById<TDtoResult>(prescription.Id);

        }, nameof(Add));
    }
    public async Task<IServiceResult<TDtoResult>> Update<TDtoResult, TDtoUpdate>(TDtoUpdate dtoUpdate)
        where TDtoResult : class, IPrescriptionDto
        where TDtoUpdate : class, IPrescriptionDtoUpdate
    {
        return await ExecuteOperationAsync<TDtoResult, PrescriptionServiceErrorMessages>(async serviceResult =>
        {
            var prescription = await _context.Prescriptions
                .ApplyDtoToEntryIncludes(typeof(TDtoUpdate), _mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(p => p.Id == dtoUpdate.Id);

            if (prescription is null)
                return serviceResult.NotFound<TDtoResult>(dtoUpdate.Id);

            _mapper.Map(dtoUpdate, prescription);

            await _context.SaveChangesAsync();

            return AreDtoToEntryPathsCompatible<Prescription, TDtoUpdate, TDtoResult>()
                ? serviceResult.Success(_mapper.Map<TDtoResult>(prescription))
                : await GetById<TDtoResult>(prescription.Id);

        }, nameof(Update));
    }

    public async Task<IServiceResult> Delete(int id)
    {
        return await ExecuteOperationAsync<PrescriptionServiceErrorMessages>(async serviceResult =>
        {
            var prescription = await _context.Prescriptions.FirstOrDefaultAsync(d => d.Id == id);

            if (prescription is null)
                return serviceResult.NotFound(id);

            _context.Remove(prescription);

            await _context.SaveChangesAsync();

            return serviceResult.Success();

        }, nameof(Delete));
    }
}