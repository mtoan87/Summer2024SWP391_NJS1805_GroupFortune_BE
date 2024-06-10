using DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Implement
{
    public class BidRepository : RepositoryGeneric<Bid>
    {
        public BidRepository(JewelryAuctionContext context) : base(context)
        {



        }
        public IEnumerable<Bid> GetAllBids()
        {
            return _context.Bids.ToList();
        }
        public async Task<IEnumerable<Bid>> GetBidByAccountId(int accountId)
        {
            return await _context.Bids
                .Where(a => a.AccountId == accountId)
                .ToListAsync();
        }
    }
}
