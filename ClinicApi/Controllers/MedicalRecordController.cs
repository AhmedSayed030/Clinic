namespace ClinicApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MedicalRecordController : ControllerBase
{
    private readonly IMedicalRecordService _medicalRecordService;

    public MedicalRecordController(IMedicalRecordService medicalRecordService)
    {
        _medicalRecordService = medicalRecordService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _medicalRecordService.GetAll<MedicalRecordDto>();

        return result.MapToActionResult();
    }

    [HttpGet("{id:min(1)}")]
    public async Task<IActionResult> Get(int id)
    {
        var result = await _medicalRecordService.GetById<MedicalRecordDto>(id);

        return result.MapToActionResult();
    }

    [HttpPost]
    public async Task<IActionResult> Add(MedicalRecordDtoAdd medicalRecord)
    {
        var result = await _medicalRecordService.Add<MedicalRecordDto, MedicalRecordDtoAdd>(medicalRecord);

        return result.MapToActionResult();
    }

    [HttpPut]
    public async Task<IActionResult> Update(MedicalRecordDtoUpdated medicalRecord)
    {
        var result = await _medicalRecordService.Update<MedicalRecordDto, MedicalRecordDtoUpdated>(medicalRecord);

        return result.MapToActionResult();
    }

    [HttpDelete("{id:min(1)}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _medicalRecordService.Delete(id);

        return result.MapToActionResult();
    }

}