//using DAL.DTO.AuctionDTO;
//using DAL.Models;
//using Microsoft.AspNetCore.Mvc;
//using Service.Implement;

//namespace jewelryauction.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class RequestAuctionController : ControllerBase
//    {
//        private readonly RequestAuctionService _requestAuctionService;

//        public RequestAuctionController(RequestAuctionService requestAuctionService)
//        {
//            _requestAuctionService = requestAuctionService ?? throw new ArgumentNullException(nameof(requestAuctionService));
//        }

//        [HttpPost]
//        [Route("RequestAuction")]
//        public async Task<ActionResult<Auction>> CreateAuction(RequestAuctionDTO requestAuction)
//        {
//            var rs = await _requestAuctionService.CreateAuctionAsync(requestAuction);
//            return Ok(rs);
//        }
//    }
//}
