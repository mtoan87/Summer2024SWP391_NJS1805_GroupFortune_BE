using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Implement
{
    public class AuctionResultRepository : RepositoryGeneric<AuctionResult>
    {
        public AuctionResultRepository(JewelryAuctionContext context) : base(context)
        {
            
        }
        public IEnumerable<AuctionResult> GetAllAuctionResults()
        {
            return _context.AuctionResults.ToList();
        }
        public async Task<bool> UpdateAuctionRsAsync(AuctionResult auctionrs)
        {
            try
            {
                _context.AuctionResults.Update(auctionrs);
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
