using DAL.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Interface;
using System;
using System.Linq;
using System.Threading.Tasks;
using BCrypt.Net;

namespace Repository.Implement
{
    public class AccountRepository : RepositoryGeneric<Account>, IAccountRepository
    {
        public AccountRepository(JewelryAuctionContext context) : base(context)
        {
        }

        public async Task<bool> CheckExistingGmailAsync(string gmail)
        {
            return await _dbSet.AnyAsync(m => m.AccountEmail == gmail);
        }

        public Account CheckLogin(string gmail, string password)
        {
            var account = _dbSet.FirstOrDefault(x => x.AccountEmail == gmail);
            if (account != null && BCrypt.Net.BCrypt.Verify(password, account.AccountPassword))
            {
                return account;
            }
            return null;
        }
        public async Task<Account?> GetAccountByEmailAsync(string email)
        {
            return await _context.Accounts.FirstOrDefaultAsync(a => a.AccountEmail == email);
        }

        public async Task<Account?> GetAccountById(int id)
        {
            return await _context.Accounts.FirstOrDefaultAsync(a => a.AccountId == id);
        }
    }
}
