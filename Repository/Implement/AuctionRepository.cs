using DAL.Models;
using Microsoft.EntityFrameworkCore;
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
        public IEnumerable<Auction> GetActiveAuctions()
        {
            return _context.Auctions.Where(a => a.Status == "Active").ToList();
        }
        public IEnumerable<Auction> GetUnActiveAuctions()
        {
            return _context.Auctions.Where(a => a.Status == "UnActive").ToList();
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
        public async Task<IEnumerable<Auction>> GetAuctionAndJewelrySilverByAccountId(int accountId)
        {
            return await _context.Auctions
                .Include(a => a.JewelrySilver)
                .Where(a => a.AccountId == accountId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Auction>> GetAuctionAndJewelryGoldByAccountId(int accountId)
        {
            return await _context.Auctions
                .Include(a => a.JewelryGold)              
                .Where(a => a.AccountId == accountId)
                .ToListAsync();
        }
        public async Task<IEnumerable<Auction>> GetAuctionAndJewelryGoldDiamondByAccountId(int accountId)
        {
            return await _context.Auctions
                .Include(a => a.JewelryGolddia)
                .Where(a => a.AccountId == accountId)
                .ToListAsync();
        }
        public int GetAccountCountInAuction(int auctionId)
        {
            return _context.JoinAuctions
                           .Where(ja => ja.AuctionId == auctionId)
                           .Select(ja => ja.AccountId)
                           .Distinct()
                           .Count();
        }
        public IEnumerable<Auction> GetJewelryActiveAuctions()
        {
            return   _context.Auctions
                .Where(a => a.Status == "Active")
                .Include(a => a.JewelryGold)
                .Include(a => a.JewelrySilver)
                .Include(a => a.JewelryGolddia)
                .ToList();
        }
    }
}
