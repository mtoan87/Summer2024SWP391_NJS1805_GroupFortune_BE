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
            return _context.Auctions.Where(a => a.Status == "Active" ).ToList();
        }

        public IEnumerable<Auction> GetUnActiveAuctions()
        {
            return _context.Auctions.Where(a => a.Status == "UnActive").ToList();
        }

        public async Task<IEnumerable<Auction>> GetAuctionByAccountId(int accountId)
        {
            return await _context.Auctions              
                .Where(a => a.AccountId == accountId)
                .ToListAsync();
        }
        public async Task<IEnumerable<Auction>> GetAuctionAndJoinAuctionByAccountId(int accountId)
        {
            return await _context.Auctions
                .Include(a => a.JoinAuctions)
                .Where(a => a.AccountId == accountId)             
                .ToListAsync();
        }
        public async Task<IEnumerable<Auction>> GetAuctionByJoinAuctionId(int accountId)
        {
            return await _context.Auctions
                .Where(a => a.AccountId == accountId)
                .ToListAsync();
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
            return _context.Auctions.Any(a => a.JewelryGoldId == jewelryGoldId && a.Status != "Failed");
        }

        public bool IsJewelryInAuctionSilver(int? jewelrySilverId)
        {
            return _context.Auctions.Any(a => a.JewelrySilverId == jewelrySilverId && a.Status != "Failed");
        }

        public bool IsJewelryInAuctionGoldDiamond(int? jewelryGoldDiaId)
        {
            return _context.Auctions.Any(a => a.JewelryGolddiaId == jewelryGoldDiaId && a.Status != "Failed");
        }

        public bool ProcessAuctionResult(AuctionResult auctionResult)
        {
            // Ensure the auction result has a status of "Win"
            if (auctionResult.Status != "Win")
                return false;

            // Find the winning account's wallet
            var winningAccountWallet = _context.AccountWallets
                .FirstOrDefault(aw => aw.AccountId == auctionResult.AccountId);

            if (winningAccountWallet == null)
                return false;

            // Find the auction associated with the result
            var auction = _context.Auctions
                .FirstOrDefault(a => a.AuctionId == auctionResult.Joinauction.AuctionId);

            if (auction == null)
                return false;

            // Find the auction owner's wallet
            var auctionOwnerWallet = _context.AccountWallets
                .FirstOrDefault(aw => aw.AccountId == auction.AccountId);

            if (auctionOwnerWallet == null)
                return false;

            // Ensure the winning account has sufficient budget
            if (winningAccountWallet.Budget < auctionResult.Price)
                return false;

            // Deduct the price from the winning account's budget
            winningAccountWallet.Budget -= auctionResult.Price;

            // Add the price to the auction owner's budget
            auctionOwnerWallet.Budget += auctionResult.Price;

            // Save the changes
            _context.SaveChanges();

            return true;
        }
    }
}
