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

        
    }
}
