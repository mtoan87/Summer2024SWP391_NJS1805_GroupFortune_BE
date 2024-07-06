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

    public class JewelryGoldRepository : RepositoryGeneric<JewelryGold> , IJewelryGoldRepository
    {
        public JewelryGoldRepository(JewelryAuctionContext context) : base(context)
        {

        }
        
        public IEnumerable<JewelryGold> GetAll()
        {
            return _context.Set<JewelryGold>().ToList();
        }

        public async Task<JewelryGold> GetById(int id)
        {
            return await _context.JewelryGolds.FirstOrDefaultAsync(x =>x.JewelryGoldId == id);
        }

        public IEnumerable<JewelryGold> GetUnVerified()
        {
            return _context.JewelryGolds.Where(a =>a.Status == "UnVerified").ToList();
        }
        public IEnumerable<JewelryGold> GetVerified()
        {
            return _context.JewelryGolds.Where(a => a.Status == "Verified").ToList();
        }

        public async Task<bool> JewelryGoldExistsInAuction(int jewelryGoldId)
        {
            return await _dbSet.AnyAsync(a => a.JewelryGoldId == jewelryGoldId);
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
