namespace ClinicDataBusinessLayer.Mappings;

public class AppointmentProfile : Profile
{
    public AppointmentProfile()
    {
        CreateProjection<Appointment, AppointmentDto>();

        CreateMap<Appointment, AppointmentDto>();

        CreateMap<AppointmentDtoAdd, Appointment>();

        CreateMap<AppointmentDtoUpdate, Appointment>();

    }

}