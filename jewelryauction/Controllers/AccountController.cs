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

        [HttpPost("CreateAccount")]
        public IActionResult CreateAccount(AdminCreateAccountDTO requestModel)
        {

            var newAccount = new Account
            {
              
                AccountName = requestModel.AccountName,
                AccountEmail = requestModel.AccountEmail,
                AccountPassword = requestModel.AccountPassword,
                AccountPhone = requestModel.AccountPhone,
                RoleId = requestModel.RoleId
            };

            _accountService.CreateAccount(newAccount);
            return Ok(newAccount);
        }


    }
}
