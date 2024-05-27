using DAL.DTO.AccountDTO;
using DAL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository.Implement;
using Repository.Interface;
using Service.Implement;
using Service.Interface;
using System.Text.Json.Serialization;

namespace jewelryauction.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly AccountService _accountService;
        private readonly AccountRepository _accountRepository;
        private readonly ILogger<LoginController> _logger;
        public LoginController(AccountService accountService, ILogger<LoginController> logger, AccountRepository accountRepository)
        {
            _accountService = accountService;
            _logger = logger;
            _accountRepository = accountRepository;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            
            var account = await _accountRepository.GetAccountByEmailAsync(request.Email);
            if (account == null || account.AccountPassword != request.Password)
            {
                return BadRequest("Invalid email or password.");
            }

            var accountInfo = new
            {
                
                Email = account.AccountEmail,
                Phone = account.AccountPhone,
                Name = account.AccountName,
                Role = account.RoleId
                
            };
            return Ok(accountInfo);
        }

        [HttpPost]
        [Route("register")]
        public  async Task<IActionResult> Register(RegisterAccountDTO registerAccount)
        {
            if (await _accountRepository.CheckExistingGmailAsync(registerAccount.AccountEmail))
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
    public class LoginRequest
    {
        [JsonPropertyName("email")]
        public string Email { get; set; }

        [JsonPropertyName("password")]
        public string Password { get; set; }
    }
    
    }

