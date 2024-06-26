using System;
using System.Collections.Generic;

namespace DAL.Models
{
    public partial class JewelrySilver
    {
        public JewelrySilver()
        {
            Auctions = new HashSet<Auction>();
        }

        public int JewelrySilverId { get; set; }
        public int? AccountId { get; set; }
        public string? JewelryImg { get; set; }
        public string Name { get; set; } = null!;
        public string Materials { get; set; } = null!;
        public string Category { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string Purity { get; set; } = null!;
        public double? Price { get; set; }
        public string? Shipment { get; set; }
        public string Weight { get; set; } = null!;
        public string? Status { get; set; }

        public virtual Account? Account { get; set; }
        public virtual ICollection<Auction> Auctions { get; set; }
    }
}
