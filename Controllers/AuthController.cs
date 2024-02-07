using System.Security.Cryptography;
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
                string sqlCheckUserExists = "SELECT * FROM TutorialAppSchema.Auth WHERE Email = '" +
                userForRegistration.Email + "'";
                IEnumerable<string> existingUsers = _dapper.LoadData<string>(sqlCheckUserExists);
                if (existingUsers.Count() == 0)
                {
                    byte[] passwordSalt = new byte[128 / 8];
                    using (RandomNumberGenerator rng = RandomNumberGenerator.Create())
                    {
                        rng.GetNonZeroBytes(passwordSalt);
                    }

                    string passwordSaltPlusString = _config.GetSection("AppSettings:PasswordKey"); //Retrieval
                    return Ok();
                }
                throw new Exception("User with this email already exists");
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