﻿using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interface
{
    public interface IBidRecordService
    {
        Task<IEnumerable<BidRecord>> GetBidRecords();
        Task<IEnumerable<BidRecord>> GetBidRecordByBidId(int BidId);

        Task<IEnumerable<BidRecord>> GetBidRecordByAccountId(int BidId);

        Task<IEnumerable<BidRecord>> GetBidRecordAndBidByAccountId(int AccountId);

        List<BidRecord> GetBidRecordByAccountAndBidId(int accountId, int bidId);
    }
}
