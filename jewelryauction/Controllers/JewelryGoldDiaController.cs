﻿using DAL.DTO.JewelryDTO;
using DAL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Implement;

namespace jewelryauction.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JewelryGoldDiaController : ControllerBase
    {
        private readonly JewelryGoldDiaService _jewelryGoldDiaService;

        public JewelryGoldDiaController(JewelryGoldDiaService jewelryGoldDiaService)
        {
            _jewelryGoldDiaService = jewelryGoldDiaService;
        }

        [HttpGet]
        public async Task<ActionResult<JewelryGoldDiamond>> GetAllGoldDiamondJewelries()
        {
            var jewelry = _jewelryGoldDiaService.GetAllGoldDiaJewelries();
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
        public async Task<ActionResult<JewelryGoldDiamond>> CreateGoldDiamondJewelry([FromForm] CreateJewelryGoldDiamondDTO jewelryDTO, IFormFile jewelryImg)
        {          
            var createdJewelry = await _jewelryGoldDiaService.CreateJewelry(jewelryDTO);
            return Ok(createdJewelry);
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
