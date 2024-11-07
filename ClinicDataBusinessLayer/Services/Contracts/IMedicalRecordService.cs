using ClinicDataBusinessLayer.DTOs.MedicalRecord.Contracts;
using ClinicDataBusinessLayer.Services.ServiceResults;

namespace ClinicDataBusinessLayer.Services.Contracts
{
    public interface IMedicalRecordService
    {
        Task<IServiceResult<TDtoResult>> Add<TDtoResult, TDtoAdd>(TDtoAdd dtoAdd)
            where TDtoResult : class, IMedicalRecordDto
            where TDtoAdd : class, IMedicalRecordDtoAdd;
        Task<IServiceResult> Delete(int Id);
        Task<IServiceResult<IEnumerable<TDtoResult>>> GetAll<TDtoResult>() where TDtoResult : class, IMedicalRecordDto;
        Task<IServiceResult<TDtoResult>> GetById<TDtoResult>(int Id) where TDtoResult : class, IMedicalRecordDto;
        Task<IServiceResult<TDtoResult>> Update<TDtoResult, TDtoUpdate>(TDtoUpdate dtoUpdae)
            where TDtoResult : class, IMedicalRecordDto
            where TDtoUpdate : class, IMedicalRecordDtoUpdate;
    }
}