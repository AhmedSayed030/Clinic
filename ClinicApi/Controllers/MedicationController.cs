namespace ClinicApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MedicationController : ControllerBase
{
    private readonly IMedicationService _medicationService;

    public MedicationController(IMedicationService medicationService)
    {
        _medicationService = medicationService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _medicationService.GetAll<MedicationDto>();

        return result.MapToActionResult();
    }

    [HttpGet("{id:min(1)}")]
    public async Task<IActionResult> Get(int id)
    {
        var result = await _medicationService.GetById<MedicationDto>(id);

        return result.MapToActionResult();
    }

    [HttpPost]
    public async Task<IActionResult> Add(MedicationDtoAdd medication)
    {
        var result = await _medicationService.Add<MedicationDto, MedicationDtoAdd>(medication);

        return result.MapToActionResult();
    }

    [HttpPut]
    public async Task<IActionResult> Update(MedicationDtoUpdate medication)
    {
        var result = await _medicationService.Update<MedicationDto, MedicationDtoUpdate>(medication);

        return result.MapToActionResult();
    }

    [HttpDelete("{id:min(1)}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _medicationService.Delete(id);

        return result.MapToActionResult();
    }
}