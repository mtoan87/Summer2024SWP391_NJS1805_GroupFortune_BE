using DAL.DTO.AuctionDTO;
using DAL.Enums;
using DAL.Models;
using Repository.Implement;
using Repository.Interface;
using Service.Interface;
using System;
using System.Collections.Generic;
using System.Transactions;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using DAL.DTO.BidDTO;
using Microsoft.AspNetCore.SignalR;
using Service.Hubs;

namespace Service.Implement
{
    public class AuctionService : IAuctionService
    {
        private readonly IAuctionRepository _auctionRepository;
        private readonly IJewelryGoldRepository _jewelryGoldRepository;
        private readonly IJewelryGoldDiamondRepository _jewelryDiaRepository;
        private readonly IJewelrySilverRepository _jewelrySilverRepository;
        private readonly IBidRepository _bidRepository;
        private readonly ILogger<AuctionService> _logger;
        private readonly IHubContext<AuctionHub> _hubContext;

        public AuctionService(
            IAuctionRepository auctionRepository,
            IJewelryGoldRepository jewelryGoldRepository,
            IJewelryGoldDiamondRepository jewelryGoldDiaRepository,
            IJewelrySilverRepository jewelrySilverRepository,
            IBidRepository bidRepository,
            ILogger<AuctionService> logger,
            IHubContext<AuctionHub> hubContext)
        {
            _auctionRepository = auctionRepository;
            _jewelryDiaRepository = jewelryGoldDiaRepository;
            _jewelryGoldRepository = jewelryGoldRepository;
            _jewelrySilverRepository = jewelrySilverRepository;
            _bidRepository = bidRepository;
            _logger = logger;
            _hubContext = hubContext;
        }

        public async Task<IEnumerable<Auction>> GetAllAuctions()
        {
            return await _auctionRepository.GetAllAsync();
        }

        public async Task<IEnumerable<Auction>> GetAuctionByAccountId(int accountId)
        {
            return await _auctionRepository.GetAuctionByAccountId(accountId);
        }

        public IEnumerable<Auction> GetAllActiveAuctions()
        {
            return _auctionRepository.GetActiveAuctions();
        }

        public IEnumerable<Auction> GetAllUnActiveAuctions()
        {
            return _auctionRepository.GetUnActiveAuctions();
        }

        public async Task<Auction> GetAuctionById(int id)
        {
            return await _auctionRepository.GetByIdAsync(id);
        }

        public async Task<Auction> CreateJewelrySilverAuction(CreateSilverAuctionDTO createAuction)
        {
            if (_auctionRepository.IsJewelryInAuctionSilver(createAuction.JewelrySilverId))
            {
                throw new Exception("The jewelry item is already in an auction.");
            }

            var jewelrySilver = await _jewelrySilverRepository.GetByIdAsync(createAuction.JewelrySilverId);
            if (jewelrySilver == null)
            {
                throw new Exception("Jewelry Silver not found.");
            }

            using (var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                var newAuction = new Auction
                {
                    AccountId = createAuction.AccountId,
                    JewelrySilverId = createAuction.JewelrySilverId,
                    Starttime = createAuction.Starttime,
                    Endtime = createAuction.Endtime,
                    Status = AuctionStatus.UnActive.ToString(),
                };
                await _auctionRepository.AddAsync(newAuction);

                var newBid = new Bid
                {
                    AuctionId = newAuction.AuctionId,
                    Minprice = jewelrySilver.Price,
                    Maxprice = jewelrySilver.Price,
                    Datetime = DateTime.Now
                };
                await _bidRepository.AddAsync(newBid);

                transaction.Complete();
                return newAuction;
            }
        }

        public async Task<Auction> CreateJewelryGoldAuction(CreateGoldAuctionDTO createAuction)
        {
            if (_auctionRepository.IsJewelryInAuctionGold(createAuction.JewelryGoldId))
            {
                throw new Exception("The jewelry item is already in an auction.");
            }

            var jewelryGold = await _jewelryGoldRepository.GetByIdAsync(createAuction.JewelryGoldId);
            if (jewelryGold == null)
            {
                throw new Exception("Jewelry Gold not found.");
            }

            using (var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                var newAuction = new Auction
                {
                    AccountId = createAuction.AccountId,
                    JewelryGoldId = createAuction.JewelryGoldId,
                    Starttime = createAuction.Starttime,
                    Endtime = createAuction.Endtime,
                    Status = AuctionStatus.UnActive.ToString(),
                };
                await _auctionRepository.AddAsync(newAuction);

                var newBid = new Bid
                {
                    AuctionId = newAuction.AuctionId,
                   Minprice = jewelryGold.Price,
                  Maxprice = jewelryGold.Price,
                    Datetime = DateTime.Now
                };
                await _bidRepository.AddAsync(newBid);

                transaction.Complete();
                return newAuction;
            }
        }

