using DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Implement
{
    public class JewelrySilverRepository : RepositoryGeneric<JewelrySilver>
    {
        public JewelrySilverRepository(JewelryAuctionContext context) : base(context)
        {

        }
        
        public IEnumerable<JewelrySilver> GetAll()
        {
            return _context.Set<JewelrySilver>().ToList();
        }

        public IEnumerable<JewelrySilver> GetUnVerified()
        {
            return _context.JewelrySilvers.Where(a => a.Status == "UnVerified").ToList();
        }
        public IEnumerable<JewelrySilver> GetVerified()
        {
            return _context.JewelrySilvers.Where(a => a.Status == "Verified").ToList();
        }

        public async Task<IEnumerable<JewelrySilver>> GetAuctionAndJewelrySilverByAccountId(int accountId)
        {
            return await _context.JewelrySilvers
                .Include(a => a.Auctions)
                .Where(a => a.AccountId == accountId)
                .ToListAsync();
        }
    }
}
