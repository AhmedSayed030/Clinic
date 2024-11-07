using ClinicDataBusinessLayer.DTOs.Doctor.Contracts;
using ClinicDataBusinessLayer.Services.ServiceResults;

namespace ClinicDataBusinessLayer.Services.Contracts
{
    public interface IDoctorService
    {
        Task<IServiceResult<TDtoResult>> Add<TDtoResult, TDtoAdd>(TDtoAdd dtoAdd)
            where TDtoResult : class, IDoctorDto
            where TDtoAdd : class, IDoctorDtoAdd;
        Task<IServiceResult> Delete(int Id);
        Task<IServiceResult> UndoDelete(int Id);
        Task<IServiceResult<IEnumerable<TDtoResult>>> GetAll<TDtoResult>() where TDtoResult : class, IDoctorDto;
        Task<IServiceResult<TDtoResult>> GetById<TDtoResult>(int Id) where TDtoResult : class, IDoctorDto;
        Task<IServiceResult<TDtoResult>> Update<TDtoResult, TDtoUpdate>(TDtoUpdate dtoUpdae)
            where TDtoResult : class, IDoctorDto
            where TDtoUpdate : class, IDoctorDtoUpdate;
    }
}