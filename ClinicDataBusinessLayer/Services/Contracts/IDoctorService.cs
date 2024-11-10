namespace ClinicDataBusinessLayer.Services.Contracts;

public interface IDoctorService
{
    Task<IServiceResult<IEnumerable<TDtoResult>>> GetAll<TDtoResult>()
        where TDtoResult : class, IDoctorDto;

    Task<IServiceResult<TDtoResult>> GetById<TDtoResult>(int id)
        where TDtoResult : class, IDoctorDto;

    Task<IServiceResult<TDtoResult>> Add<TDtoResult, TDtoAdd>(TDtoAdd dtoAdd)
        where TDtoResult : class, IDoctorDto
        where TDtoAdd : class, IDoctorDtoAdd;

    Task<IServiceResult<TDtoResult>> Update<TDtoResult, TDtoUpdate>(TDtoUpdate dtoUpdate)
        where TDtoResult : class, IDoctorDto
        where TDtoUpdate : class, IDoctorDtoUpdate;

    Task<IServiceResult> Delete(int id);
    Task<IServiceResult> UndoDelete(int id);
}