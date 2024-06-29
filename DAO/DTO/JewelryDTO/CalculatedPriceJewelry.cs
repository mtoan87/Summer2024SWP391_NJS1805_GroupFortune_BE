using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DTO.JewelryDTO
{
    public class CalculatedPriceJewelry
    {
        public int AccountId { get; set; }     
        public string Materials { get; set; }       
        public float Weight { get; set; }
        public string WeightUnit { get; set; }
        public string? GoldAge { get; set; }
        public string? Purity { get; set; }
        public float Price { get; set; }
        public float? Carat { get; set; }
        public string? Clarity { get; set; }
    }
}
