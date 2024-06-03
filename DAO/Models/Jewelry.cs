using System;
using System.Collections.Generic;

namespace DAL.Models
{
    public partial class Jewelry
    {
        public Jewelry()
        {
            Auctions = new HashSet<Auction>();
            Payments = new HashSet<Payment>();
        }

        public int JewelryId { get; set; }
        public int? AccountId { get; set; }
        public string? JewelryImg { get; set; }
        public string Name { get; set; } = null!;
        public string Materials { get; set; } = null!;
        public string Description { get; set; } = null!;
        public int Price { get; set; }
        public string Weight { get; set; } = null!;
        public string? Goldage { get; set; }
        public string? Collection { get; set; }

        public virtual Account? Account { get; set; }
        public virtual ICollection<Auction> Auctions { get; set; }
        public virtual ICollection<Payment> Payments { get; set; }
    }
}
