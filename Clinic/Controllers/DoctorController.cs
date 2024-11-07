using Microsoft.AspNetCore.Mvc;
using ClinicDataBusinessLayer.Services.Contracts;
using ClinicDataBusinessLayer.DTOs.Doctor;
using ClinicApi.Extensions;

namespace ClinicApi.Controllers
{
    [Route("api/[controller]")]
	[ApiController]
	public class DoctorController : ControllerBase
	{
		private readonly IDoctorService _doctorService;
		public DoctorController(IDoctorService doctorService)
		{
			_doctorService = doctorService;
		}

		[HttpGet()]
		public async Task<IActionResult> GetAll()
		{
            var result = await _doctorService.GetAll<DoctorDto>();

            return result.MapToActionResult();
        }

		[HttpGet("{Id:min(1)}")]
		public async Task<IActionResult> Get(int Id)
		{
            var result = await _doctorService.GetById<DoctorDto>(Id);

            return result.MapToActionResult();
        }

		[HttpPost]
		public async Task<IActionResult> Add(DoctorDtoAdd doctor)
		{
            var result = await _doctorService.Add<DoctorDto, DoctorDtoAdd>(doctor);

            return result.MapToActionResult();
        }

		[HttpPut]
		public async Task<IActionResult> Update(DoctorDtoUpdate doctor)
        {
            var result = await _doctorService.Update<DoctorDto, DoctorDtoUpdate>(doctor);

            return result.MapToActionResult();
        }


		[HttpDelete("{Id:min(1)}")]
		public async Task<IActionResult> Delete(int Id)
		{
            var result = await _doctorService.Delete(Id);

            return result.MapToActionResult();
        }

        [HttpPut("UndoDelete/{Id:min(1)}")]
        public async Task<IActionResult> UndoDelete(int Id)
        {
            var result = await _doctorService.UndoDelete(Id);

            return result.MapToActionResult();
        }
    }
}
