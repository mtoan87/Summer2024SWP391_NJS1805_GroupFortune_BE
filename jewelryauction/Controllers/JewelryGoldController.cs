using DAL.DTO.JewelryDTO.Gold;
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
        public async Task<IActionResult> GetAllGoldJewelries()
        {
            var jewelry = await _jewelryGoldService.GetAllGoldJewelries();
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
        public async Task<ActionResult<JewelryGold>> CreateGoldJewelry([FromForm] CreateJewelryGoldDTO jewelryDTO)
        {
            var createdJewelry = await _jewelryGoldService.CreateJewelry(jewelryDTO);
            return Ok(createdJewelry);
        }
        [HttpPut]
        [Route("UpdateJewelryGoldMember")]
        public async Task<IActionResult> UpdateJewelryGoldMember(int id, [FromForm] UpdateJewelryDTO updateJewelry)
        {
            var existingJewelry = await _jewelryGoldService.GetJewelryById(id);
            if (existingJewelry != null)
            {
                updateJewelry.JewelryImg = existingJewelry.JewelryImg;
            }
            var rs = await _jewelryGoldService.UpdateJewelryMember(id, updateJewelry);
            return Ok(rs);
        }
        [HttpPut]
        [Route("UpdateJewelryGoldStaff")]
        public async Task<IActionResult> UpdateJewelryGoldStaff(int id, [FromForm] UpdateJewelryStaffDTO updateJewelry)
        {
            var existingJewelry = await _jewelryGoldService.GetJewelryById(id);
            if (existingJewelry != null)
            {
                updateJewelry.JewelryImg = existingJewelry.JewelryImg;
            }
            var rs = await _jewelryGoldService.UpdateJewelryStaff(id, updateJewelry);
            return Ok(rs);
        }
        [HttpPut]
        [Route("UpdateJewelryGoldManager")]
        public async Task<IActionResult> UpdateJewelryGoldManager(int id, [FromForm] UpdateJewelryManagerDTO updateJewelry)
        {
            var existingJewelry = await _jewelryGoldService.GetJewelryById(id);
            if (existingJewelry != null)
            {
                updateJewelry.JewelryImg = existingJewelry.JewelryImg;
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
