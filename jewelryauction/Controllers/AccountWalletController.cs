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
        private readonly IAccountWalletService _accountWalletService;
        public AccountWalletController(IAccountWalletService accountWalletService)
        {
            _accountWalletService = accountWalletService;
        }
        [HttpGet]
        [Route("GetAccountWallet")]
        public async Task<IActionResult> GetAccountWallets()
        {
            var accounts = await _accountWalletService.GetAccountWallet();
            return Ok(accounts);
        }
        [HttpGet("GetById/{Id}")]
        public async Task<IActionResult> GetAccountWalletById(int Id)
        {
            var jewelry = await _accountWalletService.GetAccountWalletById(Id);
            return Ok(jewelry);
        }
        [HttpGet("GetAccountWalletByAccountId/{AccountId}")]
        public async Task<IActionResult> GetAccountWalletByAccountId(int AccountId)
        {
            var jewelry = await _accountWalletService.GetAccountWalletByAccountId(AccountId);
            return Ok(jewelry);
        }
        [HttpPost]
        [Route("CreateAccountWallet")]
        public async Task<IActionResult> CreateAccountWallet(CreateAccountWalletDTO createAccountWalletDTO)
        {
            var result = await _accountWalletService.CreateAccountWallet(createAccountWalletDTO);
            return Ok(result);
        }
        [HttpPut]
        [Route("UpdateAccountWallet")]
        public async Task<IActionResult> UpdateAccountWallet(int id, UpdateAccountWalletDTO updateAccount)
        {
            var result = await _accountWalletService.UpdateAccountWallet(id, updateAccount);
            return Ok(result);
        }
    }
}
