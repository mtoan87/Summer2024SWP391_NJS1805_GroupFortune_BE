using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interface
{
    public interface ITransactionService
    {
        Task<IEnumerable<WalletTransaction>> GetAllTransactions();
        Task<WalletTransaction> GetTransactionById(int id);
        Task<IEnumerable<WalletTransaction>> GetTransactionByWalletId(int id);
    }
}
