﻿using System;
using System.Collections.Generic;

namespace DAL.Model
{
    public partial class JoinAuction
    {
        public JoinAuction()
        {
            AuctionResults = new HashSet<AuctionResult>();
            Payments = new HashSet<Payment>();
        }

        public int Id { get; set; }
        public int? AccountId { get; set; }
        public int? AuctionId { get; set; }
        public int? BidId { get; set; }
        public DateTime Joindate { get; set; }

        public virtual Account? Account { get; set; }
        public virtual Auction? Auction { get; set; }
        public virtual Bid? Bid { get; set; }
        public virtual ICollection<AuctionResult> AuctionResults { get; set; }
        public virtual ICollection<Payment> Payments { get; set; }
    }
}
