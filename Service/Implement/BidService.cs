using DAL.Models;
using Repository.Implement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Implement
{
    public class BidService
    {
        private readonly BidRepository _bidRepository;
        public BidService(BidRepository bidRepository)
        {
            _bidRepository = bidRepository;
        }
        public async Task<IEnumerable<Bid>> GetBidByAccountIdAsync(int accountId)
        {
            return await _bidRepository.GetBidByAccountId(accountId);
        }

        public IEnumerable<Bid> GetAllBids()
        {
            return _bidRepository.GetAllBids();
        }
    }
}
