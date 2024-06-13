using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DTO.AuctionDTO
{
    public class UpdateAuctionDTO
    {

        public int? AccountId { get; set; }
        public int? JewelrySilverId { get; set; }
        public int? JewelryGoldId { get; set; }
        public DateTime DateofAuction { get; set; }
        public TimeSpan Starttime { get; set; }
        public TimeSpan Endtime { get; set; }
        public string Status { get; set; } = null!;
    }
}
