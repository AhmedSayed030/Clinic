namespace ClinicDataBusinessLayer.Mappings;

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