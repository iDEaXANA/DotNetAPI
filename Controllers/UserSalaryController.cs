//!! 
//!!
//!! UserSalary
//!!
//!!

using Microsoft.AspNetCore.Mvc;
using DotnetAPI.Data;
using DotnetAPI.Models;
using DotnetAPI.DTOs;

namespace DotnetAPI.Controllers;
[ApiController]
[Route("[controller]")]
public class UserSalaryController : ControllerBase
{
    DataContextDapper _dapper;
    public UserSalaryController(IConfiguration config)
    {
        _dapper = new DataContextDapper(config);
    }

    [HttpGet("GetUserSalary")]

    public IEnumerable<UserSalary> GetUserSalary()
    {
        string sql = @"
                SELECT [UserId],
                    [Salary]
                FROM TutorialAppSchema.UserSalary";
        IEnumerable<UserSalary> UserSalaries = _dapper.LoadData<UserSalary>(sql);
        return UserSalaries;

    }

    [HttpGet("GetSingleUserSalary/{userId}")]
    public UserSalary GetSingleUserSalary(int userId)
    {
        string sql = @"
                SELECT [UserId],
                    [Salary]
                FROM TutorialAppSchema.UserSalary
                    WHERE UserId = " + userId.ToString();
        UserSalary userSalary = _dapper.LoadDataSingle<UserSalary>(sql);
        return userSalary;
    }

    [HttpPut("EditUserSalary")]
    public IActionResult EditUserSalary(UserSalary UserSalary)
    {
        string sql = @"
            UPDATE TutorialAppSchema.UserSalary
                SET [Salary] = '" + UserSalary.Salary +
            "' WHERE UserId = " + UserSalary.UserId;

        if (_dapper.ExecuteSql(sql))
        {
            return Ok();
        }

        throw new Exception("Failed to Update User's Salary");

    }

    [HttpPost("AddUserSalary")]
    public IActionResult AddUserSalary(UserSalaryToAddDTO UserSalary)
    {
        string sql = @"INSERT INTO TutorialAppSchema.UserSalary(
                    [Salary]
                ) VALUES (" +
                "'" + UserSalary.Salary +
            "')";

        if (_dapper.ExecuteSqlWithRowCount(sql) > 0)
        {
            return Ok();
        }

        throw new Exception("Failed to Add User's Salary");
    }

    [HttpDelete("DeleteUserSalary/{userId}")]
    public IActionResult DeleteUserSalary(int userId)
    {
        string sql = @"
            DELETE FROM TutorialAppSchema.UserSalary
                WHERE UserId = " + userId.ToString();

        if (_dapper.ExecuteSql(sql))
        {
            return Ok();
        }

        throw new Exception("Failed to Delete User's Salary");

    }
}