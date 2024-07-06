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
            return await _repository.GetBidRecordByBidId(BidId);
        }
    }
}
