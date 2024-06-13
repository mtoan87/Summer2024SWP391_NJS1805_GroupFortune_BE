﻿using DAL.DTO.JewelryDTO;
using DAL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Implement;

namespace jewelryauction.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JewelryGoldController : ControllerBase
    {
        private readonly JewelryGoldService _jewelryGoldService;

        public JewelryGoldController(JewelryGoldService jewelryGoldService)
        {
            _jewelryGoldService = jewelryGoldService;
        }

        [HttpGet]
        public async Task<ActionResult<JewelryGold>> GetAllGoldJewelries()
        {
            var jewelry = _jewelryGoldService.GetAllGoldJewelries();
            return Ok(jewelry);
        }

        [HttpGet("GetById/{Id}")]
        public async Task<IActionResult> GetJewelryGoldById(int Id)
        {
            var jewelry = await _jewelryGoldService.GetJewelryById(Id);
            return Ok(jewelry);
        }

        [HttpGet("GetAuctionAndJewelryGoldByAccountId/{accountId}")]
        public async Task<IActionResult> GetAuctionAndJewelryGoldByAccountId(int accountId)
        {
            var result = await _jewelryGoldService.GetAuctionAndJewelryGoldByAccountIdAsync(accountId);
            return Ok(result);
        }

        [HttpPut]
        [Route("UpdateJewelryGold")]
        public async Task<IActionResult> UpdateJewelryGold(int id, [FromForm] UpdateJewelryDTO updateJewelry, IFormFile jewelryImg)
        {
            if (jewelryImg != null)
            {
                var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "assets");
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }
                var filePath = Path.Combine(folderPath, jewelryImg.FileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await jewelryImg.CopyToAsync(stream);
                }
                updateJewelry.JewelryImg = $"assets/{jewelryImg.FileName}";
            }

            var rs = await _jewelryGoldService.UpdateJewelry(id, updateJewelry);
            return Ok(rs);
        }


    }
}
