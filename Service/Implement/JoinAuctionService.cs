using DAL.DTO.AuctionDTO;
using DAL.DTO.JewelryDTO;
using DAL.DTO.JoinAuctionDTO;
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
    public class JoinAuctionService
    {
        private readonly JoinAuctionRepository _joinAuctionRepository;
        public JoinAuctionService(JoinAuctionRepository joinAuctionRepository)
        {
            _joinAuctionRepository = joinAuctionRepository;
        }
        public IEnumerable<JoinAuction> GetAllJoinAuctions()
        {
            return _joinAuctionRepository.GetAllJoinAuction();
        }

        public async Task<JoinAuction> GetJoinAuctionById(int id)
        {
            return await _joinAuctionRepository.GetByIdAsync(id);
        }
        public async Task<JoinAuction> CreateJoinAuction(CreateJoinAuctionDTO createJoinAuction)
        {
            var newJoinAuction = new JoinAuction
            {
                AccountId = createJoinAuction.AccountId,
                BidId = createJoinAuction.BidId,
                AuctionId = createJoinAuction.AuctionId,
                Joindate = DateTime.Now,
            };
            await _joinAuctionRepository.AddAsync(newJoinAuction);
            await _joinAuctionRepository.SaveChangesAsync();
            return newJoinAuction;
        }

        public async Task<JoinAuction> UpdateJoinAuction(int id, UpdateJoinAuctionDTO updateJoinAuction)
        {
            var auction = await _joinAuctionRepository.GetByIdAsync(id);
            if (auction == null)
            {
                throw new Exception($"JoinAuction with ID {id} not found.");
            }

            auction.AuctionId = updateJoinAuction.AuctionId;
            auction.AccountId = updateJoinAuction.AccountId;
            auction.BidId = updateJoinAuction.BidId;
             await _joinAuctionRepository.UpdateJoinAuctionAsync(auction);
            return auction;
        }

        public async Task<JoinAuction> DeleteJoinAuction(int id)
        {
            var account = await _joinAuctionRepository.GetByIdAsync(id);
            if (account == null)
            {
                throw new Exception($"Account with ID {id} not found.");
            }
            await _joinAuctionRepository.RemoveAsync(account);
            return account;
        }
    }
}
