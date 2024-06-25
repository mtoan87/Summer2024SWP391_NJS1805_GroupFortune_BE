using DAL.Models;
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
        public IEnumerable<AccountWallet> GetAccountWallets()
        {
            return _context.AccountWallets.ToList();
        }
        public async Task<bool> UpdateAccountWalletAsync(AccountWallet accountWallet)
        {
            try
            {
                _context.AccountWallets.Update(accountWallet);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
