using DAL.DTO.JewelryDTO.Silver;
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
        public async Task<IActionResult> GetAllSilverJewelries()
        {
            var jewelry = await _jewelrySilverService.GetAllSilverJewelries();
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
        public async Task<ActionResult<JewelrySilver>> CreateSilverJewelry([FromForm] CreateJewelrySilverDTO jewelryDTO)
        {
            var createdJewelry = await _jewelrySilverService.CreateJewelry(jewelryDTO);
            return Ok(createdJewelry);
        }
        [HttpPut]
        [Route("UpdateJewelrySilverMember")]
        public async Task<IActionResult> UpdateSilverJewelryMember(int id, [FromForm] UpdateJewelrySilverDTO updateJewelry)
        {

            var existingJewelry = await _jewelrySilverService.GetJewelryById(id);
            if (existingJewelry != null)
            {
                updateJewelry.JewelryImg = existingJewelry.JewelryImg;
            }
            var rs = await _jewelrySilverService.UpdateJewelryMember(id, updateJewelry);
            return Ok(rs);
        }
        [HttpPut]
        [Route("UpdateJewelrySilverStaff")]
        public async Task<IActionResult> UpdateSilverJewelryStaff(int id, [FromForm] UpdateJewelrySilverStaffDTO updateJewelry)
        {
            var existingJewelry = await _jewelrySilverService.GetJewelryById(id);
            if (existingJewelry != null)
            {
                updateJewelry.JewelryImg = existingJewelry.JewelryImg;
            }
            var rs = await _jewelrySilverService.UpdateJewelryStaff(id, updateJewelry);
            return Ok(rs);
        }
        [HttpPut]
        [Route("UpdateJewelrySilverManager")]
        public async Task<IActionResult> UpdateSilverJewelryManager(int id, [FromForm] UpdateJewelrySilverManagerDTO updateJewelry)
        {
            var existingJewelry = await _jewelrySilverService.GetJewelryById(id);
            if (existingJewelry != null)
            {
                updateJewelry.JewelryImg = existingJewelry.JewelryImg;
            }
            var rs = await _jewelrySilverService.UpdateJewelryManager(id, updateJewelry);
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
