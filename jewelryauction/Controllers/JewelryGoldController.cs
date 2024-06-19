using DAL.DTO.JewelryDTO;
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

        [HttpPost]
        [Route("CreateJewelryGold")]
        public async Task<ActionResult<JewelryGold>> CreateGoldJewelry([FromForm] CreateJewelryGoldDTO jewelryDTO, IFormFile jewelryImg)
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
                jewelryDTO.JewelryImg = $"assets/{jewelryImg.FileName}";
            }

            var createdJewelry = await _jewelryGoldService.CreateJewelry(jewelryDTO);
            return Ok(createdJewelry);
        }

        [HttpPut]
        [Route("UpdateJewelryGoldMember")]
        public async Task<IActionResult> UpdateJewelryGoldMember(int id, [FromForm] UpdateJewelryDTO updateJewelry, IFormFile jewelryImg)
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

            var rs = await _jewelryGoldService.UpdateJewelryMember(id, updateJewelry);
            return Ok(rs);
        }

        [HttpPut]
        [Route("UpdateJewelryGoldStaff")]
        public async Task<IActionResult> UpdateJewelryGoldStaff(int id, [FromForm] UpdateJewelryStaffDTO updateJewelry, IFormFile jewelryImg)
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

            var rs = await _jewelryGoldService.UpdateJewelryStaff(id, updateJewelry);
            return Ok(rs);
        }

        [HttpPut]
        [Route("UpdateJewelryGoldManager")]
        public async Task<IActionResult> UpdateJewelryGoldManager(int id, [FromForm] UpdateJewelryManagerDTO updateJewelry, IFormFile jewelryImg)
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

            var rs = await _jewelryGoldService.UpdateJewelryManager(id, updateJewelry);
            return Ok(rs);
        }


        [HttpDelete]
        [Route("DeleteJewelryGold")]
        public async Task<IActionResult> DeleteSilverJewelry(int id)
        {
            var rs = await _jewelryGoldService.DeleteJewelry(id);
            return Ok(rs);
        }


    }
}
