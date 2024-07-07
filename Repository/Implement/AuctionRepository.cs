using DAL.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Interface;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Repository.Implement
{
    public class AuctionRepository : RepositoryGeneric<Auction> , IAuctionRepository
    {
        public AuctionRepository(JewelryAuctionContext context) : base(context)
        {
        }

        public IEnumerable<Auction> GetActiveAuctions()
        {
            return _context.Auctions.Where(a => a.Status == "Active").ToList();
        }

        public IEnumerable<Auction> GetUnActiveAuctions()
        {
            return _context.Auctions.Where(a => a.Status == "UnActive").ToList();
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
            return _context.Auctions
                .Where(a => a.Status == "Active")
                .Include(a => a.JewelryGold)
                .Include(a => a.JewelrySilver)
                .Include(a => a.JewelryGolddia)
                .ToList();
        }

        public List<JewelrySilver> GetJewelrySilversByAuctionId(int auctionId)
        {
            
            var jewelrySilvers = _context.JewelrySilvers
                .Include(j => j.Account) 
                .Include(j => j.Auctions)
                .Where(j => j.Auctions.Any(a => a.AuctionId == auctionId))
                .ToList();

            return jewelrySilvers;
        }
        public List<JewelryGold> GetJewelryGoldsByAuctionId(int auctionId)
        {
            // Query the JewelrySilver entities that are associated with the AuctionId
            var jewelryGolds = _context.JewelryGolds
                .Include(j => j.Account) // Include related Account if needed
                .Include(j => j.Auctions)
                .Where(j => j.Auctions.Any(a => a.AuctionId == auctionId))
                .ToList();

            return jewelryGolds;
        }
        public List<JewelryGoldDiamond> GetJewelryGoldDiamondsByAuctionId(int auctionId)
        {
            // Query the JewelrySilver entities that are associated with the AuctionId
            var jewelryGoldDiamonds = _context.JewelryGoldDiamonds
                .Include(j => j.Account) // Include related Account if needed
                .Include(j => j.Auctions)
                .Where(j => j.Auctions.Any(a => a.AuctionId == auctionId))
                .ToList();

            return jewelryGoldDiamonds;
        }

        public bool IsJewelryInAuctionGold(int? jewelryGoldId)
        {
            return _context.Auctions.Any(a => a.JewelryGoldId == jewelryGoldId);
        }

        public bool IsJewelryInAuctionSilver(int? jewelrySilverId)
        {
            return _context.Auctions.Any(a => a.JewelrySilverId == jewelrySilverId);
        }

        public bool IsJewelryInAuctionGoldDiamond(int? jewelryGoldDiaId)
        {
            return _context.Auctions.Any(a => a.JewelryGolddiaId == jewelryGoldDiaId);
        }
    }
}
