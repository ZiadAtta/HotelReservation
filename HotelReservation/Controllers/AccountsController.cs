using Core.Enum;
using HotelReservation.Core.Dtos;
using HotelReservation.Core.Entities;
using HotelReservation.Core.IServices;
using Infrastructure.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace HotelReservation.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ILogger<AccountsController> _logger;
        private readonly ITokenService _tokenService;
        private readonly AppDbContext _context;

        public AccountsController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, ILogger<AccountsController> logger, ITokenService tokenService, AppDbContext context)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _logger = logger;
            _tokenService = tokenService;
            _context = context;
        }

        [HttpPost("signup")]
        public async Task<IActionResult> Signup(SignupDto signupDto)
        {
            try
            {
                var existingUser = await _userManager.FindByNameAsync(signupDto.Email);
                if (existingUser != null)
                {
                    return BadRequest("User already exists");
                }

                // Create User role if it doesn't exist
                if ((await _roleManager.RoleExistsAsync(UserType.Customer.ToString())) == false)
                {
                    var roleResult = await _roleManager
                          .CreateAsync(new IdentityRole(UserType.Customer.ToString()));

                    if (roleResult.Succeeded == false)
                    {
                        var roleErros = roleResult.Errors.Select(e => e.Description);
                        _logger.LogError($"Failed to create user role. Errors : {string.Join(",", roleErros)}");
                        return BadRequest($"Failed to create user role. Errors : {string.Join(",", roleErros)}");
                    }
                }

                ApplicationUser user = new()
                {
                    Email = signupDto.Email,
                    SecurityStamp = Guid.NewGuid().ToString(),
                    UserName = signupDto.Email,
                    Name = signupDto.Name,
                    EmailConfirmed = true
                };

                // Attempt to create a user
                var createUserResult = await _userManager.CreateAsync(user, signupDto.Password);

                // Validate user creation. If user is not created, log the error and
                // return the BadRequest along with the errors
                if (createUserResult.Succeeded == false)
                {
                    var errors = createUserResult.Errors.Select(e => e.Description);
                    _logger.LogError(
                        $"Failed to create user. Errors: {string.Join(", ", errors)}"
                    );
                    return BadRequest($"Failed to create user. Errors: {string.Join(", ", errors)}");
                }

                // adding role to user
                var addUserToRoleResult = await _userManager.AddToRoleAsync(user: user, role: UserType.Customer.ToString());

                if (addUserToRoleResult.Succeeded == false)
                {
                    var errors = addUserToRoleResult.Errors.Select(e => e.Description);
                    _logger.LogError($"Failed to add role to the user. Errors : {string.Join(",", errors)}");
                }
                return CreatedAtAction(nameof(Signup), null);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

        }

        [HttpPost("signup-hotel-staff")]
        public async Task<IActionResult> SignupHotelStaff(SignupDto signupDto)
        {
            try
            {
                var existingUser = await _userManager.FindByNameAsync(signupDto.Email);
                if (existingUser != null)
                {
                    return BadRequest("User already exists");
                }

                // Create HotelStaff role if it doesn't exist
                if (!await _roleManager.RoleExistsAsync(UserType.HotelStaff.ToString()))
                {
                    var roleResult = await _roleManager.CreateAsync(
                        new IdentityRole(UserType.HotelStaff.ToString())
                    );

                    if (!roleResult.Succeeded)
                    {
                        var roleErrors = roleResult.Errors.Select(e => e.Description);
                        _logger.LogError($"Failed to create HotelStaff role. Errors: {string.Join(",", roleErrors)}");
                        return BadRequest($"Failed to create HotelStaff role. Errors: {string.Join(",", roleErrors)}");
                    }
                }

                var user = new ApplicationUser
                {
                    Email = signupDto.Email,
                    UserName = signupDto.Email,
                    Name = signupDto.Name,
                    SecurityStamp = Guid.NewGuid().ToString(),
                    EmailConfirmed = true
                };

                var createUserResult = await _userManager.CreateAsync(user, signupDto.Password);

                if (!createUserResult.Succeeded)
                {
                    var errors = createUserResult.Errors.Select(e => e.Description);
                    _logger.LogError($"Failed to create HotelStaff user. Errors: {string.Join(", ", errors)}");
                    return BadRequest($"Failed to create HotelStaff user. Errors: {string.Join(", ", errors)}");
                }

                var addToRoleResult = await _userManager.AddToRoleAsync(user, UserType.HotelStaff.ToString());

                if (!addToRoleResult.Succeeded)
                {
                    var errors = addToRoleResult.Errors.Select(e => e.Description);
                    _logger.LogError($"Failed to assign HotelStaff role. Errors: {string.Join(", ", errors)}");
                    return BadRequest($"Failed to assign HotelStaff role. Errors: {string.Join(", ", errors)}");
                }

                return CreatedAtAction(nameof(SignupHotelStaff), null);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            try
            {
                var user = await _userManager.FindByNameAsync(loginDto.Email); // return invalid name if this wrong
                var isAvalidPssword = await _userManager.CheckPasswordAsync(user, loginDto.Password); // return invalid password if this wrong
                if (user == null || !isAvalidPssword)
                {
                    return Unauthorized();
                }

                // for learning
                //List<Claim> authClaims = [
                //       new (ClaimTypes.Name, user.UserName),
                //       new (ClaimTypes.Email, "Ayman@gmail.com"),
                //       new (ClaimTypes.Role, "Admin"),
                //       new (JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),

                //       //custom claim 
                //       new Claim("Depattment","It")
                //// unique id for token
                //];

                // creating the necessary claims
                List<Claim> authClaims = [
                        new (ClaimTypes.Name, user.UserName),
                        new (JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                // unique id for token
                ];

                var userRoles = await _userManager.GetRolesAsync(user);

                // adding roles to the claims. So that we can get the user role from the token.
                foreach (var userRole in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                }

                // generating access token
                var token = _tokenService.GenerateAccessToken(authClaims);

                // refresh token
                string refreshToken = _tokenService.GenerateRefreshToken();

                var tokenInfo = _context.TokenInfos.
                   FirstOrDefault(a => a.Username == user.UserName);

                // If tokenInfo is null for the user, create a new one
                if (tokenInfo == null)
                {
                    var ti = new TokenInfo
                    {
                        Username = user.UserName,
                        RefreshToken = refreshToken,
                        ExpiredAt = DateTime.UtcNow.AddDays(7)
                    };
                    _context.TokenInfos.Add(ti);
                }
                // Else, update the refresh token and expiration
                else
                {
                    tokenInfo.RefreshToken = refreshToken;
                    tokenInfo.ExpiredAt = DateTime.UtcNow.AddDays(7);
                }
                await _context.SaveChangesAsync();

                return Ok(new TokenDto
                {
                    AccessToken = token,
                    RefreshToken = refreshToken
                });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost("token/refresh")]
        public async Task<IActionResult> Refresh(TokenDto tokenDto)
        {
            try
            {
                var principal = _tokenService.GetPrincipalFromExpiredToken(tokenDto.AccessToken);
                var username = principal.Identity.Name;

                var tokenInfo = _context.TokenInfos.SingleOrDefault(u => u.Username == username);
                if (tokenInfo == null
                    || tokenInfo.RefreshToken != tokenDto.RefreshToken
                    || tokenInfo.ExpiredAt <= DateTime.UtcNow)
                {
                    return BadRequest("Invalid refresh token. Please login again.");
                }

                var newAccessToken = _tokenService.GenerateAccessToken(principal.Claims);
                var newRefreshToken = _tokenService.GenerateRefreshToken();

                tokenInfo.RefreshToken = newRefreshToken; // rotating the refresh token
                await _context.SaveChangesAsync();

                return Ok(new TokenDto
                {
                    AccessToken = newAccessToken,
                    RefreshToken = newRefreshToken
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost("token/revoke")]
        [Authorize]
        public async Task<IActionResult> Revoke()
        {
            try
            {
                var username = User.Identity.Name;

                var userToken = _context.TokenInfos.SingleOrDefault(u => u.Username == username);
                if (userToken == null)
                {
                    return BadRequest();
                }

                userToken.RefreshToken = string.Empty;

                await _context.SaveChangesAsync();

                return Ok(true);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

        }
    }
}
