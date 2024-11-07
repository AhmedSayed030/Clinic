using ClinicDataBusinessLayer.DTOs.Patient;
using ClinicDataBusinessLayer.DTOs.Patient.Contracts;
using ClinicDataBusinessLayer.DTOs.Person;
using Newtonsoft.Json;

namespace ClinicDataBusinessLayer.DTOs.Patient
{
    public class PatientDto : PersonDto, IPatientDto
    {
        [JsonProperty(Order = -1)]
        public int Id { get; set; }
    }
}
