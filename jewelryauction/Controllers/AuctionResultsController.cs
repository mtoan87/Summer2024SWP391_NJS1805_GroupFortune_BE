﻿using DAL.DTO.AuctionDTO;
using DAL.DTO.AuctionResultDTO;
using DAL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Implement;
using Service.Interface;

namespace jewelryauction.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuctionResultsController : ControllerBase
    {
        private readonly IAuctionResultService _service;
        public AuctionResultsController(IAuctionResultService service)
        {
            _service = service;
        }
        [HttpGet]
        [Route("GetAllAuctionResults")]
        public async Task<IActionResult> GetAllAuctions()
        {
            var rs = await _service.GetAllAuctionResults();
            return Ok(rs);
        }
        [HttpGet("GetById/{Id}")]
        public async Task<IActionResult> GetAuctionResultById(int Id)
        {
            var jewelry = await _service.GetAuctionResultById(Id);
            return Ok(jewelry);
        }
        [HttpGet("GetByAccountId/{accountId}")]
        public async Task<IActionResult> GetAuctionResultByAccountId(int accountId)
        {
            var jewelry = await _service.GetAuctionResultsByAccountIdAsync(accountId);
            return Ok(jewelry);
        }
        [HttpPut]
        [Route("UpdateAuctionResult")]
        public async Task<IActionResult> UpdateAuctionResult(int id, UpdateAuctionRsDTO updateAuctionRs)
        {
            var rs = await _service.UpdateAuctionRs(id, updateAuctionRs);
            return Ok(rs);
        }
        [HttpPost]
        [Route("CreateAuctionResult")]
        public async Task<IActionResult> CreateAuctionResult(CreateAuctionRsDTO createAuctionRs)
        {
            var result = await _service.CreateAuctionResultAsync(createAuctionRs);
            if (result == null)
            {
                return BadRequest("Failed to process auction result.");
            }
            return Ok(result);
        }
        [HttpDelete]
        [Route("DeleteAuctionRs")]
        public async Task<IActionResult> DeleteAuctionRs(int id)
        {
            var rs = await _service.DeleteAuction(id);
            return Ok(rs);
        }
    }
}
