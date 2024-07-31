using DAL.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Implement
{
    public class JoinAuctionRepository : RepositoryGeneric<JoinAuction> , IJoinAuctionRepository
    {
        public JoinAuctionRepository(JewelryAuctionContext context) : base(context)
        {
            
        }
        public async Task<IEnumerable<JoinAuction>> GetAuctionByJoinauctionIdAsync(int joinAuctionId)
        {
            var result = await _context.JoinAuctions
                .Where(j => j.Id == joinAuctionId)
                .Select(j => new
                {
                    JoinAuction = j,
                    Auction = new
                    {
                        j.Auction.AuctionId,
                        j.Auction.AccountId // Select explicitly
                    }
                })
                .ToListAsync();

            // If needed, map the result to your desired return type
            return result.Select(r => r.JoinAuction);
        }
        public async Task<IEnumerable<JoinAuction>> GetJoinAuctionByAccountIdAsync(int accountId)
        {
            return await _context.JoinAuctions
                .Where(ar => ar.AccountId == accountId)
                .ToListAsync();
        }
    }
}
