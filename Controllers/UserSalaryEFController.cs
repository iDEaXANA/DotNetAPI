using DotnetAPI.Data;
using DotnetAPI.DTOs;
using DotnetAPI.Models;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;

namespace DotnetAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class UserSalaryEFController : ControllerBase
{
    DataContextEF _entityFramework;
    IMapper _mapper;
    public UserSalaryEFController(IConfiguration config)
    {
        _entityFramework = new DataContextEF(config);

        _mapper = new Mapper(new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<UserSalaryToAddDTO, UserSalary>();
        }));
    }

    [HttpGet("GetUserSalaries")]
    public IEnumerable<UserSalary> GetUsers() // string[] can be replaced with IActionResult
    {
        IEnumerable<UserSalary> Usersalaries = _entityFramework.UserSalary.ToList<UserSalary>();
        return Usersalaries;
        // string[] responseArray = new string[] {
        //     "test1",
        //     "test2",
        //     testValue
        // };
        // return new OkObjectResult(responseArray); //Use ok()
    }

    [HttpGet("GetSingleUserSalary/{userId}")]
    public UserSalary GetSingleUserSalary(int userId)
    {

        UserSalary? usersalary = _entityFramework.UserSalary
            .Where(u => u.UserId == userId) // sql equiv. 
            .FirstOrDefault<UserSalary>();

        if (usersalary != null)
        {
            return usersalary;
        }
        throw new Exception("Failed to Get User's Salary");
    }

    [HttpPut("EditUserSalary")]
    public IActionResult EditUserSalary(UserSalary usersalary)
    {
        UserSalary? userDb = _entityFramework.UserSalary
            .Where(u => u.UserId == usersalary.UserId) // sql equiv. 
            .FirstOrDefault<UserSalary>();

        if (userDb != null)
        {
            userDb.Salary = usersalary.Salary; // Map it from usersalary to userDb
            if (_entityFramework.SaveChanges() > 0)
            {
                return Ok();
            }
            throw new Exception("Failed to Update User's Salary");
        }

        throw new Exception("Failed to Get User's Salary");
    }

    [HttpPost("AddUserSalary")] // Looks like he left PRIMARY KEY out and used base model instead. Only DTO = UserDTO.
    public IActionResult AddUserSalary(UserSalary usersalary)
    {
        // UserSalary userDb = _mapper.Map<UserSalary>(usersalary);

        _entityFramework.Add(usersalary);
        if (_entityFramework.SaveChanges() > 0)
        {
            return Ok();
        }
        throw new Exception("Failed to Add User's Salary");

    }

    [HttpDelete("DeleteUserSalary/{userId}")]
    public IActionResult DeleteUserSalary(int userId)
    {
        UserSalary? userDb = _entityFramework.UserSalary
           .Where(u => u.UserId == userId) // sql equiv. 
           .FirstOrDefault<UserSalary>();

        if (userDb != null)
        {
            _entityFramework.UserSalary.Remove(userDb);
            if (_entityFramework.SaveChanges() > 0)
            {
                return Ok();
            }
            throw new Exception("Failed to Update Users Salary");
        }

        throw new Exception("Failed to Get Users Salary");

    }
}
// cba to delete all the unneccessary apostrophes in the Exceptions.
