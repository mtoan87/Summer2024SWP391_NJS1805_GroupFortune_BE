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
        public AuctionResultService(IAuctionResultRepository repository, IAccountWalletRepository accountwalletRepo, ITransactionRepository transactionRepo)
        {
            _repository = repository;
            _accountwalletRepo = accountwalletRepo;
            _transactionRepo = transactionRepo;
        }
        public async Task<IEnumerable<AuctionResult>> GetAllAuctionResults()
        {
            return await _repository.GetAllAsync();
        }
        public async Task<AuctionResult> GetAuctionResultById(int id)
        {
            return await _repository.GetByIdAsync(id);
        }
        public async Task<AuctionResult> CreateAuctionRs(CreateAuctionRsDTO createAuctionRs)
        {
            var accountWallet = await _accountwalletRepo.GetByAccountIdAsync(createAuctionRs.AccountId);
            if (accountWallet == null)
            {
                throw new Exception($"Account wallet for account ID {createAuctionRs.AccountId} not found.");
            }

            if (accountWallet.Budget < createAuctionRs.Price)
            {
                throw new Exception("Insufficient budget.");
            }
            accountWallet.Budget -= createAuctionRs.Price;

            var newAuctionRs = new AuctionResult
            {
                JoinauctionId = createAuctionRs.JoinauctionId,
                Date = createAuctionRs.Date,
                Status = createAuctionRs.Status,
                Price = createAuctionRs.Price,
                AccountId = createAuctionRs.AccountId,  


            };
            await _repository.AddAsync(newAuctionRs);
            await _accountwalletRepo.UpdateAsync(accountWallet);
            var walletTransaction = new WalletTransaction
            {
                AccountwalletId = accountWallet.AccountwalletId,
                Amount = -createAuctionRs.Price,
                DateTime = DateTime.Now
            };
            await _transactionRepo.AddAsync(walletTransaction);
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
    }
}
