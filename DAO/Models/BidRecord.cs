using System;
using System.Collections.Generic;

namespace DAL.Models
{
    public partial class BidRecord
    {
        public int BidRecordId { get; set; }
        public int? BidId { get; set; }
        public double? BidAmount { get; set; }
        public double? BidStep { get; set; }

        public virtual Bid? Bid { get; set; }
    }
}
