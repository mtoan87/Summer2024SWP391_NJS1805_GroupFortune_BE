﻿using DAL.Models;
using DAL.DTO.AccountDTO;
using Repository.Interface;
using Service.Interface;
using Repository.Implement;
using Azure.Core;
namespace Service.Implement
{
    public class AccountService 
    {
       
        private  readonly AccountRepository _accountRepository;
        
        
        public AccountService( AccountRepository accountRepository)
        {
            
            _accountRepository = accountRepository;
        }
        
        public async Task<IEnumerable<Account>> GetAllAccounts()
        {
            return await _accountRepository.GetAllAsync();
        }
        public async Task<Account> GetAccountById(int id)
        {
            return await _accountRepository.GetByIdAsync(id);
        }
        public async Task<Account> CreateAccount(AdminCreateAccountDTO adminCreateAccountDTO)
        {
            var account = await _accountRepository.CheckExistingGmailAsync(adminCreateAccountDTO.AccountEmail);
                {
                if (!account)
                {
                    throw new Exception("Duplicate email!");
                }
                var newAccount = new Account
                {
                    AccountEmail = adminCreateAccountDTO.AccountEmail,
                    AccountPassword = adminCreateAccountDTO.AccountPassword,
                    AccountName = adminCreateAccountDTO.AccountName,
                    AccountPhone = adminCreateAccountDTO.AccountPhone,
                    RoleId = 2,
                };
                _accountRepository.AddAccount(newAccount);
                return newAccount;
            }
        }
        public async Task RegisterAccount(RegisterAccountDTO registerAccount)
        {
            var newAccount = new Account
            {
                AccountEmail = registerAccount.AccountEmail,
                AccountPassword = registerAccount.AccountPassword,
                AccountName = registerAccount.AccountName,
                AccountPhone = registerAccount.AccountPhone,
                RoleId = 2,
            };              
             _accountRepository.AddAccount(newAccount);
        }
        public async Task<Account> UpdateAccount(int id, UpdateAccountDTO updateAccount)
        {
            var account = await _accountRepository.GetAccountById(id);
            if (account == null)
            {               
                throw new Exception($"Account with ID {id} not found.");
            }          
            account.AccountName = updateAccount.AccountName;
            account.AccountEmail = updateAccount.AccountEmail;
            account.AccountPassword = updateAccount.AccountPassword;
            account.AccountPhone = updateAccount.AccountPhone;          
           await _accountRepository.UpdateAsync(account);
            return account;
        }
        public async Task<Account> DeleteAccount(int id)
        {
            var account = await _accountRepository.GetAccountById(id);
            if(account == null)
            {
                throw new Exception($"Account with ID {id} not found.");
            }
            await _accountRepository.RemoveAsync(account);
            return account;
        }
        
        
    }
}
