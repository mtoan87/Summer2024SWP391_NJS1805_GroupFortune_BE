using DAL.Models;
using DAL.DTO.AccountDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interface
{
    public interface IAccountService
    {
        Task<IEnumerable<Account>> GetAllAccounts();
        Task<Account> GetAccountById(int id);
        Task<Account> CreateAccount(AdminCreateAccountDTO adminCreateAccountDTO);
        Task RegisterAccount(RegisterAccountDTO registerAccount);
        Task<Account> UpdateAccount(int id, UpdateAccountDTO updateAccount);
        Task<Account> DeleteAccount(int id);
        Account CheckLogin(string gmail, string password);
        Task<bool> CheckExistingGmailAsync(string gmail);
    }
}
