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
        public async Task<IEnumerable<Bid>> GetBidByAccountId(int accountId)
        {
            return await _context.Bids
                .Where(a => a.AccountId == accountId)
                .ToListAsync();
        }
        public async Task<Bid> GetByAccountIdAndAuctionId(int accountId, int auctionId)
        {
            return  _context.Bids.FirstOrDefault(b => b.AccountId == accountId && b.AuctionId == auctionId);
        }

        public async Task<IEnumerable<Bid>> GetBidRecordByAccountId(int accountId)
        {
            return await _context.Bids
                .Include(a => a.BidRecords)
                .Where(a => a.AccountId == accountId)
                .ToListAsync();
        }
    }
}
