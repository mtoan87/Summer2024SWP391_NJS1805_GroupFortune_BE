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
using DAL.Enums;
using Humanizer;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using System.Buffers.Text;
using System.Diagnostics;
using System.Security.Policy;
using Microsoft.VisualBasic;
using System.IO.Pipelines;
using System.Reflection;

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
            { "Gold24", 2450 },
            { "Gold22", 2300 },
            { "Gold20", 2100 },
            { "Gold18", 1800 },
            { "Gold14", 1400 }
            // tinh tren 1 oz
//            The current prices for gold in 2024 have shown significant increases. As of recent data, the price per ounce of gold has reached around $2,450, with expectations that it may average $2,500 by the fourth quarter of 2024 and potentially rise to $2,600 by the end of 2025​ (APMEX)​​ (J.P. Morgan | Official Website)​.

//            Here are the updated prices for gold based on purity:

//            Gold24 (24 karat): $2,450 per ounce
//            Gold22 (22 karat): $2,300 per ounce (approximate, based on a typical 5-10% reduction from 24k prices)
//            Gold20 (20 karat): $2,100 per ounce (approximate)
//            Gold18 (18 karat): $1,800 per ounce (approximate)
//            Gold14 (14 karat): $1,400 per ounce (approximate)
        };

        private readonly Dictionary<string, (float buying, float selling)> silverPricesPerOunce = new Dictionary<string, (float buying, float selling)>
        {
            { "PureSilver999", (0, 30) },
            { "PureSilver958", (0, 29) },
            { "PureSilver925", (0, 28) },
            { "PureSilver900", (0, 27) }
//            Based on recent data, the price of pure silver per ounce is approximately $29.27​ (Kitco)​​ (markets.businessinsider.com)​. Here is the updated buying and selling information for different purities of silver:

//            Pure Silver 999:

//            Buying: $29.27
//            Selling: $30.00 (approximation based on current market)
//            Pure Silver 958:

//            Buying: $28.00
//            Selling: $29.00 (approximation)
//            Pure Silver 925:

//            Buying: $27.00
//            Selling: $28.00 (approximation)
//            Pure Silver 900:

//            Buying: $26.00
//            Selling: $27.00 (approximation)
//            These updated prices reflect current market trends and fluctuations.
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

            
            price += price * 0.10f;

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
        //1 gram = 0.035274 ounces
        //1 oz ~~ 28grams
        //
    }
}
