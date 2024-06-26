﻿using System;
using System.Collections.Generic;

namespace DAL.Models
{
    public partial class AuctionResult
    {
        public AuctionResult()
        {
            Payments = new HashSet<Payment>();
        }

        public int AuctionresultId { get; set; }
        public int? JoinauctionId { get; set; }
        public DateTime Date { get; set; }
        public string Status { get; set; } = null!;
        public double Price { get; set; }
        public int? AccountId { get; set; }

        public virtual Account? Account { get; set; }
        public virtual JoinAuction? Joinauction { get; set; }
        public virtual ICollection<Payment> Payments { get; set; }
    }
}
