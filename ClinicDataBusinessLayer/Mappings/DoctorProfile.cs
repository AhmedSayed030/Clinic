using AutoMapper;
using ClinicDataAccessLayer.Entities;
using ClinicDataBusinessLayer.DTOs.Doctor;
using ClinicDataBusinessLayer.DTOs.Patient;
using ClinicDataBusinessLayer.DTOs.Prescription;
using ClinicDataBusinessLayer.Mappings.Extensions;

namespace ClinicDataBusinessLayer.Mappings
{
    public class DoctorProfile : Profile
    {
        public DoctorProfile()
        {

            CreateProjection<Doctor, DoctorDto>();

            CreateMap<Doctor, DoctorDto>()
                   .IncludeMembers(p => p.Person);

            CreateMap<Person, DoctorDto>();

            CreateMap<DoctorDtoAdd, Doctor>()
                .MapPersonFieldsForPath();

            CreateMap<DoctorDtoUpdate, Doctor>()
                .MapPersonFieldsForPath();

        }

    }


}
