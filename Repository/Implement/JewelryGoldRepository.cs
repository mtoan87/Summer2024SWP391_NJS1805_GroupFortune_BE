using DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Implement
{

    public class JewelryGoldRepository : RepositoryGeneric<JewelryGold>
    {
        public JewelryGoldRepository(JewelryAuctionContext context) : base(context)
        {

        }
        public IEnumerable<JewelryGold> GetAllJewelries()
        {
            return _context.JewelryGolds.ToList();
        }
        public IEnumerable<JewelryGold> GetAll()
        {
            return _context.Set<JewelryGold>().ToList();
        }

        public async Task<JewelryGold> GetById(int id)
        {
            return await _context.JewelryGolds.FirstOrDefaultAsync(x =>x.JewelryGoldId == id);
        }

        public async Task<bool> UpdateJewelryAsync(JewelryGold jewelryGold)
        {
            try
            {
                _context.JewelryGolds.Update(jewelryGold);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public async Task<IEnumerable<JewelryGold>> GetAuctionAndJewelryGoldByAccountId(int accountId)
        {
            return await _context.JewelryGolds
                .Include(a => a.Auctions)
                .Where(a => a.AccountId == accountId)
                .ToListAsync();
        }
    }
}
