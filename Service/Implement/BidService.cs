// Make sure to install RabbitMQ.Client NuGet package
// Install-Package RabbitMQ.Client

using DAL.DTO.BidDTO;
using DAL.Models;
using RabbitMQ.Client;
using Repository.Implement;
using System;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Service.Implement
{
    public class BidService : IDisposable
    {
        private readonly IModel _channel;
        private const string ExchangeName = "bids";
        private readonly BidRepository _bidRepository;
        private readonly JewelryGoldRepository _jewelryGoldRepository;
        private readonly JewelrySilverRepository _jewelrySilverRepository;
        private readonly JewelryGoldDiaRepository _jewelryGoldDiaRepository;
        private readonly BidRecordRepository _bidRecordRepository;

        public BidService(BidRepository bidRepository, JewelryGoldRepository jewelryGoldRepository, JewelrySilverRepository jewelrySilverRepository, JewelryGoldDiaRepository jewelryGoldDiaRepository, BidRecordRepository bidRecordRepository)
        {
            _bidRepository = bidRepository ?? throw new ArgumentNullException(nameof(bidRepository));
            _jewelryGoldRepository = jewelryGoldRepository ?? throw new ArgumentNullException(nameof(jewelryGoldRepository));
            _jewelrySilverRepository = jewelrySilverRepository ?? throw new ArgumentNullException(nameof(jewelrySilverRepository));
            _jewelryGoldDiaRepository = jewelryGoldDiaRepository ?? throw new ArgumentNullException(nameof(jewelryGoldDiaRepository));
            _bidRecordRepository = bidRecordRepository ?? throw new ArgumentNullException(nameof(bidRecordRepository));

            var factory = new ConnectionFactory() { HostName = "localhost" };
            var connection = factory.CreateConnection();
            _channel = connection.CreateModel();

            _channel.ExchangeDeclare(ExchangeName, ExchangeType.Fanout);
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
                Maxprice = createBid.Minprice,
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
                throw new Exception($"Bid with ID {id} not found");
            }
            bid.AuctionId = updateBid.AuctionId;
            bid.AccountId = updateBid.AccountId;
            bid.Minprice = updateBid.Minprice;
            bid.Maxprice = updateBid.Maxprice;
            await _bidRepository.UpdateAsync(bid);
            await _bidRepository.SaveChangesAsync();

            return bid;
        }

        public async Task<bool> PlaceBid(BiddingDTO bidDto)
        {
           
            var newMaxPrice = await CalculateNewMaxPrice(bidDto);

            
            var existingBid = await _bidRepository.GetByAccountIdAndAuctionId(bidDto.AccountId, bidDto.AuctionId);

            if (existingBid == null)
            {
                var minPrice = newMaxPrice - bidDto.BidStep;
                var newBid = new Bid
                {
                    AccountId = bidDto.AccountId,
                    AuctionId = bidDto.AuctionId,
                    Minprice = minPrice,
                    Maxprice = newMaxPrice,
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
                await _bidRepository.SaveChangesAsync();
            }

            
            var bidUpdate = new
            {
                AuctionId = bidDto.AuctionId,
                NewMaxPrice = newMaxPrice
            };

            var message = JsonSerializer.Serialize(bidUpdate);
            var body = Encoding.UTF8.GetBytes(message);

            _channel.BasicPublish(exchange: ExchangeName,
                                  routingKey: "",
                                  basicProperties: null,
                                  body: body);

            return true;
        }

        private async Task<double> CalculateNewMaxPrice(BiddingDTO bidDto)
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
                throw new Exception($"Auction with ID {bidDto.AuctionId} not found");
            }

            var existingBid = await _bidRepository.GetByAccountIdAndAuctionId(bidDto.AccountId, bidDto.AuctionId);

            double newMaxPrice;
            if (existingBid == null)
            {
                newMaxPrice = minPrice + bidDto.BidStep;
            }
            else
            {
                newMaxPrice = existingBid.Maxprice + bidDto.BidStep;
            }

            return newMaxPrice;
        }

        public void Dispose()
        {
            _channel.Close();
            _channel.Dispose();
        }
    }
}
