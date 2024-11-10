namespace ClinicApi.Controllers;

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

    [HttpGet("{id:min(1)}")]
    public async Task<IActionResult> Get(int id)
    {
        var result = await _doctorService.GetById<DoctorDto>(id);

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


    [HttpDelete("{id:min(1)}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _doctorService.Delete(id);

        return result.MapToActionResult();
    }

    [HttpPut("UndoDelete/{id:min(1)}")]
    public async Task<IActionResult> UndoDelete(int id)
    {
        var result = await _doctorService.UndoDelete(id);

        return result.MapToActionResult();
    }
}