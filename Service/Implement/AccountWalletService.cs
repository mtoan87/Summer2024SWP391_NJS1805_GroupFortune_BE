using DAL.DTO.AccountDTO;
using DAL.DTO.AccountWalletDTO;
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
    public class AccountWalletService
    {
        private readonly AccountWalletRepository _accountWalletRepository;
        public AccountWalletService(AccountWalletRepository accountWalletRepository)
        {
            _accountWalletRepository = accountWalletRepository;
        }

        public IEnumerable<AccountWallet> GetAccountWallet()
        {
            return _accountWalletRepository.GetAccountWallets();
        }

        public async Task<AccountWallet> GetAccountWalletById(int id)
        {
            return await _accountWalletRepository.GetByIdAsync(id);
        }

        public async Task<AccountWallet> CreateAccountWallet(CreateAccountWalletDTO createAccountWallet)
        {

            var newAccountWallet = new AccountWallet
            {
                AccountId = createAccountWallet.AccountId,
                BankName = createAccountWallet.BankName,
                BankNo = createAccountWallet.BankNo,
                Budget = createAccountWallet.Budget,

            };
            await _accountWalletRepository.AddAsync(newAccountWallet);
            await _accountWalletRepository.SaveChangesAsync();
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
            await _accountWalletRepository.UpdateAccountWalletAsync(account);
            return account;
            
        }        
    }
}
    

