using ClinicDataBusinessLayer.DTOs.Patient.Contracts;
using ClinicDataBusinessLayer.DTOs.Person;

namespace ClinicDataBusinessLayer.DTOs.Patient
{
    public class PatientDtoUpdate : PersonDto, IPatientDtoUpdate
    {
        public int Id { get; set; }
    }
}
