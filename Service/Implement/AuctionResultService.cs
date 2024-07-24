using DAL.DTO.AuctionDTO;
using DAL.DTO.AuctionResultDTO;
using DAL.Models;
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
        public AuctionResultService(IAuctionResultRepository repository, IAccountWalletRepository accountwalletRepo, ITransactionRepository transactionRepo, IJoinAuctionRepository joinAuctionRepository)
        {
            _repository = repository;
            _accountwalletRepo = accountwalletRepo;
            _transactionRepo = transactionRepo;
            _joinAuctionRepository = joinAuctionRepository;
        }
        public async Task<IEnumerable<AuctionResult>> GetAllAuctionResults()
        {
            return await _repository.GetAllAsync();
        }
        public async Task<AuctionResult> GetAuctionResultById(int id)
        {
            return await _repository.GetByIdAsync(id);
        }
        public async Task<AuctionResult> CreateAuctionRs(CreateAuctionRsDTO createAuctionResultDto)
        {
           
            var joinAuctions = await _joinAuctionRepository.GetAuctionByJoinauctionIdAsync(createAuctionResultDto.JoinauctionId);
            var auction = joinAuctions.FirstOrDefault()?.Auction; 
            if (auction == null)
            {
                throw new Exception($"Auction not found for Joinauction ID {createAuctionResultDto.JoinauctionId}.");
            }

          
            if (!auction.AccountId.HasValue)
            {
                throw new Exception($"Auction does not have an AccountId.");
            }

            var auctionOwnerWallet = await _accountwalletRepo.GetByAccountIdAsync(auction.AccountId.Value);
            if (auctionOwnerWallet == null)
            {
                throw new Exception($"Account wallet for auction owner account ID {auction.AccountId.Value} not found.");
            }

          
            var auctionResults = await _repository.GetResultsByJoinauctionIdAsync(createAuctionResultDto.JoinauctionId);
            var winningResult = auctionResults.FirstOrDefault(result => result.Status == "Win");

            if (winningResult == null)
            {
                throw new Exception("No winning result found for the specified auction.");
            }

           
            if (!winningResult.AccountId.HasValue)
            {
                throw new Exception($"Winning result does not have an AccountId.");
            }

            var winnerWallet = await _accountwalletRepo.GetByAccountIdAsync(winningResult.AccountId.Value);
            if (winnerWallet == null)
            {
                throw new Exception($"Account wallet for winner account ID {winningResult.AccountId.Value} not found.");
            }

            
            if (winnerWallet.Budget < winningResult.Price)
            {
                throw new Exception("Insufficient budget for the winner.");
            }
            winnerWallet.Budget -= winningResult.Price;

          
            var winnerTransaction = new WalletTransaction
            {
                AccountwalletId = winnerWallet.AccountwalletId,
                Amount = -winningResult.Price, 
                DateTime = DateTime.Now
            };
            await _transactionRepo.AddAsync(winnerTransaction);

            
            auctionOwnerWallet.Budget += winningResult.Price;

            
            var ownerTransaction = new WalletTransaction
            {
                AccountwalletId = auctionOwnerWallet.AccountwalletId,
                Amount = winningResult.Price, 
                DateTime = DateTime.Now
            };
            await _transactionRepo.AddAsync(ownerTransaction);

            
            var newAuctionResult = new AuctionResult
            {
                JoinauctionId = createAuctionResultDto.JoinauctionId,
                Date = createAuctionResultDto.Date,
                Status = createAuctionResultDto.Status,
                Price = createAuctionResultDto.Price,
                AccountId = createAuctionResultDto.AccountId
            };

            
            await _repository.AddAsync(newAuctionResult);

            
            await _accountwalletRepo.UpdateAsync(winnerWallet);
            await _accountwalletRepo.UpdateAsync(auctionOwnerWallet);

            return newAuctionResult;
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
