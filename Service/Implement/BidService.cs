using DAL.DTO.BidDTO;
using DAL.Models;
using Microsoft.AspNetCore.SignalR;
using Repository.Interface;
using Service.Hubs;
using Service.Interface;

namespace Service.Implement
{
    public class BidService : IBidService
    {
        private readonly IBidRepository _bidRepository;
        private readonly IJewelryGoldRepository _jewelryGoldRepository;
        private readonly IJewelrySilverRepository _jewelrySilverRepository;
        private readonly IJewelryGoldDiamondRepository _jewelryGoldDiaRepository;
        private readonly IBidRecordRepository _bidRecordRepository;
        private readonly IAuctionRepository _auctionRepository;
        private readonly IHubContext<BiddingHub> _biddingHubContext;
        public BidService(IBidRepository bidRepository, IJewelryGoldRepository jewelryGoldRepository, IJewelrySilverRepository jewelrySilverRepository, IJewelryGoldDiamondRepository jewelryGoldDiaRepository, IBidRecordRepository bidRecordRepository, IHubContext<BiddingHub> hubContext)
        {
            _jewelryGoldRepository = jewelryGoldRepository;
            _jewelrySilverRepository = jewelrySilverRepository;
            _jewelryGoldDiaRepository = jewelryGoldDiaRepository;
            _bidRecordRepository = bidRecordRepository;
            _bidRepository = bidRepository;
            _biddingHubContext = hubContext;
        }
        //public async Task<IEnumerable<Bid>> GetBidByAccountIdAsync(int accountId)
        //{
        //    return await _bidRepository.GetBidByAccountId(accountId);
        //}
        public async Task<Bid> GetBidByBidId(int accountId)
        {
            return await _bidRepository.GetByIdAsync(accountId);
        }

        public async Task<IEnumerable<Bid>> GetAllBids()
        {
            return await _bidRepository.GetAllAsync();
        }
        public async Task<IEnumerable<Bid>> GetBidByAuctionId(int auctionId)
        {
            return await _bidRepository.GetBidByAuctionId(auctionId);
        }
        //public async Task<IEnumerable<Bid>> GetBidRecordByAccountId(int accountId)
        //{
        //    return await _bidRecordRepository.GetBidRecordByAccountId(accountId);
        //}
        public async Task<Bid> GetBidById(int id)
        {
            return await _bidRepository.GetByIdAsync(id);
        }
        public async Task<Bid> CreateBid(CreateBidDTO createBid)
        {
            var newBid = new Bid
            {
                //              AccountId = createBid.AccountId,
                AuctionId = createBid.AuctionId,
                Minprice = createBid.Minprice,
                Maxprice = createBid.Maxprice,
                Datetime = DateTime.Now,
            };
            await _bidRepository.AddAsync(newBid);
            await _bidRepository.SaveChangesAsync();
            return newBid;
        }

        public async Task<Bid> UpdateBid(int id, UpdateBidDTO updateBid)
        {
            var bid = await _bidRepository.GetByIdAsync(id);
            if (bid == null)
            {
                throw new Exception($"Bid with ID{id} not found");
            }
            bid.AuctionId = updateBid.AuctionId;
            //          bid.AccountId = updateBid.AccountId;
            bid.Minprice = updateBid.Minprice;
            bid.Maxprice = updateBid.Maxprice;
            await _bidRepository.UpdateAsync(bid);

            return bid;

        }
        public async Task<bool> PlaceBid(BiddingDTO bidDto)
        {

            var jewelryGold = await _jewelryGoldRepository.GetJewelryGoldByAuctionId(bidDto.AuctionId);
            var jewelryGoldDiamond = await _jewelryGoldDiaRepository.GetJewelryGoldDiamondByAuctionId(bidDto.AuctionId);
            var jewelrySilver = await _jewelrySilverRepository.GetJewelrySilverByAuctionId(bidDto.AuctionId);

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


            var existingBid = await _bidRepository.GetByIdAsync(bidDto.BidId);

            double newMaxPrice;
            if (existingBid == null)
            {

                newMaxPrice = minPrice + bidDto.BidStep;
                var newBid = new Bid
                {
                    AuctionId = bidDto.AuctionId,
                    Minprice = minPrice,
                    Maxprice = minPrice,
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

            await _biddingHubContext.Clients.Group(existingBid.BidId.ToString()).SendAsync("HighestPrice", newMaxPrice).ConfigureAwait(true);

            await _bidRepository.SaveChangesAsync();
            await _bidRecordRepository.SaveChangesAsync();

            return true;
        }




    }
}
