using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DAL.Models;
using Service.Implement;
using DAL.DTO.JewelryDTO;
using Service.Interface;
using DAL.DTO.AuctionDTO;

namespace jewelryauction.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuctionsController : ControllerBase
    {
        private readonly AuctionService _auctionService;

        public AuctionsController(AuctionService auctionService)
        {
            _auctionService = auctionService;
        }
        
        [HttpGet]
        [Route("GetAllAuctions")]
        public async Task<ActionResult<Auction>> GetAllAuctions()
        {
            var rs = _auctionService.GetAllAuctions();
            return Ok(rs);
        }
        [HttpGet("GetById/{Id}")]
        public async Task<IActionResult> GetAuctionById(int Id)
        {
            var jewelry = await _auctionService.GetAuctionById(Id);
            return Ok(jewelry);
        }
        [HttpGet("GetAuctionAndJewelrySilverByAccountId/{accountId}")]
        public async Task<IActionResult> GetAuctionAndJewelrySilverByAccountId(int accountId)
        {
            var result = await _auctionService.GetAuctionAndJewelrySilverByAccountIdAsync(accountId);
            return Ok(result);
        }
        [HttpGet]
        [Route("GetAllActiveAuctions")]
        public async Task<ActionResult<Auction>> GetAllActiveAuctions()
        {
            var rs = _auctionService.GetAllActiveAuctions();
            return Ok(rs);
        }
        [HttpGet]
        [Route("GetAllUnActiveAuctions")]
        public async Task<ActionResult<Auction>> GetAllUnActiveAuctions()
        {
            var rs = _auctionService.GetAllUnActiveAuctions();
            return Ok(rs);
        }
        [HttpPut]
        [Route("UpdateAuction")]
        public async Task<IActionResult> UpdateSilverAuction(int id, UpdateSilverAuctionDTO updateAuction)
        {
            var rs = await _auctionService.UpdateSilverAuction(id, updateAuction);
            return Ok(rs);
        }
        [HttpPost]
        [Route("CreateGoldJewelryAuction")]
        public async Task<ActionResult<Auction>> CreateJewelryGoldAuction(CreateGoldAuctionDTO createAuction)
        {
            var rs = await _auctionService.CreateJewelryGoldAuction(createAuction);
            return Ok(rs);
        }
        [HttpPost]
        [Route("CreateSilverJewelryAuction")]
        public async Task<ActionResult<Auction>> CreateJewelrySilverAuction(CreateSilverAuctionDTO createAuction)
        {
            var rs = await _auctionService.CreateJewelrySilverAuction(createAuction);
            return Ok(rs);
        }
        [HttpDelete]
        [Route("DeleteAuction")]
        public async Task<IActionResult> DeleteAuction(int id)
        {
            var rs = await _auctionService.DeleteAuction(id);
            return Ok(rs);
        }
        [HttpGet("{auctionId}/account-count")]
        public IActionResult GetAccountCountInAuction(int auctionId)
        {
            try
            {
                var count = _auctionService.GetAccountCountInAuction(auctionId);
                return Ok(new { AuctionId = auctionId, AccountCount = count });
            }
            catch (Exception ex)
            {
                
                return StatusCode(500, "Internal server error");
            }
        }

    }
}
