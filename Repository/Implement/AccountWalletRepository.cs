using DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Implement
{
    public class AccountWalletRepository : RepositoryGeneric<AccountWallet>
    {
        public AccountWalletRepository(JewelryAuctionContext context) : base(context)
        {
           
        }

        public async Task<IEnumerable<AccountWallet>> GetWalletByAccountId(int accountId)
        {
            return await _context.AccountWallets
                
                .Where(a => a.AccountId == accountId)
                .ToListAsync();
        }

        public async Task<AccountWallet> GetByAccountIdAsync(int accountId)
        {
            return await _context.Set<AccountWallet>().FirstOrDefaultAsync(aw => aw.AccountId == accountId);
        }
    }
}
