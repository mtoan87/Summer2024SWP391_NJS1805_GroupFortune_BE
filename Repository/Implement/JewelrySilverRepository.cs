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
        public IEnumerable<JewelrySilver> GetAllJewelries()
        {
            return _context.JewelrySilvers.ToList();
        }
        public async Task<bool> UpdateJewelryAsync(JewelrySilver jewelrySilver)
        {
            try
            {
                _context.JewelrySilvers.Update(jewelrySilver);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
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
