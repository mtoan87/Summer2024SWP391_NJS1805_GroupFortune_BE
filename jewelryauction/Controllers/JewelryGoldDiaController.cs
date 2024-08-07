﻿using DAL.DTO.JewelryDTO.GoldDiamond;
using DAL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Implement;
using Service.Interface;

namespace jewelryauction.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JewelryGoldDiaController : ControllerBase
    {
        private readonly IJewelryGoldDiamondService _jewelryGoldDiaService;

        public JewelryGoldDiaController(IJewelryGoldDiamondService jewelryGoldDiaService)
        {
            _jewelryGoldDiaService = jewelryGoldDiaService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllGoldDiamondJewelries()
        {
            var jewelry = await _jewelryGoldDiaService.GetAllGoldDiaJewelries();
            return Ok(jewelry);
        }

        [HttpGet("GetById/{Id}")]
        public async Task<IActionResult> GetJewelryGoldDiamondById(int Id)
        {
            var jewelry = await _jewelryGoldDiaService.GetJewelryById(Id);
            return Ok(jewelry);
        }

        [HttpGet("GetAuctionAndJewelryGoldDiamondByAccountId/{accountId}")]
        public async Task<IActionResult> GetAuctionAndJewelryGoldDiamondByAccountId(int accountId)
        {
            var result = await _jewelryGoldDiaService.GetAuctionAndJewelryGoldDiaByAccountIdAsync(accountId);
            return Ok(result);
        }

        [HttpPost]
        [Route("CreateJewelryGoldDiamond")]
        public async Task<ActionResult<JewelryGoldDiamond>> CreateGoldDiamondJewelry([FromForm] CreateJewelryGoldDiamondDTO jewelryDTO)
        {          
            var createdJewelry = await _jewelryGoldDiaService.CreateJewelry(jewelryDTO);
            return Ok(createdJewelry);
        }
        [HttpPut]
        [Route("UpdateJewelryGoldDiamondMember")]
        public async Task<IActionResult> UpdateJewelryGoldDiamondMember(int id, [FromForm] UpdateJewelryGoldDiaDTO updateJewelry)
        {
            
            var rs = await _jewelryGoldDiaService.UpdateJewelryMember(id, updateJewelry);
            return Ok(rs);
        }
        [HttpPut]
        [Route("UpdateJewelryGoldDiamondStaff")]
        public async Task<IActionResult> UpdateJewelryGoldDiamondStaff(int id, [FromForm] UpdateJewelryGoldDiamondStaffDTO updateJewelry)
        {
            
            var rs = await _jewelryGoldDiaService.UpdateJewelryStaff(id, updateJewelry);
            return Ok(rs);
        }

        [HttpPut]
        [Route("UpdateJewelryGoldDiamondManager")]
        public async Task<IActionResult> UpdateJewelryGoldDiamondManager(int id, [FromForm] UpdateJewelryGoldDiamondManagerDTO updateJewelry)
        {
            
            var rs = await _jewelryGoldDiaService.UpdateJewelryManager(id, updateJewelry);
            return Ok(rs);
        }
        [HttpDelete]
        [Route("DeleteJewelryGoldDiamond")]
        public async Task<IActionResult> DeleteSilverJewelry(int id)
        {
            var rs = await _jewelryGoldDiaService.DeleteJewelry(id);
            return Ok(rs);
        }
    }
}
