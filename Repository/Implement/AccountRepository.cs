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
    public class AccountRepository : RepositoryGeneric<Account> 
    {
        public AccountRepository(JewelryAuctionContext context) : base(context)
        {
        }

        public async Task<Account> GetClassByGmailAsync(string gmail)
        {
            return await _dbSet.SingleOrDefaultAsync(c => c.AccountEmail == gmail);
        }
        public async Task<Account> GetRoleId(int role)
        {
            return await _dbSet.SingleAsync(c => c.RoleId == role);
        }
        

        public async Task<bool> CheckExistingGmailAsync(string gmail)
        {
            return await _dbSet.AnyAsync(m => m.AccountEmail == gmail);
        }

        public async Task<bool> UpdateClassAsync(Account _class)
        {
            try
            {
                _context.Accounts.Update(_class);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        
        public void Add(Account account)
        {
            throw new NotImplementedException();
        }

        public void AddAccount(Account account)
        {
            _context.Accounts.Add(account);
            _context.SaveChanges();
        }

        public IEnumerable<Account> GetAllAccounts()
        {
            return _context.Accounts.ToList();
        }

        public Account? GetByEmail(string email)
        {
            return _context.Accounts.FirstOrDefault(a => a.AccountEmail == email);
        }

        public async Task<Account?> GetAccountByEmailAsync(string email)
        {
            return await _context.Accounts.FirstOrDefaultAsync(a => a.AccountEmail == email);
        }
    }
}
