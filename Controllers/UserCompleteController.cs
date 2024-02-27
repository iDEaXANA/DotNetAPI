using Microsoft.AspNetCore.Mvc;
using DotnetAPI.Data;
using DotnetAPI.Models;
using DotnetAPI.DTOs;

namespace DotnetAPI.Controllers;
[ApiController]
[Route("[controller]")]
public class UserCompleteController : ControllerBase
{
    DataContextDapper _dapper;
    public UserCompleteController(IConfiguration config)
    {
        _dapper = new DataContextDapper(config);
    }

    // [HttpGet("TestConnection")]
    // public DateTime TestConnection()
    // {
    //     return _dapper.LoadDataSingle<DateTime>("SELECT GETDATE()");
    // }

    [HttpGet("GetUsers/{userId}/{isActive}")]
    public IEnumerable<UserComplete> GetUsers(int userId, bool isActive) // string[] can be replaced with IActionResult
    {
        string sql = @"EXEC TutorialAppSchema.spUsers_Get";
        string paramaters = "";
        if (userId != 0)
        {
            paramaters += ", @UserId=" + userId.ToString();
        }
        if (isActive)
        {
            paramaters += ", @Active=" + isActive.ToString();
        }

        sql += paramaters.Substring(1); //, paramaters.Length);

        IEnumerable<UserComplete> users = _dapper.LoadData<UserComplete>(sql);
        return users;
        // string[] responseArray = new string[] {
        //     "test1",
        //     "test2",
        //     testValue
        // };
        // return new OkObjectResult(responseArray); //Use ok()
    }


    [HttpPut("UpsertUser")]
    public IActionResult EditUser(UserComplete user)
    {

        //, The following SP consolidates the POST/PUT requests!
        string sql = @"
        EXEC TutorialAppSchema.spUser_Upsert
                @FirstName = '" + user.FirstName +
                "', @LastName = '" + user.LastName +
                "', @Email = '" + user.Email +
                "', @Gender = '" + user.Gender +
                "', @Active = '" + user.Active +
                "', @JobTitle = '" + user.JobTitle +
                "', @Department = '" + user.Department +
                "', @Salary = '" + user.Salary +
                "',  @UserId = " + user.UserId;

        Console.WriteLine(sql);

        if (_dapper.ExecuteSql(sql))
        {
            return Ok();
        }

        throw new Exception("Failed to Update User");

    }

    [HttpDelete("DeleteUser/{userId}")]
    public IActionResult DeleteUser(int userId)
    {
        string sql = @"
        EXEC TutorialAppSchema.spUser_Delete
        @UserId = " + userId.ToString();

        Console.WriteLine(sql);

        if (_dapper.ExecuteSql(sql))
        {
            return Ok();
        }

        throw new Exception("Failed to Delete User");

    }

}

