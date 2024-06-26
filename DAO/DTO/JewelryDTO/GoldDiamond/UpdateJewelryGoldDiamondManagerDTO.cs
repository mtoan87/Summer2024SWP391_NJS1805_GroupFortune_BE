using DAL.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DTO.JewelryDTO.GoldDiamond
{
    public class UpdateJewelryGoldDiamondManagerDTO
    {
        public int? AccountId { get; set; }
        public string? JewelryImg { get; set; }
        public string Name { get; set; } = null!;
        [EnumDataType(typeof(Category))]
        public string Category { get; set; } = null!;
        [EnumDataType(typeof(Material))]
        public string Materials { get; set; } = null!;
        public string Description { get; set; } = null!;
        [EnumDataType(typeof(DiamondClarity))]
        public string Clarity { get; set; } = null!;
        public string Carat { get; set; } = null!;
        [EnumDataType(typeof(GoldAge))]
        public string GoldAge { get; set; } = null!;

        public double? Price { get; set; }
        public string Weight { get; set; } = null!;
        [EnumDataType(typeof(JewelryStatus))]
        public string? Status { get; set; }
    }
}
