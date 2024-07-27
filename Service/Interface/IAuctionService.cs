using DAL.DTO.AuctionDTO;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interface
{
    public interface IAuctionService
    {
        Task<IEnumerable<Auction>> GetAllAuctions();
        IEnumerable<Auction> GetAllActiveAuctions();
        IEnumerable<Auction> GetAllUnActiveAuctions();
        Task<IEnumerable<Auction>> GetAuctionByAccountId(int accountId);
        Task<Auction> GetAuctionById(int id);
        Task<Auction> CreateJewelrySilverAuction(CreateSilverAuctionDTO createAuction);
        Task<Auction> CreateJewelryGoldAuction(CreateGoldAuctionDTO createAuction);
        Task<Auction> CreateJewelryGoldDiamondAuction(CreateGoldDiamondAuctionDTO createAuction);
        Task<Auction> UpdateGoldAuction(int id, UpdateGoldAuctionDTO updateAuction);
        Task<Auction> UpdateSilverAuction(int id, UpdateSilverAuctionDTO updateAuction);
        Task<Auction> UpdateGoldDiamondAuction(int id, UpdateGoldDiamondAuctionDTO updateAuction);
        Task<Auction> DeleteAuction(int id);
        Task<IEnumerable<Auction>> GetAuctionAndJewelryGoldByAccountIdAsync(int accountId);
        Task<IEnumerable<Auction>> GetAuctionAndJewelryGoldDiamondByAccountIdAsync(int accountId);
        Task<IEnumerable<Auction>> GetAuctionAndJewelrySilverByAccountIdAsync(int accountId);
        int GetAccountCountInAuction(int auctionId);
        IEnumerable<Auction> GetJewelryActiveAuctions();
        IEnumerable<object> GetAuctionsWithRemainingTime();
        Task BroadcastRemainingTimeUpdates();
    }
}
