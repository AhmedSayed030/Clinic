namespace ClinicApi.Controllers;

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

    [HttpGet("{id:min(1)}")]
    public async Task<IActionResult> Get(int id)
    {
        var result = await _prescriptionService.GetById<PrescriptionDto>(id);

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

    [HttpDelete("{id:min(1)}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _prescriptionService.Delete(id);

        return result.MapToActionResult();
    }

}