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

        public async Task RegisterAccount(RegisterAccountDTO registerAccount)
        {
            var newAccount = new Account
            {
                AccountEmail = registerAccount.AccountEmail,
                AccountPassword = registerAccount.AccountPassword,
                AccountName = registerAccount.AccountName,
                AccountPhone = registerAccount.AccountPhone,
                RoleId = 2,
            };              
             _accountRepository.AddAccount(newAccount);
        }

        public async Task<Account>? Login(string email)
        {
            return await _accountRepository.GetAccountByEmailAsync(email);
        }
        public bool IsEmailTaken(string email)
        {
            return _accountRepository.GetByEmail(email) != null;
        }

    }
}
