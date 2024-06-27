using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DTO.BidDTO
{
    public class CreateBidDTO
    {
        public int? AccountId { get; set; }
        public int? AuctionId { get; set; }
        public double Minprice { get; set; }       
        public DateTime Datetime { get; set; }
    }
}
