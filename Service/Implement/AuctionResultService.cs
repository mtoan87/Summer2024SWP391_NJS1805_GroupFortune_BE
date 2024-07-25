using DAL.DTO.AuctionDTO;
using DAL.DTO.AuctionResultDTO;
using DAL.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Implement;
using Repository.Interface;
using Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Implement
{
    public class AuctionResultService : IAuctionResultService
    {
        private readonly IAuctionResultRepository _repository;
        private readonly IAccountWalletRepository _accountwalletRepo;
        private readonly ITransactionRepository _transactionRepo;
        private readonly IJoinAuctionRepository _joinAuctionRepository;
        private readonly IAuctionRepository _auctionRepository;
        public AuctionResultService(IAuctionResultRepository repository, IAccountWalletRepository accountwalletRepo, ITransactionRepository transactionRepo, IJoinAuctionRepository joinAuctionRepository, IAuctionRepository auctionService)
        {
            _repository = repository;
            _accountwalletRepo = accountwalletRepo;
            _transactionRepo = transactionRepo;
            _joinAuctionRepository = joinAuctionRepository;
            _auctionRepository = auctionService;
        }
        public async Task<IEnumerable<AuctionResult>> GetAllAuctionResults()
        {
            return await _repository.GetAllAsync();
        }
        public async Task<AuctionResult> GetAuctionResultById(int id)
        {
            return await _repository.GetByIdAsync(id);
        }
        private AuctionResult ConvertDtoToEntity(CreateAuctionRsDTO dto)
        {
            return new AuctionResult
            {
                JoinauctionId = dto.JoinauctionId,
                Date = DateTime.Now,
                Status = dto.Status,
                Price = dto.Price,
                AccountId = dto.AccountId
            };
        }

        public async Task<IEnumerable<AuctionResult>> CreateAuctionResultAsync(CreateAuctionRsDTO auctionResultDto)
        {
            // Convert DTO to entity
            var auctionResult = ConvertDtoToEntity(auctionResultDto);

            // Add the auction result to the repository
            await _repository.AddAsync(auctionResult);

            // Include related data
            auctionResult.Joinauction = await _joinAuctionRepository.GetByIdAsync(auctionResultDto.JoinauctionId);

            if (auctionResult.Joinauction == null)
            {
                throw new InvalidOperationException("Joinauction not found for the given JoinauctionId.");
            }

            // Process the auction result to update budgets if necessary
            bool processed = await _auctionRepository.ProcessAuctionResultAsync(auctionResult);

            if (!processed)
                return Enumerable.Empty<AuctionResult>();

            // Save the changes
            await _repository.SaveChangesAsync();

            // Return the updated list of auction results
            return await _repository.GetAllAsync();
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

            await _repository.UpdateAsync(auctionRs);
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

        public async Task<IEnumerable<AuctionResult>> GetResultsByJoinauctionIdAsync(int joinAuctionId)
        {
            return await _repository.GetResultsByJoinauctionIdAsync(joinAuctionId);
        }

       
    }
}
