namespace ClinicApi.Controllers;

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

    [HttpGet("{id:min(1)}")]
    public async Task<IActionResult> Get(int id)
    {
        var result = await _patientService.GetById<PatientDto>(id);

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

    [HttpDelete("{id:min(1)}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _patientService.Delete(id);

        return result.MapToActionResult();
    }

    [HttpPut("UndoDelete/{id:min(1)}")]
    public async Task<IActionResult> UndoDelete(int id)
    {
        var result = await _patientService.UndoDelete(id);

        return result.MapToActionResult();
    }
}