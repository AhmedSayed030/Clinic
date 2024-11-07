using ClinicApi.Extensions;
using ClinicDataBusinessLayer.DTOs.Patient;
using ClinicDataBusinessLayer.Services.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace ClinicApi.Controllers
{

    [Route("api/[controller]")]
	[ApiController]
	public class PatientController : ControllerBase
	{
		private readonly IPatientService _patientService;

		public PatientController(IPatientService patientService)
		{
			_patientService = patientService;
		}

		[HttpGet()]
		public async Task<IActionResult> GetAll()
		{
            var result = await _patientService.GetAll<PatientDto>();

            return result.MapToActionResult();
        }


		[HttpGet("{Id:min(1)}")]
		public async Task<IActionResult> Get(int Id)
		{
            var result = await _patientService.GetById<PatientDto>(Id);

            return result.MapToActionResult();
        }
		
		[HttpPost]
		public async Task<IActionResult> Add(PatientDtoAdd patient)
		{
            var result = await _patientService.Add<PatientDto, PatientDtoAdd>(patient);

            return result.MapToActionResult();
        }

		[HttpPut]
		public async Task<IActionResult> Update(PatientDtoUpdate patient)
		{
            var result = await _patientService.Update<PatientDto, PatientDtoUpdate>(patient);

            return result.MapToActionResult();
        }

		[HttpDelete("{Id:min(1)}")]
		public async Task<IActionResult> Delete(int Id)
		{
            var result = await _patientService.Delete(Id);

            return result.MapToActionResult();
        }

		[HttpPut("UndoDelete/{Id:min(1)}")]
		public async Task<IActionResult> UndoDelete(int Id)
		{
			var result = await _patientService.UndoDelete(Id);

            return result.MapToActionResult();
        }
    }
}

