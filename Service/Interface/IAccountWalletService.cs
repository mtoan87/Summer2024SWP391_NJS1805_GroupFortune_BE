using DAL.DTO.AccountWalletDTO;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interface
{
    public interface IAccountWalletService
    {
        Task<IEnumerable<AccountWallet>> GetAccountWallet();
        Task<AccountWallet> GetAccountWalletById(int id);
        Task<AccountWallet> CreateAccountWallet(CreateAccountWalletDTO createAccountWallet);
        Task<AccountWallet> UpdateAccountWallet(int id, UpdateAccountWalletDTO updateAccountWallet);
    }
}
