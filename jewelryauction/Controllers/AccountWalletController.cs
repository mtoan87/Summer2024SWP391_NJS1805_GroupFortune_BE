using DAL.DTO.AccountDTO;
using DAL.DTO.AccountWalletDTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Implement;
using Service.Interface;

namespace jewelryauction.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountWalletController : ControllerBase
    {
        private readonly AccountWalletService _accountWalletService;
        public AccountWalletController(AccountWalletService accountWalletService)
        {
            _accountWalletService = accountWalletService;
        }
        
    }
}
