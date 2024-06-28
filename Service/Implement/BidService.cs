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
        private readonly JewelryGoldRepository _jewelryGoldRepository;
        private readonly JewelrySilverRepository _jewelrySilverRepository;
        private readonly JewelryGoldDiaRepository _jewelryGoldDiaRepository;
        private readonly BidRecordRepository _bidRecordRepository;
        public BidService(BidRepository bidRepository, JewelryGoldRepository jewelryGoldRepository, JewelrySilverRepository jewelrySilverRepository, JewelryGoldDiaRepository jewelryGoldDiaRepository, BidRecordRepository bidRecordRepository)
        {
            _jewelryGoldRepository = jewelryGoldRepository;
            _jewelrySilverRepository = jewelrySilverRepository;
            _jewelryGoldDiaRepository = jewelryGoldDiaRepository;
            _bidRecordRepository = bidRecordRepository;
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

        public async Task<IEnumerable<Bid>> GetBidRecordByAccountId(int accountId)
        {
            return await _bidRepository.GetBidRecordByAccountId(accountId);
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
        public async Task<bool> PlaceBid(BiddingDTO bidDto)
        {
            var jewelryGold = await _jewelryGoldRepository.GetByIdAsync(bidDto.AuctionId);
            var jewelryGoldDiamond = await _jewelryGoldDiaRepository.GetByIdAsync(bidDto.AuctionId);
            var jewelrySilver = await _jewelrySilverRepository.GetByIdAsync(bidDto.AuctionId);

            double minPrice = 0;
            if (jewelryGold != null)
            {
                minPrice = jewelryGold.Price ?? 0;
            }
            else if (jewelryGoldDiamond != null)
            {
                minPrice = jewelryGoldDiamond.Price ?? 0;
            }
            else if (jewelrySilver != null)
            {
                minPrice = jewelrySilver.Price ?? 0;
            }
            else
            {
                return false;
            }

            var existingBid = await _bidRepository.GetByAccountIdAndAuctionId(bidDto.AccountId, bidDto.AuctionId);

            double newMaxPrice;
            if (existingBid == null)
            {
                newMaxPrice = minPrice + bidDto.BidStep;
                var newBid = new Bid
                {
                    AccountId = bidDto.AccountId,
                    AuctionId = bidDto.AuctionId,
                    Minprice = minPrice,
                    Maxprice = minPrice,  // Ensuring Maxprice is initialized to Minprice
                    Datetime = DateTime.Now
                };

                await _bidRepository.AddAsync(newBid);
                await _bidRepository.SaveChangesAsync();

                var bidRecord = new BidRecord
                {
                    BidId = newBid.BidId,
                    BidAmount = newMaxPrice,
                    BidStep = bidDto.BidStep
                };

                await _bidRecordRepository.AddAsync(bidRecord);
            }
            else
            {
                newMaxPrice = existingBid.Maxprice + bidDto.BidStep;
                existingBid.Maxprice = newMaxPrice;
                existingBid.Datetime = DateTime.Now;

                var bidRecord = new BidRecord
                {
                    BidId = existingBid.BidId,
                    BidAmount = newMaxPrice,
                    BidStep = bidDto.BidStep
                };

                await _bidRecordRepository.AddAsync(bidRecord);
            }

            await _bidRepository.SaveChangesAsync();
            await _bidRecordRepository.SaveChangesAsync();

            return true;
        }

    }
}
