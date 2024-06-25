using DAL.DTO.PaymentDTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Implement;

namespace jewelryauction.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly PaymentService _service;
        public PaymentController(PaymentService service)
        {
            _service = service;

        }

        [HttpGet]
        [Route("GetAllPayments")]
        public async Task<IActionResult> GetAllPayments()
        {
            var rs = await _service.GetAllPayments();
            return Ok(rs);
        }
        [HttpGet]
        [Route("GetPaymentById")]
        public async Task<IActionResult> GetPaymentById(int id)
        {
            var rs = await _service.GetPaymentById(id);
            if(rs == null)
            {
                throw new Exception($"Payment with {id} not found!");
            }
            return Ok(rs);
        }
        [HttpPost]
        [Route("CreatePayment")]
        public async Task<IActionResult> CreatePayment(CreatePaymentDTO createPayment)
        {
            var rs = await _service.CreatePayment(createPayment);
            return Ok(rs);
        }

        [HttpPut]
        [Route("UpdatePayment")]
        public async Task<IActionResult> UpdatePayment(int id, UpdatePaymentDTO updatePayment)
        {
            var rs = await _service.UpdatePayment(id, updatePayment);
            return Ok(rs);
        }

    }
}
