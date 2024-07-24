using DAL.Models;
using DAL.DTO.AccountDTO;
using Repository.Interface;
using Service.Interface;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.Implement
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;

        public AccountService(IAccountRepository accountRepository)
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

        public async Task<bool> CheckExistingGmailAsync(string gmail)
        {
            return await _accountRepository.CheckExistingGmailAsync(gmail);
        }

        public Account CheckLogin(string gmail, string password)
        {
            return _accountRepository.CheckLogin(gmail, password);
        }

        public async Task<Account> CreateAccount(AdminCreateAccountDTO adminCreateAccountDTO)
        {
            if (await _accountRepository.CheckExistingGmailAsync(adminCreateAccountDTO.AccountEmail))
            {
                throw new Exception("Duplicate email!");
            }

            var newAccount = new Account
            {
                AccountEmail = adminCreateAccountDTO.AccountEmail,
                AccountPassword = BCrypt.Net.BCrypt.HashPassword(adminCreateAccountDTO.AccountPassword),
                AccountName = adminCreateAccountDTO.AccountName,
                AccountPhone = adminCreateAccountDTO.AccountPhone,
                RoleId = 2,
            };

            await _accountRepository.AddAsync(newAccount);
            return newAccount;
        }

        public async Task RegisterAccount(RegisterAccountDTO registerAccount)
        {
            var newAccount = new Account
            {
                AccountEmail = registerAccount.AccountEmail,
                AccountPassword = BCrypt.Net.BCrypt.HashPassword(registerAccount.AccountPassword),
                AccountName = registerAccount.AccountName,
                AccountPhone = registerAccount.AccountPhone,
                RoleId = 2,
            };

            await _accountRepository.AddAsync(newAccount);
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
            account.AccountPassword = BCrypt.Net.BCrypt.HashPassword(updateAccount.AccountPassword);
            account.AccountPhone = updateAccount.AccountPhone;

            await _accountRepository.UpdateAsync(account);
            return account;
        }

        public async Task<Account> DeleteAccount(int id)
        {
            var account = await _accountRepository.GetAccountById(id);
            if (account == null)
            {
                throw new Exception($"Account with ID {id} not found.");
            }

            await _accountRepository.RemoveAsync(account);
            return account;
        }
    }
}
