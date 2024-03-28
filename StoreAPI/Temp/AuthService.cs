// using System.IdentityModel.Tokens.Jwt;
// using System.Net;
// using System.Security.Claims;
// using System.Security.Cryptography;
// using System.Text;
// using Microsoft.AspNetCore.Identity;
// using Microsoft.IdentityModel.Tokens;
// using StoreAPI.Core.Dtos.Request;
// using StoreAPI.Core.Dtos.Response;
// using StoreAPI.Core.Entities;
// using StoreAPI.Core.Interfaces;
// using StoreAPI.Models;
//
// namespace StoreAPI.Core.Services;
//
// public class AuthService : IAuthService
// {
//     private readonly UserManager<ApplicationUser> _userManager;
//     private readonly RoleManager<IdentityRole> _roleManager;
//     private readonly IConfiguration _configuration;
//
//     public AuthService(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration)
//     {
//         _userManager = userManager;
//         _roleManager = roleManager;
//         _configuration = configuration;
//     }
//
//     public async Task<AuthServiceResponse> LoginAsync(RequestLogin loginDto)
//     {
//         var user = await _userManager.FindByNameAsync(loginDto.UserName);
//
//         if (user is null)
//             return new AuthServiceResponse()
//             {
//                 IsSucceed = false,
//                 Status = HttpStatusCode.NotFound,
//                 Message = "Invalid Credentials"
//             };
//
//         var isPasswordCorrect = await _userManager.CheckPasswordAsync(user, loginDto.Password);
//
//         if (!isPasswordCorrect)
//             return new AuthServiceResponse()
//             {
//                 IsSucceed = false,
//                 Status = HttpStatusCode.NotFound,
//                 Message = "Invalid Credentials"
//             };
//
//         var userRoles = await _userManager.GetRolesAsync(user);
//
//         var authClaims = new List<Claim>
//             {
//                 new(ClaimTypes.Name, user.UserName??""),
//                 new(ClaimTypes.NameIdentifier, user.Id),
//                 new("TokenID", Guid.NewGuid().ToString()),
//                 new("FirstName", user.FirstName),
//                 new("LastName", user.LastName),
//             };
//
//         foreach (var userRole in userRoles)
//         {
//             authClaims.Add(new Claim(ClaimTypes.Role, userRole));
//         }
//
//         var token = GenerateNewJsonWebToken(authClaims);
//         var refreshToken = GenerateRefreshToken();
//         _ = int.TryParse(_configuration["JWT:RefreshTokenValidityInDays"], out int refreshTokenValidityInDays);
//         user.RefreshToken = refreshToken;
//         user.RefreshTokenExpiryTime = DateTime.Now.AddDays(refreshTokenValidityInDays).ToUniversalTime();
//         await _userManager.UpdateAsync(user);
//
//         return new AuthServiceResponse()
//         {
//             IsSucceed = true,
//             Status = HttpStatusCode.OK,
//             Message = "Login successful",
//             Token = new JwtSecurityTokenHandler().WriteToken(token),
//             RefreshToken = refreshToken
//         };
//     }
//
//     public async Task<AuthServiceResponse> RegisterAdminAsync(RequestUpdatePermission updatePermissionDto)
//     {
//         var user = await _userManager.FindByNameAsync(updatePermissionDto.UserName);
//
//         if (user is null)
//             return new AuthServiceResponse()
//             {
//                 IsSucceed = false,
//                 Status = HttpStatusCode.NotFound,
//                 Message = "Invalid User name. Please enter your name."
//             };
//
//         await _userManager.AddToRoleAsync(user, UserRoles.Admin);
//         return new AuthServiceResponse()
//         {
//             IsSucceed = true,
//             Status = HttpStatusCode.OK,
//             Message = "User is now an ADMIN"
//         };
//     }
//     public async Task<AuthServiceResponse> RegisterManagerAsync(RequestUpdatePermission updatePermission)
//     {
//         var user = await _userManager.FindByNameAsync(updatePermission.UserName);
//
//         if (user is null)
//             return new AuthServiceResponse()
//             {
//                 IsSucceed = false,
//                 Status = HttpStatusCode.NotFound,
//                 Message = "Invalid User name. Please enter your name."
//             };
//
//         await _userManager.AddToRoleAsync(user, UserRoles.Manager);
//         return new AuthServiceResponse()
//         {
//             IsSucceed = true,
//             Status = HttpStatusCode.OK,
//             Message = "User is now Manager"
//         };
//     }
//
//     public async Task<AuthServiceResponse> RegisterAsync(RequestRegister register)
//     {
//         var isExistsUser = await _userManager.FindByNameAsync(register.UserName);
//
//         if (isExistsUser != null)
//             return new AuthServiceResponse()
//             {
//                 IsSucceed = false,
//                 Status = HttpStatusCode.Conflict,
//                 Message = "UserName Already Exists"
//             };
//
//         ApplicationUser newUser = new ApplicationUser()
//         {
//             FirstName = register.FirstName,
//             LastName = register.LastName,
//             Email = register.Email,
//             UserName = register.UserName,
//             SecurityStamp = Guid.NewGuid().ToString(),
//         };
//
//         var createUserResult = await _userManager.CreateAsync(newUser, register.Password);
//
//         if (!createUserResult.Succeeded)
//         {
//             var errorString = "User Creation Failed Beacause: ";
//             foreach (var error in createUserResult.Errors)
//             {
//                 errorString += " # " + error.Description;
//             }
//             return new AuthServiceResponse()
//             {
//                 IsSucceed = false,
//                 Status = HttpStatusCode.NotAcceptable,
//                 Message = errorString
//             };
//         }
//         // Add a Default USER Role to all users
//
//         await _userManager.AddToRoleAsync(newUser, UserRoles.User);
//
//         return new AuthServiceResponse()
//         {
//             IsSucceed = true,
//             Status = HttpStatusCode.Created,
//             Message = "User Created Successfully"
//         };
//     }
//
//     public async Task<UserRoleResponse> SeedRolesAsync()
//     {
//         bool isUserRoleExists = await _roleManager.RoleExistsAsync(UserRoles.User);
//         bool isAdminRoleExists = await _roleManager.RoleExistsAsync(UserRoles.Admin);
//         bool isManagerRoleExists = await _roleManager.RoleExistsAsync(UserRoles.Manager);
//
//         if (isUserRoleExists && isAdminRoleExists && isManagerRoleExists)
//             return new UserRoleResponse()
//             {
//                 IsSucceed = true,
//                 Status = HttpStatusCode.OK,
//                 Message = "Roles Seeding is Already Done"
//             };
//
//         await _roleManager.CreateAsync(new IdentityRole(UserRoles.User));
//         await _roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
//         await _roleManager.CreateAsync(new IdentityRole(UserRoles.Manager));
//
//         return new UserRoleResponse()
//         {
//             IsSucceed = true,
//             Status = HttpStatusCode.Created,
//             Message = "Role Seeding Done Successfully"
//         };
//     }
//
//     private JwtSecurityToken GenerateNewJsonWebToken(List<Claim> claims)
//     {
//         var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("JWT:Secret").Value!));
//         _ = int.TryParse(_configuration["JWT:TokenValidityInMinutes"], out int tokenValidityInMinutes);
//
//         var token = new JwtSecurityToken(
//             issuer: _configuration["JWT:ValidIssuer"],
//             audience: _configuration["JWT:ValidAudience"],
//             expires: DateTime.Now.AddMinutes(tokenValidityInMinutes),
//             claims: claims,
//             signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
//             );
//
//         return token;
//     }
//     private static string GenerateRefreshToken()
//     {
//         var randomNumber = new byte[64];
//         using var rng = RandomNumberGenerator.Create();
//         rng.GetBytes(randomNumber);
//         return Convert.ToBase64String(randomNumber);
//     }
//
//     public async Task<TokenResponse> RefreshToken(RequestToken requestToken)
//     {
//         string? accessToken = requestToken.AccessToken;
//         string? refreshToken = requestToken.RefreshToken;
//
//         var principal = GetPrincipalFromExpiredToken(accessToken);
//         if (principal == null)
//         {
//             return new TokenResponse()
//             {
//                 IsSucceed = false,
//                 Status = HttpStatusCode.BadRequest,
//                 Message = "Invalid access token or refresh token"
//
//             };
//         }
//         string? userName = principal?.Identity?.Name;
// #pragma warning disable CS8604 // Possible null reference argument.
//         var user = await _userManager.FindByNameAsync(userName);
// #pragma warning restore CS8604 // Possible null reference argument.
//         if (user == null || user.RefreshToken != refreshToken || user.RefreshTokenExpiryTime <= DateTime.Now)
//         {
//             return new TokenResponse()
//             {
//                 IsSucceed = false,
//                 Status = HttpStatusCode.NotFound,
//                 Message = "Invalid User name or Invalid access token"
//             };
//         }
//
// #pragma warning disable CS8602 // Dereference of a possibly null reference.
//         var newAccessToken = GenerateNewJsonWebToken(principal.Claims.ToList());
// #pragma warning restore CS8602 // Dereference of a possibly null reference.
//         var newRefreshToken = GenerateRefreshToken();
//         user.RefreshToken = newRefreshToken;
//         await _userManager.UpdateAsync(user);
//         return new TokenResponse()
//         {
//             IsSucceed = true,
//             Status = HttpStatusCode.OK,
//             Message = "Create new refresh token successfully",
//             AccessToken = new JwtSecurityTokenHandler().WriteToken(newAccessToken),
//             RefreshToken = newRefreshToken
//         };
//     }
//     private ClaimsPrincipal? GetPrincipalFromExpiredToken(string? token)
//     {
//         var tokenValidationParameters = new TokenValidationParameters
//         {
//             ValidateAudience = false,
//             ValidateIssuer = false,
//             ValidateIssuerSigningKey = true,
//             IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("JWT:Secret").Value!)),
//             ValidateLifetime = false
//         };
//
//         var tokenHandler = new JwtSecurityTokenHandler();
//         var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);
//         if (securityToken is not JwtSecurityToken jwtSecurityToken || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
//             throw new SecurityTokenException("Invalid token");
//
//         return principal;
//     }
//
//     public async Task<RevokeResponse> RevokeAll()
//     {
//         var users = _userManager.Users.ToList();
//         foreach (var user in users)
//         {
//             user.RefreshToken = null;
//             await _userManager.UpdateAsync(user);
//         }
//         return new RevokeResponse()
//         {
//             IsSucceed = true,
//             Status = HttpStatusCode.OK,
//             Message = "Refresh token has been revoked"
//         };
//     }
//
//     public async Task<RevokeResponse> Revoke(string username)
//     {
//         var user = await _userManager.FindByNameAsync(username);
//         if (user == null) return new RevokeResponse()
//         {
//             IsSucceed = false,
//             Status = HttpStatusCode.NotFound,
//             Message = "Invalid user name"
//         }; //return BadRequest("Invalid user name");
//
//         user.RefreshToken = null;
//         await _userManager.UpdateAsync(user);
//
//         return new RevokeResponse()
//         {
//             IsSucceed = true,
//             Status = HttpStatusCode.OK,
//             Message = "Refresh token has been revoked"
//         };
//     }
//
//
// }