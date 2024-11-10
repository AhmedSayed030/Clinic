namespace ClinicDataBusinessLayer.Services.Contracts;

public interface IMedicalRecordService
{
    Task<IServiceResult<IEnumerable<TDtoResult>>> GetAll<TDtoResult>()
        where TDtoResult : class, IMedicalRecordDto;

    Task<IServiceResult<TDtoResult>> GetById<TDtoResult>(int id)
        where TDtoResult : class, IMedicalRecordDto;

    Task<IServiceResult<TDtoResult>> Add<TDtoResult, TDtoAdd>(TDtoAdd dtoAdd)
        where TDtoResult : class, IMedicalRecordDto
        where TDtoAdd : class, IMedicalRecordDtoAdd;

    Task<IServiceResult<TDtoResult>> Update<TDtoResult, TDtoUpdate>(TDtoUpdate dtoUpdate)
        where TDtoResult : class, IMedicalRecordDto
        where TDtoUpdate : class, IMedicalRecordDtoUpdate;

    Task<IServiceResult> Delete(int id);
}