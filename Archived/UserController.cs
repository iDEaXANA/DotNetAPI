// using Microsoft.AspNetCore.Mvc;
// using DotnetAPI.Data;
// using DotnetAPI.Models;
// using DotnetAPI.DTOs;

// namespace DotnetAPI.Controllers;
// [ApiController]
// [Route("[controller]")]
// public class UserController : ControllerBase
// {
//     DataContextDapper _dapper;
//     public UserController(IConfiguration config)
//     {
//         _dapper = new DataContextDapper(config);
//     }

//     // [HttpGet("TestConnection")]
//     // public DateTime TestConnection()
//     // {
//     //     return _dapper.LoadDataSingle<DateTime>("SELECT GETDATE()");
//     // }

//     [HttpGet("GetUsers")]
//     public IEnumerable<User> GetUsers() // string[] can be replaced with IActionResult
//     {
//         string sql = @"
//             SELECT [UserId],
//                 [FirstName],
//                 [LastName],
//                 [Email],
//                 [Gender],
//                 [Active] 
//             FROM TutorialAppSchema.Users";
//         IEnumerable<User> users = _dapper.LoadData<User>(sql);
//         return users;
//         // string[] responseArray = new string[] {
//         //     "test1",
//         //     "test2",
//         //     testValue
//         // };
//         // return new OkObjectResult(responseArray); //Use ok()
//     }

//     [HttpGet("GetSingleUser/{userId}")]
//     public User GetSingleUser(int userId)
//     {
//         string sql = @"
//             SELECT [UserId],
//                 [FirstName],
//                 [LastName],
//                 [Email],
//                 [Gender],
//                 [Active] 
//             FROM TutorialAppSchema.Users
//                 WHERE UserId = " + userId.ToString();
//         User users = _dapper.LoadDataSingle<User>(sql);
//         return users;
//     }

//     [HttpPut("EditUser")]
//     public IActionResult EditUser(User user)
//     {
//         string sql = @"
//         UPDATE TutorialAppSchema.Users
//             SET [FirstName] = '" + user.FirstName +
//                 "', [LastName] = '" + user.LastName +
//                 "', [Email] = '" + user.Email +
//                 "', [Gender] = '" + user.Gender +
//                 "', [Active] = '" + user.Active +
//             "' WHERE UserId = " + user.UserId;

//         Console.WriteLine(sql);

//         if (_dapper.ExecuteSql(sql))
//         {
//             return Ok();
//         }

//         throw new Exception("Failed to Update User");

//     }

//     [HttpPost("AddUser")]
//     public IActionResult AddUser(UserToAddDTO user)
//     {
//         string sql = @"INSERT INTO TutorialAppSchema.Users(
//                 [FirstName],
//                 [LastName],
//                 [Email],
//                 [Gender],
//                 [Active]
//             ) VALUES (" +
//                 "'" + user.FirstName +
//                 "', '" + user.LastName +
//                 "', '" + user.Email +
//                 "', '" + user.Gender +
//                 "', '" + user.Active +
//             "')";

//         if (_dapper.ExecuteSql(sql))
//         {
//             return Ok();
//         }

//         throw new Exception("Failed to Add User");
//     }


//     [HttpDelete("DeleteUser/{userId}")]
//     public IActionResult DeleteUser(int userId)
//     {
//         string sql = @"
//         DELETE FROM TutorialAppSchema.Users 
//             WHERE UserId = " + userId.ToString();

//         Console.WriteLine(sql);

//         if (_dapper.ExecuteSql(sql))
//         {
//             return Ok();
//         }

//         throw new Exception("Failed to Delete User");

//     }

// }

