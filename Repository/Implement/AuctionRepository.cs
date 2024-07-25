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

        public async Task<bool> ProcessAuctionResultAsync(AuctionResult auctionResult)
        {
            // Ensure the auction result has a status of "Win"
            if (auctionResult.Status != "Win")
                return false;

            // Ensure auctionResult and its related properties are not null
            if (auctionResult == null || auctionResult.Joinauction == null)
                throw new ArgumentNullException(nameof(auctionResult), "AuctionResult or its related Joinauction is null.");

            // Find the winning account's wallet
            var winningAccountWallet = await _context.AccountWallets
                .FirstOrDefaultAsync(aw => aw.AccountId == auctionResult.AccountId);

            if (winningAccountWallet == null)
                throw new InvalidOperationException("Winning account's wallet not found.");

            // Find the auction associated with the result
            var auction = await _context.Auctions
                .FirstOrDefaultAsync(a => a.AuctionId == auctionResult.Joinauction.AuctionId);

            if (auction == null)
                throw new InvalidOperationException("Auction associated with the auction result not found.");

            // Find the auction owner's wallet
            var auctionOwnerWallet = await _context.AccountWallets
                .FirstOrDefaultAsync(aw => aw.AccountId == auction.AccountId);

            if (auctionOwnerWallet == null)
                throw new InvalidOperationException("Auction owner's wallet not found.");

            // Ensure the winning account has sufficient budget
            if (winningAccountWallet.Budget < auctionResult.Price)
                throw new InvalidOperationException("Winning account does not have sufficient budget.");

            // Deduct the price from the winning account's budget
            winningAccountWallet.Budget -= auctionResult.Price;

            // Add the price to the auction owner's budget
            auctionOwnerWallet.Budget += auctionResult.Price;

            // Save the changes
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
