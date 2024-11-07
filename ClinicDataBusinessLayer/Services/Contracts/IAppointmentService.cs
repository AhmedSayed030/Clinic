using ClinicDataBusinessLayer.DTOs.Appointment.Contracts;
using ClinicDataBusinessLayer.Services.ServiceResults;

namespace ClinicDataBusinessLayer.Services.Contracts
{
    public interface IAppointmentService
    {
        Task<IServiceResult<TDtoResult>> Add<TDtoResult, TDtoAdd>(TDtoAdd dtoAdd)
            where TDtoResult : class, IAppointmentDto
            where TDtoAdd : class, IAppointmentDtoAdd;
        Task<IServiceResult> Delete(int Id);
        Task<IServiceResult<IEnumerable<TDtoResult>>> GetAll<TDtoResult>() where TDtoResult : class, IAppointmentDto;
        Task<IServiceResult<IEnumerable<TDtoResult>>> GetByDoctorId<TDtoResult>(int Id) where TDtoResult : class, IAppointmentDto;
        Task<IServiceResult<TDtoResult>> GetById<TDtoResult>(int Id) where TDtoResult : class, IAppointmentDto;
        Task<IServiceResult<IEnumerable<TDtoResult>>> GetByPatientId<TDtoResult>(int Id) where TDtoResult : class, IAppointmentDto;
        Task<IServiceResult<TDtoResult>> Update<TDtoResult, TDtoUpdate>(TDtoUpdate dtoUpdate)
            where TDtoResult : class, IAppointmentDto
            where TDtoUpdate : class, IAppointmentDtoUpdate;
    }
}