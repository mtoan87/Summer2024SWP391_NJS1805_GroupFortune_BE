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

        [HttpGet("GetById/{Id}")]
        public async Task<IActionResult> GetJoinAuctionById(int Id)
        {
            var jewelry = await _joinAuctionService.GetJoinAuctionById(Id);
            return Ok(jewelry);
        }

        [HttpPost]
        [Route("CreateJoinAuction")]
        public async Task<ActionResult<JoinAuction>> CreateJoinAuction(CreateJoinAuctionDTO joinauction)
        {

            var rs = await _joinAuctionService.CreateJoinAuction(joinauction);
            return Ok(rs);
        }

        [HttpPut]
        [Route("UpdateJoinAuction")]
        public async Task<IActionResult> UpdateJoinAuction(int id, UpdateJoinAuctionDTO updateJoinAuction)
        {
            var rs = await _joinAuctionService.UpdateJoinAuction(id, updateJoinAuction);
            return Ok(rs);
        }

        [HttpDelete]
        [Route("DeleteJoinAuction")]
        public async Task<IActionResult> DeleteJoinAuction(int id)
        {
            var rs = await _joinAuctionService.DeleteJoinAuction(id);
            return Ok(rs);
        }

    }
}
