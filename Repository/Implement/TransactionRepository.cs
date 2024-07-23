using DAL.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Implement
{
    public class TransactionRepository : RepositoryGeneric<WalletTransaction>, ITransactionRepository
    {
        public TransactionRepository(JewelryAuctionContext context) : base(context)
        {

        }
        public async Task<IEnumerable<WalletTransaction>> GetTransactionByWalletId(int walletId)
        {
            return await _context.WalletTransactions
                .Where(a => a.AccountwalletId == walletId)
                .ToListAsync();
        }
    }
}
