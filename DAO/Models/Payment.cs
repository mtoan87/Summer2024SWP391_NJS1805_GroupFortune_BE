using System;
using System.Collections.Generic;

namespace DAL.Models
{
    public partial class Payment
    {
        public int PaymentId { get; set; }
        public int? AccountId { get; set; }
        public int? JoinauctionId { get; set; }
        public int? JewelryId { get; set; }
        public string Status { get; set; } = null!;
        public string Paymentmethod { get; set; } = null!;
        public DateTime Date { get; set; }
        public double Price { get; set; }
        public double Totalprice { get; set; }
        public double Fee { get; set; }

        public virtual Account? Account { get; set; }
        public virtual Jewelry? Jewelry { get; set; }
        public virtual JoinAuction? Joinauction { get; set; }
    }
}
