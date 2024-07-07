using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DTO.BidDTO
{
    public class UpdateBidDTO
    {
        
        public int? AuctionId { get; set; }
        public double Minprice { get; set; }
        public double Maxprice { get; set; }
        public DateTime Datetime { get; set; }
    }
}
