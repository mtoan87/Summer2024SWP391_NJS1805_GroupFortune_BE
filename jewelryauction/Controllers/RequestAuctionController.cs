using DAL.DTO.AuctionDTO;
using DAL.DTO.JewelryDTO;
using DAL.Models;
using Microsoft.AspNetCore.Mvc;
using Service.Implement;

namespace jewelryauction.Controllers
{
    
    public class RequestAuctionController : Controller
    {
        private readonly RequestAuctionService _requestAuctionService;
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [Route("RequestAuction")]
        public async Task<ActionResult<Auction>> CreateAuction(RequestAuctionDTO requestAuction)
        {
            var rs = await _requestAuctionService.CreateAuctionAsync(requestAuction);
            return Ok(rs);
        }
    }
}
