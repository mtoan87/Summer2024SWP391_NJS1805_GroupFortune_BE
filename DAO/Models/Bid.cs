using System;
using System.Collections.Generic;

namespace DAL.Models
{
    public partial class Bid
    {
        public Bid()
        {
            BidRecords = new HashSet<BidRecord>();
            JoinAuctions = new HashSet<JoinAuction>();
        }

        public int BidId { get; set; }
        public int? AccountId { get; set; }
        public int? AuctionId { get; set; }
        public double Minprice { get; set; }
        public double Maxprice { get; set; }
        public DateTime Datetime { get; set; }

        public virtual Account? Account { get; set; }
        public virtual Auction? Auction { get; set; }
        public virtual ICollection<BidRecord> BidRecords { get; set; }
        public virtual ICollection<JoinAuction> JoinAuctions { get; set; }
    }
}
