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

        public async Task<JoinAuction> CreateJoinAuction(CreateJoinAuctionDTO createJoinAuction)
        {
            var newJoinAuction = new JoinAuction
            {
                AccountId = createJoinAuction.AccountId,
                BidId = createJoinAuction.BidId,
                AuctionId = createJoinAuction.AuctionId,
                Joindate = DateTime.Now,
            };
            _joinAuctionRepository.AddAsync(newJoinAuction);
            _joinAuctionRepository.SaveChangesAsync();
            return newJoinAuction;
        }
    }
}
