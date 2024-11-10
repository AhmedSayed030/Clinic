namespace ClinicDataBusinessLayer.Mappings;

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