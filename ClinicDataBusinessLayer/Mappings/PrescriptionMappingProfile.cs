namespace ClinicDataBusinessLayer.Mappings;

public class PrescriptionMappingProfile : Profile
{
    public PrescriptionMappingProfile()
    {
        CreateProjection<Prescription, PrescriptionDto>();

        CreateMap<Prescription, PrescriptionDto>();

        CreateMap<PrescriptionDtoAdd, Prescription>();
        CreateMap<PrescriptionDtoAddFromMedicalRecord, Prescription>();

        CreateMap<PrescriptionDtoUpdate, Prescription>()
            .MapCollection(
                dest => dest.Medications,
                sur => sur.Medications,
                ent => ent.Id,
                dto => dto.Id
            );

        CreateMap<PrescriptionDtoUpdateFromMedicalRecord, Prescription>()
            .MapCollection(
                dest => dest.Medications,
                sur => sur.Medications,
                ent => ent.Id,
                dto => dto.Id
            );

    }
}