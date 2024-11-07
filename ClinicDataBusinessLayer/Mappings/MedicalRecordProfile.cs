using AutoMapper;
using ClinicDataAccessLayer.Entities;
using ClinicDataBusinessLayer.DTOs.Appointment;
using ClinicDataBusinessLayer.DTOs.Doctor;
using ClinicDataBusinessLayer.DTOs.MedicalRecord;

namespace ClinicDataBusinessLayer.Mappings
{
    public class MedicalRecordProfile : Profile
    {
        public MedicalRecordProfile()
        {
            CreateProjection<MedicalRecord, MedicalRecordDto>();

            CreateMap<MedicalRecord, MedicalRecordDto>();

            CreateMap<MedicalRecordDtoAdd, MedicalRecord>();

            CreateMap<MedicalRecordDtoUpdated, MedicalRecord>();
        }

    }

}
