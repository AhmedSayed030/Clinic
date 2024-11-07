using ClinicApi.Extensions;
using ClinicDataAccessLayer.Entities;
using ClinicDataBusinessLayer.DTOs.Payment;
using ClinicDataBusinessLayer.Services.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace ClinicApi.Controllers
{
    [Route("api/[controller]")]
	[ApiController]
	public class PaymentController : ControllerBase
	{
        private readonly IPaymentService _payementService;

        public PaymentController(IPaymentService paymentService)
		{
            _payementService = paymentService;
        }

		[HttpGet()]
		public async Task<IActionResult> GetAll()
		{
            var result = await _payementService.GetAll<PaymentDto>();

            return result.MapToActionResult();
        }

		[HttpGet("{Id}")]
		public async Task<IActionResult> Get(int Id)
		{
            var result = await _payementService.GetById<PaymentDto>(Id);

            return result.MapToActionResult();
        }

        [HttpPost]
		public async Task<IActionResult> Add(PaymentDtoAdd payment)
		{
            var result = await _payementService.Add<PaymentDto, PaymentDtoAdd>(payment);

            return result.MapToActionResult();
        }

		[HttpPut]
		public async Task<IActionResult> Update(PaymentDtoUpdaet payment)
		{
            var result = await _payementService.Update<PaymentDto, PaymentDtoUpdaet>(payment);

            return result.MapToActionResult();
        }

		[HttpDelete("{Id:min(1)}")]
		public async Task<IActionResult> Delete(int Id)
		{
            var result = await _payementService.Delete(Id);

            return result.MapToActionResult();
        }
	}
}

