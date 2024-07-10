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
    public class BidRepository : RepositoryGeneric<Bid> , IBidRepository
    {
        public BidRepository(JewelryAuctionContext context) : base(context)
        {

            

        }
        public async Task<IEnumerable<Bid>> GetBidByAuctionId(int auctionId)
        {
            return await _context.Bids             
                .Where(a => a.AuctionId == auctionId)
                .ToListAsync();
        }

        

    }
}
