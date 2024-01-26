//!! 
//!!
//!! UserJobInfo
//!!
//!!
using Microsoft.AspNetCore.Mvc;
using DotnetAPI.Data;
using DotnetAPI.Models;
using DotnetAPI.DTOs;

namespace DotnetAPI.Controllers;
[ApiController]
[Route("[controller]")]
public class UserJobInfoController : ControllerBase
{
    DataContextDapper _dapper;
    public UserJobInfoController(IConfiguration config)
    {
        _dapper = new DataContextDapper(config);
    }

    [HttpGet("GetUsersJobInfo")]

    public IEnumerable<UserJobInfo> GetUsersJobInfo()
    {
        string sql = @"
                SELECT [UserId],
                    [JobTitle],
                    [Department]
                FROM TutorialAppSchema.UserJobInfo";
        IEnumerable<UserJobInfo> userJobInfos = _dapper.LoadData<UserJobInfo>(sql);
        return userJobInfos;

    }

    [HttpGet("GetSingleUserJobInfo/{userId}")]
    public UserJobInfo GetSingleUserJobInfo(int userId)
    {
        string sql = @"
                SELECT [UserId],
                    [JobTitle],
                    [Department]
                FROM TutorialAppSchema.UserJobInfo
                    WHERE UserId = " + userId.ToString();
        UserJobInfo userJobInfo = _dapper.LoadDataSingle<UserJobInfo>(sql);
        return userJobInfo;
    }

    [HttpPut("EditUserJobInfo")]
    public IActionResult EditUserJobInfo(UserJobInfo userJobInfo)
    {
        string sql = @"
            UPDATE TutorialAppSchema.UserJobInfo
                SET [JobTitle] = '" + userJobInfo.JobTitle +
                "', [Department] = '" + userJobInfo.Department +
            "' WHERE UserId = " + userJobInfo.UserId;

        if (_dapper.ExecuteSql(sql))
        {
            return Ok();
        }

        throw new Exception("Failed to Update User's Job Info");

    }

    [HttpPost("AddUserJobInfo")]
    public IActionResult AddUserJobInfo(UserJobInfoToAddDTO userJobInfo)
    {
        string sql = @"INSERT INTO TutorialAppSchema.UserJobInfo(
                    [JobTitle],
                    [Department]
                ) VALUES (" +
                "'" + userJobInfo.JobTitle +
                "', '" + userJobInfo.Department +
            "')";

        if (_dapper.ExecuteSql(sql))
        {
            return Ok();
        }

        throw new Exception("Failed to Add User's Job Info");
    }

    [HttpDelete("DeleteUserJobInfo/{userId}")]
    public IActionResult DeleterUserJobInfo(int userId)
    {
        string sql = @"
            DELETE FROM TutorialAppSchema.UserJobInfo
                WHERE UserId = " + userId.ToString();

        if (_dapper.ExecuteSql(sql))
        {
            return Ok();
        }

        throw new Exception("Failed to Delete User's Job Info");

    }
}