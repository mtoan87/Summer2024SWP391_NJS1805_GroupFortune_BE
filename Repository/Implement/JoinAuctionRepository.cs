﻿using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Implement
{
    public class JoinAuctionRepository : RepositoryGeneric<JoinAuction>
    {
        public JoinAuctionRepository(JewelryAuctionContext context) : base(context)
        {
            
        }
        public IEnumerable<JoinAuction> GetAllJoinAuction()
        {
            return _context.JoinAuctions.ToList();
        }
    }
}
