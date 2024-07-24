using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interface
{
    public interface IAccountRepository : IRepositoryGeneric<Account>
    {
        Task<bool> CheckExistingGmailAsync(string gmail);
       
        Task<Account?> GetAccountByEmailAsync(string email);
        Task<Account?> GetAccountById(int id);

        Account CheckLogin(string gmail, string password);
    }
}
