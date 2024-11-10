namespace ClinicDataBusinessLayer.Services.Contracts;

public interface IPaymentService
{
    Task<IServiceResult<IEnumerable<TDtoResult>>> GetAll<TDtoResult>()
        where TDtoResult : class, IPaymentDto;

    Task<IServiceResult<TDtoResult>> GetById<TDtoResult>(int id)
        where TDtoResult : class, IPaymentDto;

    Task<IServiceResult<TDtoResult>> Add<TDtoResult, TDtoAdd>(TDtoAdd dtoAdd)
        where TDtoResult : class, IPaymentDto
        where TDtoAdd : class, IPaymentDtoAdd;

    Task<IServiceResult<TDtoResult>> Update<TDtoResult, TDtoUpdate>(TDtoUpdate dtoUpdae)
        where TDtoResult : class, IPaymentDto
        where TDtoUpdate : class, IPaymentDtoUpdate;

    Task<IServiceResult> Delete(int id);
}