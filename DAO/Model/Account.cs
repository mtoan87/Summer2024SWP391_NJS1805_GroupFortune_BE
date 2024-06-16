﻿using System;
using System.Collections.Generic;

namespace DAL.Model
{
    public partial class Account
    {
        public Account()
        {
            AccountWallets = new HashSet<AccountWallet>();
            AuctionResults = new HashSet<AuctionResult>();
            Auctions = new HashSet<Auction>();
            Bids = new HashSet<Bid>();
            JewelryGolds = new HashSet<JewelryGold>();
            JewelrySilvers = new HashSet<JewelrySilver>();
            JoinAuctions = new HashSet<JoinAuction>();
            Payments = new HashSet<Payment>();
        }

        public int AccountId { get; set; }
        public string AccountName { get; set; } = null!;
        public string AccountEmail { get; set; } = null!;
        public string AccountPassword { get; set; } = null!;
        public string AccountPhone { get; set; } = null!;
        public int? RoleId { get; set; }

        public virtual Role? Role { get; set; }
        public virtual ICollection<AccountWallet> AccountWallets { get; set; }
        public virtual ICollection<AuctionResult> AuctionResults { get; set; }
        public virtual ICollection<Auction> Auctions { get; set; }
        public virtual ICollection<Bid> Bids { get; set; }
        public virtual ICollection<JewelryGold> JewelryGolds { get; set; }
        public virtual ICollection<JewelrySilver> JewelrySilvers { get; set; }
        public virtual ICollection<JoinAuction> JoinAuctions { get; set; }
        public virtual ICollection<Payment> Payments { get; set; }
    }
}
