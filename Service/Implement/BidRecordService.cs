using DAL.Models;
using Repository.Implement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Implement
{
    public class BidRecordService
    {
        private readonly BidRecordRepository _repository;
        public BidRecordService(BidRecordRepository bidRecordRepository)
        {
            _repository = bidRecordRepository;
        }

        public async Task<IEnumerable<BidRecord>> GetBidRecords()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<IEnumerable<BidRecord>> GetBidRecordByBidId(int BidId)
        {
            return await _repository.GetBidRecordByBidId(BidId);
        }
    }
}
