using AutoMapper;
using ClinicDataAccessLayer.Entities;
using ClinicDataBusinessLayer.DTOs;
using ClinicDataBusinessLayer.DTOs.Medication;
using ClinicDataBusinessLayer.DTOs.Prescription;
using ClinicDataBusinessLayer.Mappings.Extensions;
using System.Diagnostics.Metrics;
using System.Reflection;

namespace ClinicDataBusinessLayer.Mappings
{
    public class PrescriptionMappingProfile : Profile
    {
        public PrescriptionMappingProfile()
        {
            CreateProjection<Prescription, PrescriptionDto>();

            CreateMap<Prescription, PrescriptionDto>();

            CreateMap<PrescriptionDtoAdd, Prescription>();
            CreateMap<PrescriptionDtoAddFromMedicalRecord, Prescription>();

            CreateMap<PrescriptionDtoUpdate, Prescription>()
              .MapCollection(dest => dest.Medications, sur => sur.Medications, ent => ent.Id, dto => dto.Id);

            CreateMap<PrescriptionDtoUpdateFromMedicalRecord, Prescription>()
              .MapCollection(dest => dest.Medications, sur => sur.Medications, ent => ent.Id, dto => dto.Id);

        }
    }

}
