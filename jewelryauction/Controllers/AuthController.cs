using DAL.Authenticate;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace jewelryauction.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService;

        public AuthController(AuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel loginModel)
        {
            if (loginModel == null || string.IsNullOrEmpty(loginModel.AccountEmail) || string.IsNullOrEmpty(loginModel.AccountPassword))
            {
                return BadRequest("Invalid client request");
            }

            var account = await _authService.ValidateUserCredentials(loginModel.AccountEmail, loginModel.AccountPassword);
            if (account != null)
            {
                var tokenString = _authService.GenerateJwtToken(account);
                return Ok(new { Token = tokenString });
            }
            else
            {
                return Unauthorized();
            }
        }
    }
}
        
    
