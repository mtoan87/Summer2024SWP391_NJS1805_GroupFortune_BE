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
    }
}
