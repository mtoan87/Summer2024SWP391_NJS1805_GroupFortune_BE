using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interface
{
    public interface IJoinAuctionRepository : IRepositoryGeneric<JoinAuction>
    {
        Task<IEnumerable<JoinAuction>> GetAuctionByJoinauctionIdAsync(int joinAuctionId);
    }
}
