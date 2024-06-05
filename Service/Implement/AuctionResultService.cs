using DAL.DTO.AuctionDTO;
using DAL.DTO.AuctionResultDTO;
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
    public class AuctionResultService
    {
        private readonly AuctionResultRepository _repository;
        public AuctionResultService(AuctionResultRepository repository)
        {
            _repository = repository;
        }
        public IEnumerable<AuctionResult> GetAllAuctionResults()
        {
            return _repository.GetAllAuctionResults();
        }
        public async Task<AuctionResult> GetAuctionResultById(int id)
        {
            return await _repository.GetByIdAsync(id);
        }
        public async Task<AuctionResult> CreateAuctionRs(CreateAuctionRsDTO createAuctionRs)
        {


            var newAuctionRs = new AuctionResult
            {
                JoinauctionId = createAuctionRs.JoinauctionId,
                Date = createAuctionRs.Date,
                Status = createAuctionRs.Status,
                Price = createAuctionRs.Price,
                AccountId = createAuctionRs.AccountId,  


            };
            _repository.AddAsync(newAuctionRs);
            _repository.SaveChangesAsync();
            return newAuctionRs;
        }
        public async Task<AuctionResult> UpdateAuctionRs(int id, UpdateAuctionRsDTO updateAuctionRs)
        {
            var auctionRs = await _repository.GetByIdAsync(id);
            if (auctionRs == null)
            {
                throw new Exception($"AuctionRs with ID {id} not found.");
            }

            auctionRs.JoinauctionId = updateAuctionRs.JoinauctionId;
            auctionRs.Date = updateAuctionRs.Date;
            auctionRs.Status = updateAuctionRs.Status;
            auctionRs.Price = updateAuctionRs.Price;
            auctionRs.AccountId = updateAuctionRs.AccountId;

            await _repository.UpdateAuctionRsAsync(auctionRs);
            return auctionRs;
        }
        public async Task<AuctionResult> DeleteAuction(int id)
        {
            var auctionRs = await _repository.GetByIdAsync(id);
            if (auctionRs == null)
            {
                throw new Exception($"AuctionRs with ID {id} not found.");
            }
            await _repository.RemoveAsync(auctionRs);
            return auctionRs;
        }
    }
}
