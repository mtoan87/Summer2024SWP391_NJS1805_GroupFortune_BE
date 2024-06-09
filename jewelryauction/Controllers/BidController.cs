using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Implement;

namespace jewelryauction.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BidController : ControllerBase
    {
        private readonly BidService _bidService;
        public BidController(BidService bidService)
        {
            _bidService = bidService;
        }
        [HttpGet("GetBidByAccountId/{accountId}")]
        public async Task<IActionResult> GetBidByAccountId(int accountId)
        {
            var result = await _bidService.GetBidByAccountIdAsync(accountId);
            return Ok(result);
        }
    }
}
