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
        private readonly AuctionResultService _service;
        public AuctionResultsController(AuctionResultService service)
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
        [HttpPut]
        [Route("UpdateAuctionResult")]
        public async Task<IActionResult> UpdateAuctionResult(int id, UpdateAuctionRsDTO updateAuctionRs)
        {
            var rs = await _service.UpdateAuctionRs(id, updateAuctionRs);
            return Ok(rs);
        }
        [HttpPost]
        [Route("CreateAuctionResult")]
        public async Task<ActionResult<AuctionResult>> CreateAuctionResult(CreateAuctionRsDTO createAuctionRs)
        {
            var rs = await _service.CreateAuctionRs(createAuctionRs);
            return Ok(rs);
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
