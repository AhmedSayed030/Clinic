namespace ClinicDataBusinessLayer.Services.Contracts;

public interface IAppointmentService
{
    Task<IServiceResult<IEnumerable<TDtoResult>>> GetAll<TDtoResult>()
        where TDtoResult : class, IAppointmentDto;

    Task<IServiceResult<IEnumerable<TDtoResult>>> GetByPatientId<TDtoResult>(int id)
        where TDtoResult : class, IAppointmentDto;

    Task<IServiceResult<IEnumerable<TDtoResult>>> GetByDoctorId<TDtoResult>(int id)
        where TDtoResult : class, IAppointmentDto;

    Task<IServiceResult<TDtoResult>> GetById<TDtoResult>(int id)
        where TDtoResult : class, IAppointmentDto;

    Task<IServiceResult<TDtoResult>> Add<TDtoResult, TDtoAdd>(TDtoAdd dtoAdd)
        where TDtoResult : class, IAppointmentDto
        where TDtoAdd : class, IAppointmentDtoAdd;

    Task<IServiceResult<TDtoResult>> Update<TDtoResult, TDtoUpdate>(TDtoUpdate dtoUpdate)
        where TDtoResult : class, IAppointmentDto
        where TDtoUpdate : class, IAppointmentDtoUpdate;

    Task<IServiceResult> Delete(int id);
}