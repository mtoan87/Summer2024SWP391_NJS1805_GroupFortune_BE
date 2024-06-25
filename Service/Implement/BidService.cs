using DAL.DTO.AuctionDTO;
using DAL.DTO.AuctionResultDTO;
using DAL.DTO.BidDTO;
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

        public async Task<IEnumerable<Bid>> GetAllBids()
        {
            return await _bidRepository.GetAllAsync();
        }

        public async Task<Bid> CreateBid(CreateBidDTO createBid)
        {
            var newBid = new Bid
            {
                AccountId = createBid.AccountId,
                AuctionId = createBid.AuctionId,
                Minprice = createBid.Minprice,
                Maxprice = createBid.Maxprice,
                Datetime = DateTime.Now,
            };
            await _bidRepository.AddAsync(newBid);
            await _bidRepository.SaveChangesAsync();
            return newBid;
        }

        public async Task<Bid> UpdateBid(int id,UpdateBidDTO updateBid)
        {
            var bid = await _bidRepository.GetByIdAsync(id);
            if(bid == null)
            {
                throw new Exception($"Bid with ID{id} not found");
            }
            bid.AuctionId = updateBid.AuctionId;
            bid.AccountId = updateBid.AccountId;
            bid.Minprice = updateBid.Minprice;
            bid.Maxprice = updateBid.Maxprice;
            await _bidRepository.UpdateAsync(bid);
           
            return bid;

        }
    }
}
