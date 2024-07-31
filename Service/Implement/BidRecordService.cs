using DAL.Models;
using Repository.Implement;
using Repository.Interface;
using Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Implement
{
    public class BidRecordService : IBidRecordService
    {
        private readonly IBidRecordRepository _repository;
        public BidRecordService(IBidRecordRepository bidRecordRepository)
        {
            _repository = bidRecordRepository;
        }

        public async Task<IEnumerable<BidRecord>> GetBidRecords()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<IEnumerable<BidRecord>> GetBidRecordByBidId(int BidId)
        {
            var bidRecords = await _repository.GetBidRecordByBidId(BidId);
            bidRecords = bidRecords.OrderByDescending(b => b.BidAmount).ToList();
            return bidRecords;
        }

        public async Task<IEnumerable<BidRecord>> GetBidRecordByAccountId(int AccountId)
        {
            return await _repository.GetBidByAccountId(AccountId);
        }

        public async Task<IEnumerable<BidRecord>> GetBidRecordAndBidByAccountId(int AccountId)
        {
            return await _repository.GetBidAndBidRecorsByAccountId(AccountId);
        }

        public List<BidRecord> GetBidRecordByAccountAndBidId(int accountId, int bidId)
        {
           return _repository.GetBidRecordByAccountAndBidId(accountId, bidId);
        }
    }
}
