// using DotnetAPI.Data;
// using DotnetAPI.DTOs;
// using DotnetAPI.Models;
// using Microsoft.AspNetCore.Mvc;
// using AutoMapper;

// namespace DotnetAPI.Controllers;

// [ApiController]
// [Route("[controller]")]
// public class UserJobInfoEFController : ControllerBase
// {
//     IUserRepository _userRepository;
//     IMapper _mapper;
//     public UserJobInfoEFController(IConfiguration config, IUserRepository userRepository)
//     {
//         _userRepository = userRepository;

//         _mapper = new Mapper(new MapperConfiguration(cfg =>
//         {
//             cfg.CreateMap<UserJobInfo, UserJobInfo>().ReverseMap();

//         }));
//     }

//     [HttpGet("GetUserJobInfo/{userId}")]
//     public UserJobInfo GetUserJobInfoEF(int userId)
//     {
//         return _userRepository.GetSingleUserJobInfo(userId);
//     }

//     [HttpPost("AddUserJobInfo")]
//     public IActionResult PostUserJobInfoEF(UserJobInfo userForInsert)
//     {
//         _userRepository.AddEntity<UserJobInfo>(userForInsert);

//         if (_userRepository.SaveChanges())
//         {
//             return Ok();
//         }
//         throw new Exception("Adding Process failed on save");

//     }

//     [HttpPut("UserJobInfo")]
//     public IActionResult PutUserJobInfoEF(UserJobInfo userForUpdate)
//     {
//         UserJobInfo? userToUpdate = _userRepository.GetSingleUserJobInfo(userForUpdate.UserId);

//         if (userToUpdate != null)
//         {
//             _mapper.Map(userForUpdate, userToUpdate);
//             if (_userRepository.SaveChanges())
//             {
//                 return Ok();
//             }
//             throw new Exception("Updating process failed on save");
//         }

//         throw new Exception("Failed to find userJobInfo to Update");
//     }



//     [HttpDelete("UserJobInfo/{userId}")]
//     public IActionResult DeleteUserJobInfoEF(int userId)
//     {
//         UserJobInfo? userToDelete = _userRepository.GetSingleUserJobInfo(userId);

//         if (userToDelete != null)
//         {
//             _userRepository.RemoveEntity<UserJobInfo>(userToDelete);
//             if (_userRepository.SaveChanges())
//             {
//                 return Ok();
//             }
//             throw new Exception("Failed to Update User's Job Info");
//         }

//         throw new Exception("Failed to Get User's Job Info");

//     }
// }

