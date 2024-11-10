namespace ClinicDataBusinessLayer.Services.Contracts;

public interface IMedicationService
{
    Task<IServiceResult<IEnumerable<TDtoResult>>> GetAll<TDtoResult>()
        where TDtoResult : class, IMedicationDto;

    Task<IServiceResult<TDtoResult>> GetById<TDtoResult>(int id)
        where TDtoResult : class, IMedicationDto;

    Task<IServiceResult<TDtoResult>> Add<TDtoResult, TDtoAdd>(TDtoAdd dtoAdd)
        where TDtoResult : class, IMedicationDto
        where TDtoAdd : class, IMedicationDtoAdd;

    Task<IServiceResult<TDtoResult>> Update<TDtoResult, TDtoUpdate>(TDtoUpdate dtoUpdate)
        where TDtoResult : class, IMedicationDto
        where TDtoUpdate : class, IMedicationDtoUpdate;

    Task<IServiceResult> Delete(int id);
}