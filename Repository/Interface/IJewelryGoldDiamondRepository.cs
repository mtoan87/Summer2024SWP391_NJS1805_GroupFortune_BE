using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interface
{
    public interface IJewelryGoldDiamondRepository : IRepositoryGeneric<JewelryGoldDiamond>
    {
        IEnumerable<JewelryGoldDiamond> GetAll();
        IEnumerable<JewelryGoldDiamond> GetUnVerified();
        IEnumerable<JewelryGoldDiamond> GetVerified();
        Task<bool> JewelryGoldDiaExistsInAuction(int jewelrySilverId);
        Task<JewelryGoldDiamond> GetJewelryGoldDiamondByAuctionId(int auctionId);
        
        Task<IEnumerable<JewelryGoldDiamond>> GetAuctionAndJewelryGoldDiamondByAccountId(int accountId);
    }
}
