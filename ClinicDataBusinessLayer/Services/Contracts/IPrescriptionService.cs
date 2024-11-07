using ClinicDataBusinessLayer.DTOs.Prescription.Contracts;
using ClinicDataBusinessLayer.Services.ServiceResults;

namespace ClinicDataBusinessLayer.Services.Contracts
{
    public interface IPrescriptionService
    {
        Task<IServiceResult<TDtoResult>> Add<TDtoResult, TDtoAdd>(TDtoAdd dtoAdd)
            where TDtoResult : class, IPrescriptionDto
            where TDtoAdd : class, IPrescriptionDtoAdd;
        Task<IServiceResult> Delete(int Id);
        Task<IServiceResult<IEnumerable<TDtoResult>>> GetAll<TDtoResult>() where TDtoResult : class, IPrescriptionDto;
        Task<IServiceResult<TDtoResult>> GetById<TDtoResult>(int Id) where TDtoResult : class, IPrescriptionDto;
        Task<IServiceResult<TDtoResult>> Update<TDtoResult, TDtoUpdate>(TDtoUpdate dtoUpdae)
            where TDtoResult : class, IPrescriptionDto
            where TDtoUpdate : class, IPrescriptionDtoUpdate;
    }
}