using DAL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Implement;
using Service.Interface;

namespace jewelryauction.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JoinAuctionController : ControllerBase
    {
        private readonly JoinAuctionService _joinAuctionService;
        public JoinAuctionController(JoinAuctionService joinAuctionService)
        {
            _joinAuctionService = joinAuctionService;
        }

        [HttpGet]
        public async Task<ActionResult<JoinAuction>> GetAllJoinAuctions()
        {
            var jewelry = _joinAuctionService.GetAllJoinAuctions();
            return Ok(jewelry);
        }
    }
}
