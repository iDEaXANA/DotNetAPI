using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using AutoMapper;
using Dapper;
using DotnetAPI.Data;
using DotnetAPI.DTOs;
using DotnetAPI.Helpers;
using DotnetAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Tokens;

namespace DotnetAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly DataContextDapper _dapper; // fields to connect (private)
        private readonly AuthHelper _authHelper;

        private readonly ReusableSql _reusableSql;

        private readonly IMapper _mapper;

        public AuthController(IConfiguration config)// fields to use (public)
        {
            _dapper = new DataContextDapper(config);
            _authHelper = new AuthHelper(config);
            _reusableSql = new ReusableSql(config);
            _mapper = new Mapper(new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<UserForRegistrationDTO, UserComplete>();
            }));
        }
        [AllowAnonymous] // Bypasses Auth.
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
                    UserForLoginDTO userForSetPassword = new UserForLoginDTO()
                    {
                        Email = userForRegistration.Email,
                        Password = userForRegistration.Password
                    };
                    if (_authHelper.SetPassword(userForSetPassword))
                    {
                        UserComplete userComplete = _mapper.Map<UserComplete>(userForRegistration);
                        userComplete.Active = true;

                        // string sqlAddUser = @"
                        // EXEC TutorialAppSchema.spUser_Upsert
                        //     @FirstName = '" + userForRegistration.FirstName +
                        //     "', @LastName = '" + userForRegistration.LastName +
                        //     "', @Email = '" + userForRegistration.Email +
                        //     "', @Gender = '" + userForRegistration.Gender +
                        //     "', @Active = 1" +
                        //     ", @JobTitle = '" + userForRegistration.JobTitle +
                        //     "', @Department = '" + userForRegistration.Department +
                        //     "', @Salary = '" + userForRegistration.Salary + "'";
                        // // string sqlAddUser = @"INSERT INTO TutorialAppSchema.Users(
                        // //     [FirstName],
                        // //     [LastName],
                        // //     [Email],
                        // //     [Gender],
                        // //     [Active]
                        // // ) VALUES (" +
                        // //     "'" + userForRegistration.FirstName +
                        // //     "', '" + userForRegistration.LastName +
                        // //     "', '" + userForRegistration.Email +
                        // //     "', '" + userForRegistration.Gender +
                        // //     "', 1)";
                        if (_reusableSql.UpsertUser(userComplete))
                        {
                            return Ok();
                        }
                        throw new Exception("Failed to add user.");
                    }
                    throw new Exception("Failed to register user.");
                }
                throw new Exception("User with this email already exists");
            }
            throw new Exception("Passwords do not match.");
        }

        [HttpPut("ResetPassword")]
        public IActionResult ResetPassword(UserForLoginDTO userForSetPassword)
        {
            if (_authHelper.SetPassword(userForSetPassword))
            {
                return Ok();
            }
            throw new Exception("Failed to update password");
        }

        [AllowAnonymous]
        [HttpPost("Login")]
        public IActionResult Login(UserForLoginDTO userForLogin)
        {
            string sqlForHashAndSalt = @"EXEC TutorialAppSchema.spLoginConfirmation_Get 
            @Email = @EmailParam";

            DynamicParameters sqlParameters = new DynamicParameters();

            // SqlParameter emailParameter = new SqlParameter("@EmailParam", SqlDbType.VarChar);
            // emailParameter.Value = userForLogin.Email;
            // sqlParameters.Add(emailParameter);

            sqlParameters.Add("@EmailParam", userForLogin.Email, DbType.String);

            UserForLoginConfirmationDTO userForLoginConfirmation = _dapper
                .LoadDataSingleWithParameters<UserForLoginConfirmationDTO>(sqlForHashAndSalt, sqlParameters);

            byte[] passwordHash = _authHelper.GetPasswordHash(userForLogin.Password, userForLoginConfirmation.PasswordSalt);
            // if (passwordHash == userForLoginConfirmation.PasswordHash); won't work as they're objects.

            for (int index = 0; index < passwordHash.Length; index++)
            {
                if (passwordHash[index] != userForLoginConfirmation.PasswordHash[index])
                {
                    return StatusCode(401, "Incorrect password."); // equiv => throw new Exception
                }
            }

            string userIdSql = @"
                SELECT UserId FROM TutorialAppSchema.Users WHERE Email = '" +
                userForLogin.Email + "'";

            int userId = _dapper.LoadDataSingle<int>(userIdSql);

            return Ok(new Dictionary<string, string>
            {
                {"token", _authHelper.CreateToken(userId)}
            });
        }

        [HttpGet("RefreshToken")]
        public string RefreshToken()
        {
            string userIdSql = @"
                SELECT UserId FROM TutorialAppSchema.Users WHERE UserId = '" +
                User.FindFirst("userId")?.Value + "'";

            int userId = _dapper.LoadDataSingle<int>(userIdSql);

            return _authHelper.CreateToken(userId);
        }



    }
}