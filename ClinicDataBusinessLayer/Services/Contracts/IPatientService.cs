namespace ClinicDataBusinessLayer.Services.Contracts;

public interface IPatientService
{
    Task<IServiceResult<IEnumerable<TDtoResult>>> GetAll<TDtoResult>()
        where TDtoResult : class, IPatientDto;

    Task<IServiceResult<TDtoResult>> GetById<TDtoResult>(int id)
        where TDtoResult : class, IPatientDto;

    Task<IServiceResult<TDtoResult>> Add<TDtoResult, TDtoAdd>(TDtoAdd dtoAdd)
        where TDtoResult : class, IPatientDto
        where TDtoAdd : class, IPatientDtoAdd;

    Task<IServiceResult<TDtoResult>> Update<TDtoResult, TDtoUpdate>(TDtoUpdate dtoUpdate)
        where TDtoResult : class, IPatientDto
        where TDtoUpdate : class, IPatientDtoUpdate;

    Task<IServiceResult> Delete(int id);
    Task<IServiceResult> UndoDelete(int id);
}