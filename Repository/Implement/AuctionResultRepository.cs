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
    public class AuctionResultRepository : RepositoryGeneric<AuctionResult> , IAuctionResultRepository
    {
        public AuctionResultRepository(JewelryAuctionContext context) : base(context)
        {
            
        }

        public async Task<IEnumerable<AuctionResult>> GetResultsByJoinauctionIdAsync(int joinAuctionId)
        {
            return await _context.AuctionResults
                .Where(a => a.AccountId == joinAuctionId)
                .ToListAsync();
        }
        
    }
}
