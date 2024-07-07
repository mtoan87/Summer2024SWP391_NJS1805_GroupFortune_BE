using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interface
{
    public interface IJewelrySilverRepository : IRepositoryGeneric<JewelrySilver>
    {
        IEnumerable<JewelrySilver> GetAll();
        IEnumerable<JewelrySilver> GetUnVerified();
        IEnumerable<JewelrySilver> GetVerified();
        Task<bool> JewelrySilverExistsInAuction(int jewelrySilverId);

        Task<IEnumerable<JewelrySilver>> GetAuctionAndJewelrySilverByAccountId(int accountId);
        Task<JewelrySilver> GetJewelrySilverByAuctionId(int auctionId);
    }
}
