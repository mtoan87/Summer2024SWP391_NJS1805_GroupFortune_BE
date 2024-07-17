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
using Service.Interface;

namespace jewelryauction.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JewelriesController : ControllerBase
    {
        private readonly IJewelryService _jewelryService;

        public JewelriesController(IJewelryService jewelryService)
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

        [HttpGet]
        [Route("GetVerified")]
        public IActionResult GetVerifiedJewelry()
        {
            var (silvers, golds, golddias) = _jewelryService.GetVerified();

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
        private readonly Dictionary<string, float> goldPricesPerOunce = new Dictionary<string, float>
    {
        { "Gold24", 1950 },
        { "Gold22", 1800 },
        { "Gold18", 1500 },
        { "Gold14", 1200 },
        { "Gold10", 900 }
       
    };

        private readonly Dictionary<string, (float buying, float selling)> silverPricesPerOunce = new Dictionary<string, (float buying, float selling)>
    {
        { "PureSilver999", (1950, 2000) },
        { "PureSilver958", (1910, 1950) },
        { "PureSilver925", (0, 1820) },
        { "PureSilver900", (0, 1450) } 
    };

        private readonly Dictionary<string, float> diamondPricesPerCarat = new Dictionary<string, float>
    {
        { "FL", 8000 },
        { "IF", 7000 },
        { "VVS1", 6000 },
        { "VVS2", 5000 },
        { "VS1", 4000 },
        { "VS2", 3000 },
        { "SI1", 2000 },
        { "SI2", 1000 },
        { "I1", 500 },
        { "I2", 300 },
        { "I3", 100 }
    };
        [HttpPost("CalculatePrice")]
        public IActionResult CalculatePrice([FromBody] CalculatedPriceJewelry jewelry)
        {
            if (jewelry == null || jewelry.Weight <= 0) return BadRequest("Invalid jewelry details");

            var ounces = ConvertToOunces(jewelry.Weight, jewelry.WeightUnit);
            float price = 0;

            if (jewelry.Materials == "Gold" && !string.IsNullOrEmpty(jewelry.GoldAge))
            {
                price = ounces * goldPricesPerOunce[jewelry.GoldAge];
            }
            else if (jewelry.Materials == "Silver" && !string.IsNullOrEmpty(jewelry.Purity))
            {
                var purityPrice = silverPricesPerOunce[jewelry.Purity];
                price = ounces * purityPrice.selling;
            }
            else if (jewelry.Materials == "GoldDiamond" && !string.IsNullOrEmpty(jewelry.GoldAge) && !string.IsNullOrEmpty(jewelry.Clarity) && jewelry.Carat.HasValue)
            {
                var goldPrice = ounces * goldPricesPerOunce[jewelry.GoldAge];
                var diamondPrice = jewelry.Carat.Value * diamondPricesPerCarat[jewelry.Clarity];
                price = goldPrice + diamondPrice;
            }
            else
            {
                return BadRequest("Invalid materials or missing required properties");
            }

            return Ok(new { CalculatedPrice = price });
        }

        private float ConvertToOunces(float weight, string unit)
        {
            var conversionRates = new Dictionary<string, float>
        {
            { "grams", 0.035274f },
            { "milligrams", 0.000035274f },
            { "ounces", 1 },
            { "pennyweights", 0.05f }
        };

            return weight * conversionRates[unit];
        }

    }
}
