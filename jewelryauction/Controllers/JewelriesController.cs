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
        public async Task<ActionResult<Jewelry>> CreateJewelry(CreateJewelryDTO jewelry)
        {
            var rs = await _jewelryService.CreateJewelry(jewelry);
            return Ok(rs );
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
