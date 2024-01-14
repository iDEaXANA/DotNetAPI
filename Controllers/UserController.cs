using Microsoft.AspNetCore.Mvc;

namespace DotnetAPI.Controllers;
[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    DataContextDapper _dapper;
    public UserController(IConfiguration config)
    {
        _dapper = new DataContextDapper(config);
    }

    [HttpGet("TestConnection")]
    public DateTime TestConnection()
    {
        return _dapper.LoadDataSingle<DateTime>("SELECT GETDATE()");
    }

    [HttpGet("GetUsers/{testValue}")]
    public IActionResult GetUsers(string testValue) // string[] can be replaced with IActionResult
    {
        string[] responseArray = new string[] {
            "test1",
            "test2",
            testValue
        };
        return new OkObjectResult(responseArray); //Use ok()
    }

}

