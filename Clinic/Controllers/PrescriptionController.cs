using ClinicApi.Extensions;
using ClinicDataBusinessLayer.DTOs.Prescription;
using ClinicDataBusinessLayer.Services.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace ClinicApi.Controllers
{
    [Route("api/[controller]")]
	[ApiController]
	public class PrescriptionController : ControllerBase
	{
		private readonly IPrescriptionService _prescriptionService;

		public PrescriptionController(IPrescriptionService prescriptionService)
		{
			_prescriptionService = prescriptionService;
		}

		[HttpGet()]
		public async Task<IActionResult> GetAll()
		{
            var result = await _prescriptionService.GetAll<PrescriptionDto>();

            return result.MapToActionResult();
        }

		[HttpGet("{Id:min(1)}")]
		public async Task<IActionResult> Get(int Id)
		{
            var result = await _prescriptionService.GetById<PrescriptionDto>(Id);

            return result.MapToActionResult();
        }

		[HttpPost]
		public async Task<IActionResult> Add(PrescriptionDtoAdd appointment)
		{

            var result = await _prescriptionService.Add<PrescriptionDto, PrescriptionDtoAdd>(appointment);

            return result.MapToActionResult();
        }

		[HttpPut]
		public async Task<IActionResult> Update(PrescriptionDtoUpdate appointment)
		{
			var result = await _prescriptionService.Update<PrescriptionDto, PrescriptionDtoUpdate>(appointment);

			return result.MapToActionResult();
		}

		[HttpDelete("{Id:min(1)}")]
		public async Task<IActionResult> Delete(int Id)
		{
            var result = await _prescriptionService.Delete(Id);

            return result.MapToActionResult();
        }

	}
}

