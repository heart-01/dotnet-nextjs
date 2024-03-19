using Microsoft.AspNetCore.Mvc;
using WebApi.Models;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")] // api/user
public class UserController : ControllerBase
{
    private static readonly List<User> _users = new List<User>
    {
        new User { Id = 1, Username = "user1", Email = "user1@email.com", Fullname = "User One" },
        new User { Id = 2, Username = "user2", Email = "user2@email.com", Fullname = "User Two" },
    };

    // GET api/user
    [HttpGet]
    public ActionResult<IEnumerable<User>> GetUsers()
    {
        foreach (var user in _users)
        {
            Console.WriteLine($"Id: {user.Id}, Username: {user.Username}, Email: {user.Email}, Fullname: {user.Fullname}");
        };

        return Ok(_users);
    }

    // GET api/user/1
    [HttpGet("{id}")]
    public ActionResult<User> GetUser(int id)
    {
        var user = _users.Find(u => u.Id == id);
        if (user == null)
        {
            return NotFound();
        }
        return Ok(user);
    }

    // POST api/user
    [HttpPost]
    public ActionResult<User> CreateUser([FromBody] User user)
    {
        _users.Add(user);
        return CreatedAtAction(nameof(GetUser), new { id = user.Id }, user);
    }

    // PUT: api/user/{id}
    [HttpPut("{id}")]
    public IActionResult UpdateUser(int id, [FromBody] User user)
    {
        //Validate user id
        if (id != user.Id)
        {
            return BadRequest();
        }

        // Find existing user
        var existingUser = _users.Find(u => u.Id == id);
        if (existingUser == null)
        {
            return NotFound();
        }

        // Update user
        existingUser.Username = user.Username;
        existingUser.Email = user.Email;
        existingUser.Fullname = user.Fullname;

        // Return updated user
        return Ok(existingUser);
    }

    // DELETE: api/User/{id}
    [HttpDelete("{id}")]
    public ActionResult DeleteUser(int id)
    {
        var user = _users.Find(u => u.Id == id);

        if (user == null)
        {
            return NotFound();
        }

        _users.Remove(user);
        return NoContent();
    }
}