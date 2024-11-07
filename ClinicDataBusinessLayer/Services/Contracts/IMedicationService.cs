using ClinicDataBusinessLayer.DTOs.Medication.Contracts;
using ClinicDataBusinessLayer.Services.ServiceResults;

namespace ClinicDataBusinessLayer.Services.Contracts
{
    public interface IMedicationService
    {
        Task<IServiceResult<TDtoResult>> Add<TDtoResult, TDtoAdd>(TDtoAdd dtoAdd)
            where TDtoResult : class, IMedicationDto
            where TDtoAdd : class, IMedicationDtoAdd;
        Task<IServiceResult> Delete(int Id);
        Task<IServiceResult<IEnumerable<TDtoResult>>> GetAll<TDtoResult>() where TDtoResult : class, IMedicationDto;
        Task<IServiceResult<TDtoResult>> GetById<TDtoResult>(int Id) where TDtoResult : class, IMedicationDto;
        Task<IServiceResult<TDtoResult>> Update<TDtoResult, TDtoUpdate>(TDtoUpdate dtoUpdae)
            where TDtoResult : class, IMedicationDto
            where TDtoUpdate : class, IMedicationDtoUpdate;
    }
}