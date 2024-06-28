using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Implement;

namespace jewelryauction.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BidRecordController : ControllerBase
    {
        private readonly BidRecordService _bidRecordService;
        public BidRecordController(BidRecordService bidRecordService)
        {
            _bidRecordService = bidRecordService;
        }

        [HttpGet]
        [Route("GetBidRecords")]
        public async Task<IActionResult> GetBidRecords()
        {
            var rs = await _bidRecordService.GetBidRecords();
            return Ok(rs);
        }
        [HttpGet]
        [Route("GetBidRecordByBidId")]
        public async Task<IActionResult> GetBidRecordByBidId(int bidId)
        {
            var rs = await _bidRecordService.GetBidRecordByBidId(bidId);
            return Ok(rs);
        }
    }
}
