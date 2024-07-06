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
    public class JewelryGoldDiaRepository : RepositoryGeneric<JewelryGoldDiamond> , IJewelryGoldDiamondRepository
    {
        public JewelryGoldDiaRepository(JewelryAuctionContext context) : base(context)
        {
            
        }
        
        public IEnumerable<JewelryGoldDiamond> GetAll()
        {
            return _context.Set<JewelryGoldDiamond>().ToList();
        }
        public IEnumerable<JewelryGoldDiamond> GetUnVerified()
        {
            return _context.JewelryGoldDiamonds.Where(a => a.Status == "UnVerified").ToList();
        }
        public IEnumerable<JewelryGoldDiamond> GetVerified()
        {
            return _context.JewelryGoldDiamonds.Where(a => a.Status == "Verified").ToList();
        }
        public async Task<bool> JewelryGoldDiaExistsInAuction(int jewelryGoldDiaId)
        {
            return await _dbSet.AnyAsync(a => a.JewelryGolddiaId == jewelryGoldDiaId);
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
