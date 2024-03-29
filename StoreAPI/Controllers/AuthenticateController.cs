using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using StoreAPI.Models;

namespace StoreAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthenticateController : ControllerBase
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly IConfiguration _configuration;

    public AuthenticateController(
        UserManager<IdentityUser> userManager,
        RoleManager<IdentityRole> roleManager,
        IConfiguration configuration)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _configuration = configuration;
    }

    // Register for new user
    [HttpPost]
    [Route("register")]
    public async Task<IActionResult> Register([FromBody] Register registerData)
    {
        var userExists = await _userManager.FindByNameAsync(registerData.Username);
        if (userExists != null)
            return StatusCode(StatusCodes.Status500InternalServerError,
                new Response { Status = "Error", Message = "User already exists!" });

        IdentityUser user = new IdentityUser()
        {
            Email = registerData.Email,
            SecurityStamp = Guid.NewGuid().ToString(),
            UserName = registerData.Username
        };
        var result = await _userManager.CreateAsync(user, registerData.Password);
        await _userManager.AddToRoleAsync(user, UserRoles.User);

        if (!result.Succeeded)
            return StatusCode(StatusCodes.Status500InternalServerError,
                new Response
                    { Status = "Error", Message = "User creation failed! Please check user details and try again." });

        return Ok(new Response { Status = "Success", Message = "User created successfully!" });
    }

    // Register for admin
    [HttpPost]
    [Route("register-admin")]
    public async Task<IActionResult> RegisterAdmin([FromBody] Register model)
    {
        var userExists = await _userManager.FindByNameAsync(model.Username!);
        if (userExists != null)
            return StatusCode(
                StatusCodes.Status500InternalServerError,
                new Response
                {
                    Status = "Error",
                    Message = "User already exists!"
                }
            );

        IdentityUser user = new()
        {
            Email = model.Email,
            SecurityStamp = Guid.NewGuid().ToString(),
            UserName = model.Username
        };

        var result = await _userManager.CreateAsync(user, model.Password!);

        if (!result.Succeeded)
            return StatusCode(
                StatusCodes.Status500InternalServerError,
                new Response
                {
                    Status = "Error",
                    Message = "User creation failed! Please check user details and try again."
                }
            );

        if (!await _roleManager.RoleExistsAsync(UserRoles.Admin))
        {
            await _roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
            await _userManager.AddToRoleAsync(user, UserRoles.Admin);
        }
        else if (!await _roleManager.RoleExistsAsync(UserRoles.Manager))
        {
            await _roleManager.CreateAsync(new IdentityRole(UserRoles.Manager));
            await _userManager.AddToRoleAsync(user, UserRoles.Manager);
        }
        else if (!await _roleManager.RoleExistsAsync(UserRoles.User))
        {
            await _roleManager.CreateAsync(new IdentityRole(UserRoles.User));
            await _userManager.AddToRoleAsync(user, UserRoles.User);
        }

        return Ok(new Response { Status = "Success", Message = "User created successfully!" });
    }

    // Login
    [HttpPost]
    [Route("login")]
    public async Task<IActionResult> Login([FromBody] LoginModel model)
    {
        var user = await _userManager.FindByNameAsync(model.Username!);
        if (user is null)
        {
            return Unauthorized();
        }

        var isPasswordCorrect = await _userManager.CheckPasswordAsync(user, model.Password!);
        if (!isPasswordCorrect)
        {
            return Unauthorized();
        }

        // Check if user exists
        var userRoles = await _userManager.GetRolesAsync(user);
        var authClaims = new List<Claim>
        {
            new(ClaimTypes.Name, user.UserName ?? ""),
            new(ClaimTypes.NameIdentifier, user.Id),
            new("TokenID", Guid.NewGuid().ToString()),
        };

        foreach (var userRole in userRoles)
        {
            authClaims.Add(new Claim(ClaimTypes.Role, userRole));
        }

        var token = GenerateNewJsonWebToken(authClaims);

        return Ok(new
        {
            expiration = token.ValidTo,
            token = new JwtSecurityTokenHandler().WriteToken(token),
        });
    }

    private JwtSecurityToken GenerateNewJsonWebToken(List<Claim> claims)
    {
        var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]!));

        var token = new JwtSecurityToken(
            issuer: _configuration["JWT:ValidIssuer"],
            audience: _configuration["JWT:ValidAudience"],
            expires: DateTime.Now.AddDays(1),
            claims: claims,
            signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
        );
        return token;
    }

    // Logout
    [HttpPost]
    [Route("logout")]
    public async Task<IActionResult> Logout()
    {
        var userName = User.Identity?.Name;
        if (userName != null)
        {
            var user = await _userManager.FindByNameAsync(userName);
            if (user != null)
            {
                await _userManager.UpdateSecurityStampAsync(user);
                return Ok(new Response { Status = "Success", Message = "User logged out!" });
            }
        }

        return Ok();
    }
}