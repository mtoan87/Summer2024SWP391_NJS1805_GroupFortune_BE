﻿using DAL.DTO.PaymentDTO;
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
        

    }
}
