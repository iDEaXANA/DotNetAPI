using DotnetAPI.Data;
using DotnetAPI.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace DotnetAPI.Controllers
{
    public class AuthController : ControllerBase
    {
        private readonly DataContextDapper _dapper; // fields to connect (private)
        private readonly IConfiguration _config;
        public AuthController(IConfiguration config)// fields to use (public)
        {
            _dapper = new DataContextDapper(config);
            _config = config; // allows class to access settings/configurations
        }

        [HttpPost("Register")]
        public IActionResult Register(UserForRegistrationDTO userForRegistration)
        {
            if (userForRegistration.Password == userForRegistration.PasswordConfirm)
            {
                return Ok();
            }
            throw new Exception("Passwords do not match");
        }
        [HttpPost("Login")]
        public IActionResult Login(UserForLoginDTO userForLogin)
        {
            return Ok();
        }
    }
}