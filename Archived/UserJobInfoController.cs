// //!! 
// //!!
// //!! UserJobInfo
// //!!
// //!!
// using Microsoft.AspNetCore.Mvc;
// using DotnetAPI.Data;
// using DotnetAPI.Models;
// using DotnetAPI.DTOs;

// namespace DotnetAPI.Controllers;
// [ApiController]
// [Route("[controller]")]
// public class UserJobInfoController : ControllerBase
// {
//     DataContextDapper _dapper;
//     public UserJobInfoController(IConfiguration config)
//     {
//         _dapper = new DataContextDapper(config);
//     }

//     [HttpGet("GetSingleUserJobInfo/{userId}")]
//     public UserJobInfo GetSingleUserJobInfo(int userId)
//     {
//         string sql = @"
//                 SELECT [UserId],
//                     [JobTitle],
//                     [Department]
//                 FROM TutorialAppSchema.UserJobInfo
//                     WHERE UserId = " + userId.ToString();
//         UserJobInfo userJobInfo = _dapper.LoadDataSingle<UserJobInfo>(sql);
//         return userJobInfo;
//     }

//     [HttpDelete("DeleteUserJobInfo/{userId}")]
//     public IActionResult DeleterUserJobInfo(int userId)
//     {
//         string sql = @"
//             DELETE FROM TutorialAppSchema.UserJobInfo
//                 WHERE UserId = " + userId.ToString();

//         if (_dapper.ExecuteSql(sql))
//         {
//             return Ok();
//         }

//         throw new Exception("Failed to Delete User's Job Info");

//     }
// }