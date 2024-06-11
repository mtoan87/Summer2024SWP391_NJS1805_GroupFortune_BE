using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DTO.JoinAuctionDTO
{
    public class UpdateJoinAuctionDTO
    {
        public int? AccountId { get; set; }
        public int? AuctionId { get; set; }
        public int? BidId { get; set; }
    }
}
