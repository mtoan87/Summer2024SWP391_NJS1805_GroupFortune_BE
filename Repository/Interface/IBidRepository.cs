using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interface
{
    public interface IBidRepository : IRepositoryGeneric<Bid>
    {
        Task<IEnumerable<Bid>> GetBidByAccountId(int accountId);
        Task<Bid> GetByAccountIdAndAuctionId(int accountId, int auctionId);
        Task<IEnumerable<Bid>> GetBidRecordByAccountId(int accountId);
    }
}
