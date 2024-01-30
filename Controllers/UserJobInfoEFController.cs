using DotnetAPI.Data;
using DotnetAPI.DTOs;
using DotnetAPI.Models;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;

namespace DotnetAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class UserJobInfoEFController : ControllerBase
{
    DataContextEF _entityFramework;
    IMapper _mapper;
    public UserJobInfoEFController(IConfiguration config)
    {
        _entityFramework = new DataContextEF(config);

        _mapper = new Mapper(new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<UserJobInfoToAddDTO, UserJobInfo>();
        }));
    }

    [HttpGet("GetUserJobInfos")]
    public IEnumerable<UserJobInfo> GetUsers() // string[] can be replaced with IActionResult
    {
        IEnumerable<UserJobInfo> UserJobInfos = _entityFramework.UserJobInfo.ToList<UserJobInfo>();
        return UserJobInfos;
        // string[] responseArray = new string[] {
        //     "test1",
        //     "test2",
        //     testValue
        // };
        // return new OkObjectResult(responseArray); //Use ok()
    }

    [HttpGet("GetSingleUserJobInfo/{userId}")]
    public UserJobInfo GetSingleUserJobInfo(int userId)
    {

        UserJobInfo? UserJobInfos = _entityFramework.UserJobInfo
            .Where(u => u.UserId == userId) // sql equiv. 
            .FirstOrDefault<UserJobInfo>();

        if (UserJobInfos != null)
        {
            return UserJobInfos;
        }
        throw new Exception("Failed to Get User's Job Info");
    }

    [HttpPut("EditUserJobInfo")]
    public IActionResult EditUserJobInfo(UserJobInfo userJobInfo)
    {
        UserJobInfo? userDb = _entityFramework.UserJobInfo
            .Where(u => u.UserId == userJobInfo.UserId) // sql equiv. 
            .FirstOrDefault<UserJobInfo>();

        if (userDb != null)
        {
            userDb.JobTitle = userJobInfo.JobTitle;
            userDb.Department = userJobInfo.Department;
            if (_userRepository.SaveChanges())
            {
                return Ok();
            }
            throw new Exception("Failed to Update User's Job Info");
        }

        throw new Exception("Failed to Get the User's Job Info.");
    }

    [HttpPost("AddUserJobInfo")]
    public IActionResult AddUserJobInfo(UserJobInfoToAddDTO userJobInfo)
    {
        UserJobInfo userDb = _mapper.Map<UserJobInfo>(userJobInfo);

        _entityFramework.Add(userDb);
        if (_userRepository.SaveChanges())
        {
            return Ok();
        }
        throw new Exception("Failed to Add User's Job Info");

    }

    [HttpDelete("DeleteUserJobInfo/{userId}")]
    public IActionResult DeleteUserJobInfo(int userId)
    {
        UserJobInfo? userDb = _entityFramework.UserJobInfo
           .Where(u => u.UserId == userId) // sql equiv. 
           .FirstOrDefault<UserJobInfo>();

        if (userDb != null)
        {
            _entityFramework.UserJobInfo.Remove(userDb);
            if (_userRepository.SaveChanges())
            {
                return Ok();
            }
            throw new Exception("Failed to Update User's Job Info");
        }

        throw new Exception("Failed to Get User's Job Info");

    }
}

