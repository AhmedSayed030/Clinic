namespace ClinicApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AppointmentController : ControllerBase
{
    private readonly IAppointmentService _appointmentService;

    public AppointmentController(IAppointmentService appointmentService)
    {
        _appointmentService = appointmentService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _appointmentService.GetAll<AppointmentDto>();

        return result.MapToActionResult();
    }

    [HttpGet("GetByPatientId/{id:min(1)}")]
    public async Task<IActionResult> GetByPatientId(int id)
    {
        var result = await _appointmentService.GetByPatientId<AppointmentDto>(id);

        return result.MapToActionResult();
    }

    [HttpGet("GetByDoctorId/{id:min(1)}")]
    public async Task<IActionResult> GetByDoctorId(int id)
    {
        var result = await _appointmentService.GetByDoctorId<AppointmentDto>(id);

        return result.MapToActionResult();
    }
		

    [HttpGet("{id:min(1)}")]
    public async Task<IActionResult> Get(int id)
    {
        var result = await _appointmentService.GetById<AppointmentDto>(id);

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

    [HttpDelete("{id:min(1)}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _appointmentService.Delete(id);

        return result.MapToActionResult();
    }

}