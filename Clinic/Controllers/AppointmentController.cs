using ClinicApi.Extensions;
using ClinicDataBusinessLayer.DTOs.Appointment;
using ClinicDataBusinessLayer.Services.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace ClinicApi.Controllers
{
    [Route("api/[controller]")]
	[ApiController]
	public class AppointmentController : ControllerBase
	{
		private readonly IAppointmentService _appointmentService;

		public AppointmentController(IAppointmentService appointmentService)
		{
			
			_appointmentService = appointmentService;
        }

		[HttpGet()]
		public async Task<IActionResult> GetAll()
		{
			var result = await _appointmentService.GetAll<AppointmentDto>();

			return result.MapToActionResult();
		}

		[HttpGet("GetByPatientId/{Id:min(1)}")]
		public async Task<IActionResult> GetByPatientId(int Id)
		{
            var result = await _appointmentService.GetByPatientId<AppointmentDto>(Id);

            return result.MapToActionResult();
        }

		[HttpGet("GetByDoctorId/{Id:min(1)}")]
		public async Task<IActionResult> GetByDoctorId(int Id)
		{
            var result = await _appointmentService.GetByDoctorId<AppointmentDto>(Id);

            return result.MapToActionResult();
        }
		

		[HttpGet("{Id:min(1)}")]
		public async Task<IActionResult> Get(int Id)
		{
            var result = await _appointmentService.GetById<AppointmentDto>(Id);

            return result.MapToActionResult();
        }

		[HttpPost]
		public async Task<IActionResult> Add(AppointmentDtoAdd appointment)
		{
            var result = await _appointmentService.Add<AppointmentDto, AppointmentDtoAdd>(appointment);

            return result.MapToActionResult();
        }

		[HttpPut]
        public async Task<IActionResult> Update(AppointmentDtoUpdate appointment)
		{
            var result = await _appointmentService.Update<AppointmentDto, AppointmentDtoUpdate>(appointment);

            return result.MapToActionResult();
        }

		[HttpDelete("{Id}")]
		public async Task<IActionResult> Delete(int Id)
		{
            var result = await _appointmentService.Delete(Id);

            return result.MapToActionResult();
        }

	}
}
