using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DAL.Models;
using Service.Implement;
using jewelryauction.Views;

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
        public IActionResult GetAllAccounts()
        {
            var accounts = _accountService.GetAllAccounts();
            return Ok(accounts);
        }

        [HttpPost("Create Account")]
        public IActionResult CreateAccount(RequestAccountModel requestModel)
        {

            var newAccount = new Account
            {
                AccountId = requestModel.AccountId,
                AccountName = requestModel.AccountName,
                AccountEmail = requestModel.AccountEmail,
                AccountPassword = requestModel.AccountPassword,
                AccountPhone = requestModel.AccountPhone,
                RoleId = requestModel.RoleId
            };

            _accountService.CreateAccount(newAccount);
            return Ok("Account created successfully.");
        }

    }
}
