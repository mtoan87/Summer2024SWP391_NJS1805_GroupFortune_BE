using DAL.DTO.AccountDTO;
using DAL.Models;



using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Repository.Implement;
using Repository.Interface;
using Service.Implement;
using Service.Interface;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Json.Serialization;
using DAL.Authenticate;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;

namespace jewelryauction.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly IAccountService _accountService;      
        public LoginController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost]
        [Route("login")]
        public IActionResult Login(LoginModel request)
        {

            var account = _accountService.CheckLogin(request.AccountEmail, request.AccountPassword);
            if (account == null)
            {
                return BadRequest("Invalid Email or Password");
            }
            var accountInfo = new
            {

                AccountId = account.AccountId,
                Email = account.AccountEmail,
                Phone = account.AccountPhone,
                Name = account.AccountName,
                Role = account.RoleId

            };
            return Ok(accountInfo);
        }




        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register(RegisterAccountDTO registerAccount)
        {
            if (await _accountService.CheckExistingGmailAsync(registerAccount.AccountEmail))
            {
                return BadRequest("Email already exists.");
            }
            var account = _accountService.RegisterAccount(registerAccount);

            if (account == null)
            {
                return BadRequest("Register fail!");
            }

            return Ok(new { Message = "Register successful" });
        }


    }
}


