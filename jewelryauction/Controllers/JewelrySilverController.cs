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

    }
}
