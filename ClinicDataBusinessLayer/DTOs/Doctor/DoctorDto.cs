using ClinicDataBusinessLayer.DTOs.Doctor.Contracts;
using ClinicDataBusinessLayer.DTOs.Person;
using Newtonsoft.Json;

namespace ClinicDataBusinessLayer.DTOs.Doctor
{
    public class DoctorDto : PersonDto, IDoctorDto
    {
        [JsonProperty(Order = -2)]
        public int Id { get; set; }

        [JsonProperty(Order = -1)]
        public string Specializatio { get; set; } = string.Empty;
    }
}
