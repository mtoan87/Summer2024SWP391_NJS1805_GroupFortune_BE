using DAL.DTO.BidDTO;
using DAL.DTO.JewelryDTO;
using DAL.DTO.JoinAuctionDTO;
using DAL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Implement;
using Service.Interface;

namespace jewelryauction.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BidController : ControllerBase
    {
        private readonly IBidService _bidService;
        public BidController(IBidService bidService)
        {
            _bidService = bidService;
        }

        [HttpGet]
        [Route("GetAllBids")]
        public async Task<IActionResult> GetAllBids()
        {
            var jewelry = await _bidService.GetAllBids();
            return Ok(jewelry);
        }
        //[HttpGet("GetBidByAccountId/{accountId}")]
        //public async Task<IActionResult> GetBidByAccountId(int accountId)
        //{
        //    var result = await _bidService.GetBidByAccountIdAsync(accountId);
        //    return Ok(result);
        //}
        //[HttpGet("GetBidRecordByAccountId/{accountId}")]
        //public async Task<IActionResult> GetBidRecordByAccountId(int accountId)
        //{
        //    var result = await _bidService.GetBidRecordByAccountId(accountId);
        //    return Ok(result);
        //}
        [HttpPost]
        [Route("CreateBid")]
        public async Task<IActionResult> CreateBid(CreateBidDTO createBid)
        {
            var rs = await _bidService.CreateBid(createBid);
            return Ok(rs);
        }
        [HttpPost]
        [Route("Bidding")]
        public async Task<IActionResult> Bidding([FromBody] BiddingDTO bidding)
        {
            var rs = await _bidService.PlaceBid(bidding);
            return Ok(rs);
        }
        [HttpPost]
        [Route("UpdateBid")]
        public async Task<IActionResult> UpdateBid(int id,UpdateBidDTO update)
        {
            var rs = await _bidService.UpdateBid(id,update);
            return Ok(rs);
        }
    }
}
