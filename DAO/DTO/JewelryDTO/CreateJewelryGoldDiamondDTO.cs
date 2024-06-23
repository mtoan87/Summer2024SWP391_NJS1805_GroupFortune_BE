using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DTO.JewelryDTO
{
    public   class CreateJewelryGoldDiamondDTO
    {
        public int? AccountId { get; set; }
        public IFormFile JewelryImg { get; set; }
        public string Name { get; set; } = null!;
        public string Category { get; set; } = null!;
        public string Materials { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string Clarity { get; set; } = null!;
        public string Carat { get; set; } = null!;
        public string GoldAge { get; set; } = null!;
        public string Weight { get; set; } = null!;
        public string? Status { get; set; }
    }
}
