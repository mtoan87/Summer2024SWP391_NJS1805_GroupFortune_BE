using DAL.DTO.AuctionDTO;
using DAL.DTO.JewelryDTO;
using DAL.Models;
using Microsoft.Extensions.DependencyInjection;
using Repository.Implement;
using Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Implement
{
    public class AuctionService
    {
        private readonly AuctionRepository _auctionRepository;
        public AuctionService(AuctionRepository auctionRepository)
        {
            _auctionRepository = auctionRepository;
        }

        public IEnumerable<Auction> GetAllAuctions()
        {
            return _auctionRepository.GetAllAuctions();
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
        public async Task<Auction> CreateJewelrySilverAuction(CreateAuctionDTO createAuction)
        {


            var newAuction = new Auction
            {
                AccountId = createAuction.AccountId,
                JewelrySilverId = createAuction.JewelrySilverId,
                DateofAuction = createAuction.DateofAuction,
                Starttime = createAuction.Starttime,
                Endtime = createAuction.Endtime,
                Status = "UnActive"
            };
            await _auctionRepository.AddAsync(newAuction);
            await _auctionRepository.SaveChangesAsync();
            return newAuction;
        }
        public async Task<Auction> CreateJewelryGoldAuction(CreateAuctionDTO createAuction)
        {


            var newAuction = new Auction
            {
                AccountId = createAuction.AccountId,
                JewelryGoldId = createAuction.JewelryGoldId,
                DateofAuction = createAuction.DateofAuction,
                Starttime = createAuction.Starttime,
                Endtime = createAuction.Endtime,
                Status = "UnActive"
            };
            await _auctionRepository.AddAsync(newAuction);
            await _auctionRepository.SaveChangesAsync();
            return newAuction;
        }
        public async Task<Auction> UpdateAuction(int id, UpdateAuctionDTO updateAuction)
        {
            var auction = await _auctionRepository.GetByIdAsync(id);
            if (auction == null)
            {
                throw new Exception($"Auction with ID {id} not found.");
            }
            
            auction.JewelrySilverId = updateAuction.JewelrySilverId;
            auction.JewelryGoldId = updateAuction.JewelryGoldId;
            auction.Starttime = updateAuction.Starttime;
            auction.Endtime = updateAuction.Endtime;
            auction.Status = updateAuction.Status;
            await _auctionRepository.UpdateAuctionAsync(auction);
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
            return auction;
        }

        public async Task<IEnumerable<Auction>> GetAuctionAndJewelryGoldByAccountIdAsync(int accountId)
        {
            return await _auctionRepository.GetAuctionAndJewelryGoldByAccountId(accountId);
        }
        public async Task<IEnumerable<Auction>> GetAuctionAndJewelrySilverByAccountIdAsync(int accountId)
        {
            return await _auctionRepository.GetAuctionAndJewelrySilverByAccountId(accountId);
        }
        public int GetAccountCountInAuction(int auctionId)
        {
            return _auctionRepository.GetAccountCountInAuction(auctionId);
        }
    }
}
