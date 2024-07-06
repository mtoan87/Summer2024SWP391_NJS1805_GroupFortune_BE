using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interface
{
    public interface IJewelryGoldRepository : IRepositoryGeneric<JewelryGold>
    {
        IEnumerable<JewelryGold> GetAll();
        IEnumerable<JewelryGold> GetUnVerified();
        IEnumerable<JewelryGold> GetVerified();
        Task<bool> JewelryGoldExistsInAuction(int jewelrySilverId);

        Task<IEnumerable<JewelryGold>> GetAuctionAndJewelryGoldByAccountId(int accountId);
    }
}
