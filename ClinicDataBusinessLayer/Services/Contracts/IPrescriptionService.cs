namespace ClinicDataBusinessLayer.Services.Contracts;

public interface IPrescriptionService
{
    Task<IServiceResult<IEnumerable<TDtoResult>>> GetAll<TDtoResult>()
        where TDtoResult : class, IPrescriptionDto;

    Task<IServiceResult<TDtoResult>> GetById<TDtoResult>(int id)
        where TDtoResult : class, IPrescriptionDto;

    Task<IServiceResult<TDtoResult>> Add<TDtoResult, TDtoAdd>(TDtoAdd dtoAdd)
        where TDtoResult : class, IPrescriptionDto
        where TDtoAdd : class, IPrescriptionDtoAdd;

    Task<IServiceResult<TDtoResult>> Update<TDtoResult, TDtoUpdate>(TDtoUpdate dtoUpdate)
        where TDtoResult : class, IPrescriptionDto
        where TDtoUpdate : class, IPrescriptionDtoUpdate;

    Task<IServiceResult> Delete(int id);
}