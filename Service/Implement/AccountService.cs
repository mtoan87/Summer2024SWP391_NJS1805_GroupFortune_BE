using DAL.Models;
using DAL.DTO.AccountDTO;
using Repository.Interface;
using Service.Interface;
using Repository.Implement;
namespace Service.Implement
{
    public class AccountService 
    {
       
        private  AccountRepository _accountRepository;
        
        public AccountService( AccountRepository accountRepository)
        {
            
            _accountRepository = accountRepository;
        }
        public void CreateAccount(Account account)
        {
            

            _accountRepository.AddAccount(account);
        }
        public IEnumerable<Account> GetAllAccounts()
        {
            return _accountRepository.GetAllAccounts();
        }


    }
}
