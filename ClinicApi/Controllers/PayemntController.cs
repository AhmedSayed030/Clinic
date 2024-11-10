namespace ClinicApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PaymentController : ControllerBase
{
    private readonly IPaymentService _paymentService;

    public PaymentController(IPaymentService paymentService)
    {
        _paymentService = paymentService;
    }

    [HttpGet()]
    public async Task<IActionResult> GetAll()
    {
        var result = await _paymentService.GetAll<PaymentDto>();

        return result.MapToActionResult();
    }

    [HttpGet("{id:min(1)}")]
    public async Task<IActionResult> Get(int id)
    {
        var result = await _paymentService.GetById<PaymentDto>(id);

        return result.MapToActionResult();
    }

    [HttpPost]
    public async Task<IActionResult> Add(PaymentDtoAdd payment)
    {
        var result = await _paymentService.Add<PaymentDto, PaymentDtoAdd>(payment);

        return result.MapToActionResult();
    }

    [HttpPut]
    public async Task<IActionResult> Update(PaymentDtoUpdate payment)
    {
        var result = await _paymentService.Update<PaymentDto, PaymentDtoUpdate>(payment);

        return result.MapToActionResult();
    }

    [HttpDelete("{id:min(1)}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _paymentService.Delete(id);

        return result.MapToActionResult();
    }
}