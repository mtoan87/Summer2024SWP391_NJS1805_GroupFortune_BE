using DAL.Enums;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DTO.JewelryDTO.Silver
{
    public class CreateJewelrySilverDTO
    {
        public int? AccountId { get; set; }
        public IFormFile JewelryImg { get; set; }
        public string Name { get; set; } = null!;
        [EnumDataType(typeof(Material))]
        public string Materials { get; set; } = null!;
        [EnumDataType(typeof(Category))]
        public string Category { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string Weight { get; set; } = null!;
        [EnumDataType(typeof(Purity))]
        public string Purity { get; set; } = null!;


    }
}
