using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interface
{
    public interface IBidRecordRepository : IRepositoryGeneric<BidRecord>
    {
        Task<IEnumerable<BidRecord>> GetBidRecordByBidId(int bidId);
        Task<IEnumerable<BidRecord>> GetBidByAccountId(int accountId);
        Task<IEnumerable<BidRecord>> GetBidAndBidRecorsByAccountId(int accountId);
        List<BidRecord> GetBidRecordByAccountAndBidId(int accountId, int bidId);
    }
}
