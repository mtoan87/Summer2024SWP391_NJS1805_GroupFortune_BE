using DAL.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Implement
{
    public class AccountRepository : RepositoryGeneric<Account> , IAccountRepository
    {
        
        public AccountRepository(JewelryAuctionContext context) : base(context)
        {

        }
        
        public async Task<bool> CheckExistingGmailAsync(string gmail)
        {
            return await _dbSet.AnyAsync(m => m.AccountEmail == gmail);
        }
        
        public void AddAccount(Account account)
        {
            _context.Accounts.Add(account);
            _context.SaveChanges();
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
