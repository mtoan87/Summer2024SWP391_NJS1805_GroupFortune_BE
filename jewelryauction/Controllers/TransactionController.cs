using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Interface;

namespace jewelryauction.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionService _service;
        public TransactionController(ITransactionService service)
        {
            _service = service; 
        }

        [HttpGet]
        [Route("GetAllTransactions")]
        public async Task<IActionResult> GetAllTransactions()
        {
            var rs = await _service.GetAllTransactions();
            return Ok(rs);
        }
        [HttpGet]
        [Route("GetTransactionById")]
        public async Task<IActionResult> GetTransactionById(int id)
        {
            var rs = await _service.GetTransactionById(id);
            if (rs == null)
            {
                throw new Exception($"Transaction with {id} not found!");
            }
            return Ok(rs);
        }
        [HttpGet]
        [Route("GetTransactionByWalletId")]
        public async Task<IActionResult> GetTransactionByWalletId(int id)
        {
            var rs = await _service.GetTransactionByWalletId(id);
            if (rs == null)
            {
                throw new Exception($"Transaction with walletId :{id} not found!");
            }
            return Ok(rs);
        }
    }
}
