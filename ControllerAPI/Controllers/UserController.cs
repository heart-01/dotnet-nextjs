using Microsoft.AspNetCore.Mvc;
using WebApi.Models;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")] // api/user
public class UserController : ControllerBase
{
    // GET api/user
    [HttpGet]
    public ActionResult<List<User>> GetUsers()
    {
        var users = new List<User> {
            new User { Id = 1, Username = "user1", Email = "user1@email.com", Fullname = "User One" },
        };

        return Ok(users);
    }
}