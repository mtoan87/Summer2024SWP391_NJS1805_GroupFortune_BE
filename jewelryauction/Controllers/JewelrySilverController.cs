using DAL.DTO.JewelryDTO;
using DAL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Implement;

namespace jewelryauction.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JewelrySilverController : ControllerBase
    {
        private readonly JewelrySilverService _jewelrySilverService;

        public JewelrySilverController(JewelrySilverService jewelrySilverService)
        {
            _jewelrySilverService = jewelrySilverService;
        }

        [HttpGet]
        public async Task<ActionResult<JewelrySilver>> GetAllSilverJewelries()
        {
            var jewelry = _jewelrySilverService.GetAllSilverJewelries();
            return Ok(jewelry);
        }

        [HttpGet("GetById/{Id}")]
        public async Task<IActionResult> GetJewelrySilverById(int Id)
        {
            var jewelry = await _jewelrySilverService.GetJewelryById(Id);
            return Ok(jewelry);
        }

        [HttpGet("GetAuctionAndJewelrySilverByAccountId/{accountId}")]
        public async Task<IActionResult> GetAuctionAndJewelrySilverByAccountId(int accountId)
        {
            var result = await _jewelrySilverService.GetAuctionAndJewelrySilverByAccountIdAsync(accountId);
            return Ok(result);
        }

        [HttpPost]
        [Route("CreateSilverJewelry")]
        public async Task<ActionResult<JewelrySilver>> CreateSilverJewelry([FromForm] CreateJewelrySilverDTO jewelryDTO, IFormFile jewelryImg)
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

            var createdJewelry = await _jewelrySilverService.CreateJewelry(jewelryDTO);
            return Ok(createdJewelry);
        }

        [HttpPut]
        [Route("UpdateJewelrySilverMember")]
        public async Task<IActionResult> UpdateSilverJewelryMember(int id, [FromForm] UpdateJewelrySilverDTO updateJewelry, IFormFile jewelryImg)
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

            var rs = await _jewelrySilverService.UpdateJewelryMember(id, updateJewelry);
            return Ok(rs);
        }

        [HttpPut]
        [Route("UpdateJewelrySilverStaff")]
        public async Task<IActionResult> UpdateSilverJewelryStaff(int id, [FromForm] UpdateJewelrySilverStaffDTO updateJewelry, IFormFile jewelryImg)
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

            var rs = await _jewelrySilverService.UpdateJewelryStaff(id, updateJewelry);
            return Ok(rs);
        }

        [HttpDelete]
        [Route("DeleteJewelrySilver")]
        public async Task<IActionResult> DeleteSilverJewelry(int id)
        {
            var rs = await _jewelrySilverService.DeleteJewelry(id);
            return Ok(rs);
        }


    }
}
