using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Implement
{
    public class AuctionRepository : RepositoryGeneric<Auction>
    {
        public AuctionRepository(JewelryAuctionContext context) : base(context)
        {
            
        }
        public IEnumerable<Auction> GetAllAuctions()
        {
            return _context.Auctions.ToList();
        }
        public async Task<bool> UpdateAuctionAsync(Auction auction)
        {
            try
            {
                _context.Auctions.Update(auction);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
