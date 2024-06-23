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
        public async Task<ActionResult<JewelryGold>> CreateGoldJewelry([FromForm] CreateJewelryGoldDTO jewelryDTO)
        {
            var createdJewelry = await _jewelryGoldService.CreateJewelry(jewelryDTO);
            return Ok(createdJewelry);
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
