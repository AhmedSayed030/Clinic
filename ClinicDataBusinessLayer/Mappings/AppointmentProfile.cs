using AutoMapper;
using ClinicDataAccessLayer.Entities;
using ClinicDataBusinessLayer.DTOs.Appointment;
using ClinicDataBusinessLayer.DTOs.Doctor;

namespace ClinicDataBusinessLayer.Mappings
{
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
}
