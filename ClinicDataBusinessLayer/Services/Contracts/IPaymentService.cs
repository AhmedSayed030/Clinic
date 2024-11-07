using ClinicDataBusinessLayer.DTOs.Payment.Contracts;
using ClinicDataBusinessLayer.Services.ServiceResults;

namespace ClinicDataBusinessLayer.Services.Contracts
{
    public interface IPaymentService
    {
        Task<IServiceResult<TDtoResult>> Add<TDtoResult, TDtoAdd>(TDtoAdd dtoAdd)
            where TDtoResult : class, IPaymentDto
            where TDtoAdd : class, IPaymentDtoAdd;
        Task<IServiceResult> Delete(int Id);
        Task<IServiceResult<IEnumerable<TDtoResult>>> GetAll<TDtoResult>() where TDtoResult : class, IPaymentDto;
        Task<IServiceResult<TDtoResult>> GetById<TDtoResult>(int Id) where TDtoResult : class, IPaymentDto;
        Task<IServiceResult<TDtoResult>> Update<TDtoResult, TDtoUpdate>(TDtoUpdate dtoUpdae)
            where TDtoResult : class, IPaymentDto
            where TDtoUpdate : class, IPaymentDtoUpdate;
    }
}