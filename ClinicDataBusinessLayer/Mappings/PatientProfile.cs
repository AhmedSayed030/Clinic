namespace ClinicDataBusinessLayer.Mappings;

public class PatientProfile : Profile
{
    public PatientProfile()
    {

        CreateProjection<Patient, PatientDto>();

        CreateMap<Patient, PatientDto>()
            .IncludeMembers(p => p.Person);

        CreateMap<Person, PatientDto>();

        CreateMap<PatientDtoAdd, Patient>()
            .MapPersonFieldsForPath();

        CreateMap<PatientDtoUpdate, Patient>()
            .MapPersonFieldsForPath();
    }

}