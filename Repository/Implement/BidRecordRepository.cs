using DAL.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Implement;
using Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Implement
{
    public class BidRecordRepository : RepositoryGeneric<BidRecord> , IBidRecordRepository
    {
        public BidRecordRepository(JewelryAuctionContext context): base(context)
        {
            
        }

        public async Task<IEnumerable<BidRecord>> GetBidRecordByBidId(int bidId)
        {
            return await _context.BidRecords
                
                .Where(a => a.BidId == bidId)
                .ToListAsync();
        }

        public async Task<IEnumerable<BidRecord>> GetBidByAccountId(int accountId)
        {
            return await _context.BidRecords
                .Where(a => a.AccountId == accountId)
                .ToListAsync();
        }


    }
}