        public async Task<Auction> CreateJewelryGoldDiamondAuction(CreateGoldDiamondAuctionDTO createAuction)
        {
            if (_auctionRepository.IsJewelryInAuctionGoldDiamond(createAuction.JewelryGolddiaId))
            {
                throw new Exception("The jewelry item is already in an auction.");
            }

            var jewelryGoldDiamond = await _jewelryDiaRepository.GetByIdAsync(createAuction.JewelryGolddiaId);
            if (jewelryGoldDiamond == null)
            {
                throw new Exception("Jewelry Gold Diamond not found.");
            }

            using (var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                var newAuction = new Auction
                {
                    AccountId = createAuction.AccountId,
                    JewelryGolddiaId = createAuction.JewelryGolddiaId,
                    Starttime = createAuction.Starttime,
                    Endtime = createAuction.Endtime,
                    Status = AuctionStatus.UnActive.ToString(),
                };
                await _auctionRepository.AddAsync(newAuction);

                var newBid = new Bid
                {
                    AuctionId = newAuction.AuctionId,
                    Minprice = jewelryGoldDiamond.Price,
                    Maxprice = jewelryGoldDiamond.Price,
                    Datetime = DateTime.Now
                };
                await _bidRepository.AddAsync(newBid);

                transaction.Complete();
                return newAuction;
            }
        }

        public async Task<Auction> UpdateSilverAuction(int id, UpdateSilverAuctionDTO updateAuction)
        {
            var auction = await _auctionRepository.GetByIdAsync(id);
            if (auction == null)
            {
                throw new Exception($"Auction with ID {id} not found.");
            }

            auction.JewelrySilverId = updateAuction.JewelrySilverId;
            auction.Starttime = updateAuction.Starttime;
            auction.Endtime = updateAuction.Endtime;
            auction.Status = updateAuction.Status;

            await _auctionRepository.UpdateAsync(auction);
            _logger.LogInformation($"Auction with ID {id} has been updated.");

            return auction;
        }

        public async Task<Auction> UpdateGoldAuction(int id, UpdateGoldAuctionDTO updateAuction)
        {
            var auction = await _auctionRepository.GetByIdAsync(id);
            if (auction == null)
            {
                throw new Exception($"Auction with ID {id} not found.");
            }

            auction.JewelryGoldId = updateAuction.JewelryGoldId;
            auction.Starttime = updateAuction.Starttime;
            auction.Endtime = updateAuction.Endtime;
            auction.Status = updateAuction.Status;

            await _auctionRepository.UpdateAsync(auction);
            _logger.LogInformation($"Auction with ID {id} has been updated.");

            return auction;
        }

        public async Task<Auction> UpdateGoldDiamondAuction(int id, UpdateGoldDiamondAuctionDTO updateAuction)
        {
            var auction = await _auctionRepository.GetByIdAsync(id);
            if (auction == null)
            {
                throw new Exception($"Auction with ID {id} not found.");
            }

            auction.JewelryGolddiaId = updateAuction.JewelryGolddiaId;
            auction.Starttime = updateAuction.Starttime;
            auction.Endtime = updateAuction.Endtime;
            auction.Status = updateAuction.Status;

            await _auctionRepository.UpdateAsync(auction);
            _logger.LogInformation($"Auction with ID {id} has been updated.");

            return auction;
        }

        public async Task<Auction> DeleteAuction(int id)
        {
            var auction = await _auctionRepository.GetByIdAsync(id);
            if (auction == null)
            {
                throw new Exception($"Auction with ID {id} not found.");
            }

            await _auctionRepository.RemoveAsync(auction);
            _logger.LogInformation($"Auction with ID {id} has been deleted.");

            return auction;
        }

        public async Task<IEnumerable<Auction>> GetAuctionAndJewelryGoldByAccountIdAsync(int accountId)
        {
            return await _auctionRepository.GetAuctionAndJewelryGoldByAccountId(accountId);
        }

        public async Task<IEnumerable<Auction>> GetAuctionAndJewelryGoldDiamondByAccountIdAsync(int accountId)
        {
            return await _auctionRepository.GetAuctionAndJewelryGoldDiamondByAccountId(accountId);
        }

        public async Task<IEnumerable<Auction>> GetAuctionAndJewelrySilverByAccountIdAsync(int accountId)
        {
            return await _auctionRepository.GetAuctionAndJewelrySilverByAccountId(accountId);
        }

        public int GetAccountCountInAuction(int auctionId)
        {
            return _auctionRepository.GetAccountCountInAuction(auctionId);
        }

        public IEnumerable<Auction> GetJewelryActiveAuctions()
        {
            return _auctionRepository.GetJewelryActiveAuctions();
        }

        public IEnumerable<object> GetAuctionsWithRemainingTime()
        {
            return _auctionRepository.GetAuctionsWithRemainingTime();
        }

        public async Task BroadcastRemainingTimeUpdates()
        {
            var auctions = GetAuctionsWithRemainingTime();
            await _hubContext.Clients.All.SendAsync("TimeRemaining", auctions);
        }
    }
}
