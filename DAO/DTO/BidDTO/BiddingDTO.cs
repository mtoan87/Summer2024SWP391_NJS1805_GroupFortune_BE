using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DTO.BidDTO
{
    public class BiddingDTO
    {
        public int BidId { get; set; }
        public int AuctionId { get; set; }
        public double BidStep { get; set; }
    }
}
