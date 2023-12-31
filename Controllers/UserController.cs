using Microsoft.AspNetCore.Mvc;

namespace DotnetAPI.Controllers;
[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    public UserController()
    {

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

