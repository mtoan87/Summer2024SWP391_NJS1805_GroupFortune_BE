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
using Microsoft.CodeAnalysis.CSharp.Syntax;

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
        public IActionResult GetAllJewelry()
        {
            var (silvers, golds, golddias) = _jewelryService.GetAllJewelry();

            if (!silvers.Any() && !golds.Any() && !golddias.Any())
            {
                return NoContent();
            }

            return Ok(new
            {
                JewelrySilver = silvers,
                JewelryGold = golds,
                JewelryGoldDiamond = golddias
            });
        }


        [HttpGet]
        [Route("GetUnVerified")]
        public IActionResult GetUnverifiedJewelry()
        {
            var (silvers, golds, golddias) = _jewelryService.GetUnVerified();

            if (!silvers.Any() && !golds.Any() && !golddias.Any())
            {
                return NoContent();
            }

            return Ok(new
            {
                JewelrySilver = silvers,
                JewelryGold = golds,
                JewelryGoldDiamond = golddias
            });
        }

        

    }
}
