﻿using DAL.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DTO.AuctionDTO
{
    public class UpdateGoldAuctionDTO
    {

        public int? AccountId { get; set; }
        public int? JewelryGoldId { get; set; }
        public DateTime Starttime { get; set; }
        public DateTime Endtime { get; set; }
        [EnumDataType(typeof(AuctionStatus))]
        public string Status { get; set; } = null!;
    }
}
