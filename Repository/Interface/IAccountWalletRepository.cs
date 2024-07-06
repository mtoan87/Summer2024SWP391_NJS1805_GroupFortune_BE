using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interface
{
    public interface IAccountWalletRepository : IRepositoryGeneric<AccountWallet>
    {
        Task<IEnumerable<AccountWallet>> GetWalletByAccountId(int accountId);
        Task<AccountWallet> GetByAccountIdAsync(int accountId);
    }
}
