﻿using Azure.Core;
using DAL.DTO.AccountDTO;
using DAL.DTO.AccountWalletDTO;
using DAL.DTO.WalletTransaction;
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
    public class AccountWalletService : IAccountWalletService
    {
        private readonly IAccountWalletRepository _accountWalletRepository;
        private readonly ITransactionService _transactionService;
        public AccountWalletService(IAccountWalletRepository accountWalletRepository, ITransactionService transactionService)
        {
            _accountWalletRepository = accountWalletRepository;
            _transactionService = transactionService;
        }

        public async Task<IEnumerable<AccountWallet>> GetAccountWallet()
        {
            return await _accountWalletRepository.GetAllAsync();
        }

        public async Task<AccountWallet> GetAccountWalletById(int id)
        {
            return await _accountWalletRepository.GetByIdAsync(id);
        }
        public async Task<AccountWallet> GetAccountWalletByAccountId(int id)
        {
            return await _accountWalletRepository.GetByAccountIdAsync(id);
        }

        public async Task<AccountWallet> CreateAccountWallet(WalletTransactionDTO createAccountWallet)
        {

            var newAccountWallet = new AccountWallet
            {
                AccountId = createAccountWallet.AccountId,
                BankName = createAccountWallet.BankName,
                BankNo = createAccountWallet.BankNo,
                Budget = createAccountWallet.Amount,

            };
            await _accountWalletRepository.AddAsync(newAccountWallet);
            var transaction = new WalletTransaction
            {
                AccountwalletId = newAccountWallet.AccountwalletId, 
                Amount = createAccountWallet.Amount,
                DateTime = DateTime.Now
            };
            return newAccountWallet;
        }

        public  async Task<AccountWallet> UpdateAccountWallet(int id,UpdateAccountWalletDTO updateAccountWallet)
        {
            var account = await _accountWalletRepository.GetByIdAsync(id);
            if(account == null)
            {
                throw new Exception($"AccountWallet with ID{id} not found");
            }
            account.BankName = updateAccountWallet.BankName;
            account.BankNo = updateAccountWallet.BankNo;
            account.Budget = updateAccountWallet.Budget;
            await _accountWalletRepository.UpdateAsync(account);
            return account;
            
        }        
    }
}
    

