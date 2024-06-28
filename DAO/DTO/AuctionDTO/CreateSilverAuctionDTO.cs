using DAL.DTO.JewelryDTO;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DTO.AuctionDTO
{
    public class CreateSilverAuctionDTO
    {

        public int? AccountId { get; set; }
        public int JewelrySilverId { get; set; }
        public DateTime Starttime { get; set; }
        public DateTime Endtime { get; set; }
    }
}
