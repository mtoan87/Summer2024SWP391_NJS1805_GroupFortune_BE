using DAL.DTO.AuctionDTO;
using DAL.DTO.JewelryDTO;
using DAL.Models;
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
        public async Task<Auction> CreateAuction(CreateAuctionDTO createAuction)
        {


            var newAuction = new Auction
            {
                AccountId = createAuction.AccountId,
                JewelryId = createAuction.JewelryId,
                Starttime = createAuction.Starttime,
                Endtime = createAuction.Endtime,
                Status = createAuction.Status,
            };
            _auctionRepository.AddAsync(newAuction);
            return newAuction;
        }
        public async Task<Auction> UpdateAuction(int id, UpdateAuctionDTO updateAuction)
        {
            var auction = await _auctionRepository.GetByIdAsync(id);
            if (auction == null)
            {
                throw new Exception($"Auction with ID {id} not found.");
            }
            
            auction.JewelryId = updateAuction.JewelryId;
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
    }
}
