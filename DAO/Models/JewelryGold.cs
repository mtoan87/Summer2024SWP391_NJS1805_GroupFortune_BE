using System;
using System.Collections.Generic;

namespace DAL.Models
{
    public partial class JewelryGold
    {
        public JewelryGold()
        {
            Auctions = new HashSet<Auction>();
        }

        public int JewelryGoldId { get; set; }
        public int? AccountId { get; set; }
        public string? JewelryImg { get; set; }
        public string Name { get; set; } = null!;
        public string Category { get; set; } = null!;
        public string Materials { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string GoldAge { get; set; } = null!;
        public double? Price { get; set; }
        public string Weight { get; set; } = null!;
        public string? Shipment { get; set; }
        public string? Status { get; set; }

        public virtual Account? Account { get; set; }
        public virtual ICollection<Auction> Auctions { get; set; }
    }
}
