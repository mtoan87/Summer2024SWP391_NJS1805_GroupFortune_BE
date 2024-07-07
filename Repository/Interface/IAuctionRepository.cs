using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interface
{
    public interface IAuctionRepository : IRepositoryGeneric<Auction>
    {
        IEnumerable<Auction> GetActiveAuctions();
        IEnumerable<Auction> GetUnActiveAuctions();
        Task<IEnumerable<Auction>> GetAuctionAndJewelrySilverByAccountId(int accountId);
        Task<IEnumerable<Auction>> GetAuctionAndJewelryGoldByAccountId(int accountId);
        Task<IEnumerable<Auction>> GetAuctionAndJewelryGoldDiamondByAccountId(int accountId);
        List<JewelryGold> GetJewelryGoldsByAuctionId(int auctionId);
        List<JewelrySilver> GetJewelrySilversByAuctionId(int auctionId);
        List<JewelryGoldDiamond> GetJewelryGoldDiamondsByAuctionId(int auctionId);
        int GetAccountCountInAuction(int auctionId);
        IEnumerable<Auction> GetJewelryActiveAuctions();
        bool IsJewelryInAuctionGold(int? jewelryGoldId);
        bool IsJewelryInAuctionSilver(int? jewelrySilverId);
        bool IsJewelryInAuctionGoldDiamond(int? jewelryGoldDiaId);
    }
}
