﻿using DAL.DTO.JewelryDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DTO.AuctionDTO
{
    public class RequestAuctionDTO
    {
        public int? AccountId { get; set; }      
        public string Name { get; set; } = null!;
        public string Materials { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string Weight { get; set; } = null!;
        public string? Goldage { get; set; }
        public string? Collection { get; set; }
        public DateTime Starttime { get; set; }
        public DateTime Endtime { get; set; }
    }
}
