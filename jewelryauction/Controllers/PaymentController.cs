using DAL.DTO.PaymentDTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Implement;
using Service.Interface;

namespace jewelryauction.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _service;
        public PaymentController(IPaymentService service)
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
        [HttpGet]
        [Route("GetPaymentByAccountId")]
        public async Task<IActionResult> GetPaymentByAccountId(int id)
        {
            var rs = await _service.GetPaymentByAccountId(id);
            if (rs == null)
            {
                throw new Exception($"Payment with accountId ={id} not found!");
            }
            return Ok(rs);
        }
        [HttpPost]
        [Route("CreatePayment")]
        public async Task<IActionResult> CreatePayment(CreatePaymentDTO createPayment)
        {
            var rs = await _service.CreatePaymentAsync(createPayment);
            return Ok(rs);
        }

        [HttpPut]
        [Route("UpdatePayment")]
        public async Task<IActionResult> UpdatePayment(int id, UpdatePaymentDTO updatePayment)
        {
            var rs = await _service.UpdatePayment(id, updatePayment);
            return Ok(rs);
        }

        [HttpGet("total-fees")]
        public IActionResult GetTotalFees()
        {
            var totalFees = _service.GetTotalFees();
            return Ok(totalFees);
        }

        [HttpGet("total-price")]
        public IActionResult GetTotalPrice()
        {
            var totalPrice = _service.GetTotalPrice();
            return Ok(totalPrice);
        }
        [HttpGet("price")]
        public IActionResult GetPrice()
        {
            var price = _service.GetPrice();
            return Ok(price);
        }

        [HttpGet("fees-statistics-by-date")]
        public IActionResult GetFeesStatisticsByDate()
        {
            var stats = _service.GetFeesStatisticsByDate();
            return Ok(stats);
        }

        [HttpGet("fees-statistics-by-month")]
        public IActionResult GetFeesStatisticsByMonth()
        {
            var stats = _service.GetFeesStatisticsByMonth();
            return Ok(stats);
        }

    }
}
