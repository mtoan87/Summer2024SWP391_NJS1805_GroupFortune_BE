using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DAL.Models;
using Service.Implement;

using DAL.DTO.AccountDTO;
using Service.Interface;

namespace jewelryauction.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly AccountService _accountService;

        public AccountController(AccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpGet]
        [Route("GetAllAccount")]
        public IActionResult GetAllAccounts()
        {
            var accounts = _accountService.GetAllAccounts();
            return Ok(accounts);
        }
        [HttpGet("GetById/{Id}")]
        public async Task<IActionResult> GetAccountById(int Id)
        {
            var jewelry = await _accountService.GetAccountById(Id);
            return Ok(jewelry);
        }

        [HttpPost]
        [Route("CreateAccount")]
        public async  Task<IActionResult> CreateAccount(AdminCreateAccountDTO adminCreateAccount)
        {
            var result = await _accountService.CreateAccount(adminCreateAccount);
            return Ok(result);
        }
        [HttpPut]
        [Route("UpdateAccount")]
        public async Task<IActionResult> UpdateAccount(int id, UpdateAccountDTO updateAccount)
        {
            var result = await _accountService.UpdateAccount(id, updateAccount);
            return Ok(result);
        }
        [HttpDelete]
        [Route("DeleteAccount")]
        public async Task<IActionResult> DeleteAccount(int id)
        {
            var result = await _accountService.DeleteAccount(id);
            return Ok(result);
        }

    }
}
