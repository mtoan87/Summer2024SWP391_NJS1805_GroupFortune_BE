using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DAL.Models;
using Service.Implement;
using Repository.Implement;
using DAL.DTO.JewelryDTO;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace jewelryauction.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JewelriesController : ControllerBase
    {
        private readonly JewelryService _jewelryService;

        public JewelriesController(JewelryService jewelryService)
        {
            _jewelryService = jewelryService;
        }        

        [HttpGet]
        public async Task<ActionResult<Jewelry>> GetAllJewelries()
        {
          var jewelry = _jewelryService.GetAllJewelries();
            return Ok(jewelry);
        }

        [HttpGet("GetById/{Id}")]
        public async Task<IActionResult> GetJewelryById(int Id)
        {
            var jewelry = await _jewelryService.GetJewelryById(Id);
            return Ok(jewelry);
        }

        [HttpGet("GetAuctionAndJewelryByAccountId/{accountId}")]
        public async Task<IActionResult> GetAuctionAndJewelryByAccountId(int accountId)
        {
            var result = await _jewelryService.GetAuctionAndJewelryByAccountIdAsync(accountId);
            return Ok(result);
        }

        [HttpPut]
        [Route("UpdateJewelry")]
        public async Task<IActionResult> UpdateJewelry(int id, UpdateJewelryDTO updateJewelry)
        {
            var rs = await _jewelryService.UpdateJewelry(id, updateJewelry);
            return Ok(rs);
        }

        [HttpPost]
        [Route("CreateJewelry")]
        public async Task<ActionResult<Jewelry>> CreateJewelry([FromForm] CreateJewelryDTO jewelryDTO, IFormFile jewelryImg)
        {
            if (jewelryImg != null)
            {
                var folderPath = Path.Combine("wwwroot", "upload");
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }
                var filePath = Path.Combine(folderPath, jewelryImg.FileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await jewelryImg.CopyToAsync(stream);
                }
                jewelryDTO.JewelryImg = $"upload/{jewelryImg.FileName}";
            }

            await _jewelryService.CreateJewelry(jewelryDTO);
            return Ok(new { message = "Jewelry created successfully" });

        }
        
        [HttpDelete]
        [Route("DeleteJewelry")]
        public async Task<IActionResult> DeleteJewelry(int id)
        {
            var rs = await _jewelryService.DeleteJewelry(id);
            return Ok(rs);
        }

    }
}
