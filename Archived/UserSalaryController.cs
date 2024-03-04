// //!! 
// //!!
// //!! UserSalary
// //!!
// //!!

// using Microsoft.AspNetCore.Mvc;
// using DotnetAPI.Data;
// using DotnetAPI.Models;
// using DotnetAPI.DTOs;

// namespace DotnetAPI.Controllers;
// [ApiController]
// [Route("[controller]")]
// public class UserSalaryController : ControllerBase
// {
//     DataContextDapper _dapper;
//     public UserSalaryController(IConfiguration config)
//     {
//         _dapper = new DataContextDapper(config);
//     }


//     [HttpGet("GetSingleUserSalary/{userId}")]
//     public UserSalary GetSingleUserSalary(int userId)
//     {
//         string sql = @"
//                 SELECT [UserId],
//                     [Salary]
//                 FROM TutorialAppSchema.UserSalary
//                     WHERE UserId = " + userId.ToString();
//         UserSalary userSalary = _dapper.LoadDataSingle<UserSalary>(sql);
//         return userSalary;
//     }

//     [HttpDelete("DeleteUserSalary/{userId}")]
//     public IActionResult DeleteUserSalary(int userId)
//     {
//         string sql = @"
//             DELETE FROM TutorialAppSchema.UserSalary
//                 WHERE UserId = " + userId.ToString();

//         if (_dapper.ExecuteSql(sql))
//         {
//             return Ok();
//         }

//         throw new Exception("Failed to Delete User's Salary");

//     }
// }