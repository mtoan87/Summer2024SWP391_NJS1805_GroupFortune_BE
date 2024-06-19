using DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Implement
{
    public class JewelryGoldDiaRepository : RepositoryGeneric<JewelryGoldDiamond>
    {
        public JewelryGoldDiaRepository(JewelryAuctionContext context) : base(context)
        {
            
        }
        public IEnumerable<JewelryGoldDiamond> GetAllJewelries()
        {
            return _context.JewelryGoldDiamonds.ToList();
        }
        public IEnumerable<JewelryGoldDiamond> GetAll()
        {
            return _context.Set<JewelryGoldDiamond>().ToList();
        }
        public async Task<bool> UpdateJewelryAsync(JewelryGoldDiamond jewelryGoldDiamond)
        {
            try
            {
                _context.JewelryGoldDiamonds.Update(jewelryGoldDiamond);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public async Task<IEnumerable<JewelryGoldDiamond>> GetAuctionAndJewelryGoldDiamondByAccountId(int accountId)
        {
            return await _context.JewelryGoldDiamonds
                .Include(a => a.Auctions)
                .Where(a => a.AccountId == accountId)
                .ToListAsync();
        }
    }
}
