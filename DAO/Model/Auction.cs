using System;
using System.Collections.Generic;

namespace DAL.Model
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
        public int? JewelrySilverId { get; set; }
        public int? JewelryGoldId { get; set; }
        public DateTime Starttime { get; set; }
        public DateTime Endtime { get; set; }
        public string Status { get; set; } = null!;

        public virtual Account? Account { get; set; }
        public virtual JewelryGold? JewelryGold { get; set; }
        public virtual JewelrySilver? JewelrySilver { get; set; }
        public virtual ICollection<Bid> Bids { get; set; }
        public virtual ICollection<JoinAuction> JoinAuctions { get; set; }
    }
}
