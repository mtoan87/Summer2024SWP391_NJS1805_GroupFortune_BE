using System;
using System.Collections.Generic;

namespace DAL.Models
{
    public partial class Auction
    {
        public Auction()
        {
            Bids = new HashSet<Bid>();
            JoinAuctions = new HashSet<JoinAuction>();
        }

        public int AuctionId { get; set; }
        public int? AccountId { get; set; }
        public int? JewelryId { get; set; }
        public DateTime Starttime { get; set; }
        public DateTime Endtime { get; set; }
        public string Status { get; set; } = null!;

        public virtual Account? Account { get; set; }
        public virtual Jewelry? Jewelry { get; set; }
        public virtual ICollection<Bid> Bids { get; set; }
        public virtual ICollection<JoinAuction> JoinAuctions { get; set; }
    }
}
