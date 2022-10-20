using AspDotNetCoreApi6.Models;
using AspDotNetCoreApi6.Models.ViewModels;
using AspDotNetCoreApi6.Services;
using AspDotNetCoreApi6.Services.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace AspDotNetCoreApi6.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IUser _userService;
        private readonly IConfiguration _config;

        public AccountController(IUser userService, IConfiguration config)
        {
            _userService = userService;
            _config = config;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(UserModel user)
        {
            var status = await _userService.RegisterUser(user);
            if (status == Enums.Status.RegistrationComplete)
                return Ok("User Registered Successfully!");
            else if (status == Enums.Status.EmailAlreadyExist)
                return BadRequest("Email already exist!!!");

            return StatusCode(500);
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(Login login)
        {
            if (login == null) return BadRequest("Please enter valid username and password!!!");
            var status = await _userService.Login(login);

            if (status == Enums.Status.LoginSuccess)
            {
                TokenService tokenService = new TokenService(_config);
                return Ok(tokenService.GetToken(login.UserName));
            }

            else if (status == Enums.Status.UserNotFound) { 
                return NotFound("User does not exist");
            }

            return BadRequest("Please enter valid username and password!!!");
        }
    }
}
