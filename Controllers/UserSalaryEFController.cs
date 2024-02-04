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
    IUserRepository _userRepository;
    IMapper _mapper;
    public UserSalaryEFController(IConfiguration config, IUserRepository userRepository)
    {
        _userRepository = userRepository;

        _mapper = new Mapper(new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<UserSalary, UserSalary>().ReverseMap();
        }));
    }

    [HttpGet("GetSingleUserSalary/{userId}")]
    public UserSalary GetSingleUserSalaryEF(int userId)
    {

        return _userRepository.GetSingleUserSalary(userId);
    }


    [HttpPost("UserSalary")] // Looks like he left PRIMARY KEY out and used base model instead. Only DTO = UserDTO.
    public IActionResult PostUserSalaryEF(UserSalary userToInsert)
    {
        // UserSalary userDb = _mapper.Map<UserSalary>(usersalary);

        _userRepository.AddEntity<UserSalary>(userToInsert);
        if (_userRepository.SaveChanges())
        {
            return Ok();
        }
        throw new Exception("Failed to Add User's Salary");

    }

    [HttpPut("UserSalary")]
    public IActionResult PutUserSalaryEF(UserSalary userForUpdate)
    {
        UserSalary? userToUpdate = _userRepository.GetSingleUserSalary(userForUpdate.UserId); // find HTTP Value against db and store it in userToUpdate

        if (userToUpdate != null)
        {
            _mapper.Map(userForUpdate, userToUpdate); // Map HTTP value to Db.
            if (_userRepository.SaveChanges())
            {
                return Ok();
            }
            throw new Exception("Updating process failed on save");
        }

        throw new Exception("Failed to Find User Salary to Update");
    }



    [HttpDelete("UserSalary/{userId}")]
    public IActionResult DeleteUserSalaryEF(int userId)
    {
        UserSalary? userToDelete = _userRepository.GetSingleUserSalary(userId);

        if (userToDelete != null)
        {
            _userRepository.RemoveEntity<UserSalary>(userToDelete);
            if (_userRepository.SaveChanges())
            {
                return Ok();
            }
            throw new Exception("Deleting process failed on save");
        }

        throw new Exception("Failed to Find User Salary to Delete");

    }
}
// cba to delete all the unneccessary apostrophes in the Exceptions.
