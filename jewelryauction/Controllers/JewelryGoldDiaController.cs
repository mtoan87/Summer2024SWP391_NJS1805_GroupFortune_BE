using DAL.DTO.JewelryDTO;
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

            var createdJewelry = await _jewelryGoldDiaService.CreateJewelry(jewelryDTO);
            return Ok(createdJewelry);
        }

        [HttpPut]
        [Route("UpdateJewelryGoldDiamondMember")]
        public async Task<IActionResult> UpdateJewelryGoldDiamondMember(int id, [FromForm] UpdateJewelryGoldDiaDTO updateJewelry, IFormFile jewelryImg)
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

            var rs = await _jewelryGoldDiaService.UpdateJewelryMember(id, updateJewelry);
            return Ok(rs);
        }

        [HttpPut]
        [Route("UpdateJewelryGoldDiamondStaff")]
        public async Task<IActionResult> UpdateJewelryGoldDiamondStaff(int id, [FromForm] UpdateJewelryGoldDiamondStaffDTO updateJewelry, IFormFile jewelryImg)
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

            var rs = await _jewelryGoldDiaService.UpdateJewelryStaff(id, updateJewelry);
            return Ok(rs);
        }


        [HttpPut]
        [Route("UpdateJewelryGoldDiamondManager")]
        public async Task<IActionResult> UpdateJewelryGoldDiamondManager(int id, [FromForm] UpdateJewelryGoldDiamondManagerDTO updateJewelry, IFormFile jewelryImg)
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

            var rs = await _jewelryGoldDiaService.UpdateJewelryManager(id, updateJewelry);
            return Ok(rs);
        }
    }
}
