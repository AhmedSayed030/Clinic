using AutoMapper;
using ClinicDataAccessLayer.Entities;
using ClinicDataBusinessLayer.DTOs.Medication;
using ClinicDataBusinessLayer.DTOs.Prescription;

namespace ClinicDataBusinessLayer.Mappings
{
    public class MedicationProfile : Profile
    {
        public MedicationProfile()
        {

            CreateProjection<Medication, MedicationDto>();

            CreateMap<Medication, MedicationDto>();

            CreateMap<MedicationDtoAdd, Medication>();
            CreateMap<MedicationDtoAddFromPrescription, Medication>();

            CreateMap<MedicationDtoUpdate, Medication>();
            CreateMap<MedicationDtoUpdateFromPrescription, Medication>();

        }

    }


}
