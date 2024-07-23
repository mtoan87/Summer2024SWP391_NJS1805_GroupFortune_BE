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
    public class TransactionService : ITransactionService
    {
        private readonly ITransactionRepository _repo;
        public TransactionService(ITransactionRepository repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<WalletTransaction>> GetAllTransactions()
        {
            return await _repo.GetAllAsync();
        }
        public async Task<WalletTransaction> GetTransactionById(int id)
        {
            return await _repo.GetByIdAsync(id);
        }

        public async Task<IEnumerable<WalletTransaction>> GetTransactionByWalletId(int id)
        {
            return await _repo.GetTransactionByWalletId(id);
        }
    }
}
