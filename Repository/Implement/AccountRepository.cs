using DAL.Models;
using Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Implement
{
    public class AccountRepository 

    {
        private readonly JewelryAuctionContext _context;
        public AccountRepository(JewelryAuctionContext context) 
        {
            _context = context;
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
    }
}
