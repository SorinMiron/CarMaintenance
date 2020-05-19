using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

using CarMaintenance.Models;
using CarMaintenance.Models.User;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;



namespace CarMaintenance.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicationUserController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationSettings _appSettings;
        private readonly ILogger<ApplicationUserController> _logger;

        public ApplicationUserController(UserManager<ApplicationUser> userManager, IOptions<ApplicationSettings> appSettings, ILogger<ApplicationUserController> logger)
        {
            _userManager = userManager;
            _appSettings = appSettings.Value;
            _logger = logger;

        }

        [HttpPost]
        [Route("Register")]
        //POST to /api/ApplicationUser/Register
        public async Task<object> PostApplicationUser(ApplicationUserModel applicationUserModel)
        {

            try
            {
                if (applicationUserModel == null)
                {
                    throw new ArgumentNullException(nameof(applicationUserModel));
                }
                if (string.IsNullOrWhiteSpace(applicationUserModel.UserName))
                {
                    throw new ArgumentNullException(nameof(applicationUserModel.UserName));
                }
                if (string.IsNullOrWhiteSpace(applicationUserModel.Password))
                {
                    throw new ArgumentNullException(nameof(applicationUserModel));
                }
                //default role on register: Customer
                applicationUserModel.Role = "Customer";
                ApplicationUser applicationUser = new ApplicationUser
                {
                    UserName = applicationUserModel.UserName,
                    Email = applicationUserModel.Email,
                    FullName = applicationUserModel.FullName
                };

                var result = await _userManager.CreateAsync(applicationUser, applicationUserModel.Password);
                if (!result.Succeeded)
                {
                    _logger.LogError($"Error on creating customer: {applicationUser.UserName}");
                    throw new Exception($"User {applicationUserModel.UserName} cannot be created.");
                }
                IdentityResult addRoleResult = await _userManager.AddToRoleAsync(applicationUser, applicationUserModel.Role);
                if (addRoleResult.Succeeded)
                {
                    _logger.LogInformation($"User {applicationUserModel.UserName} was created successfully.");
                    return Ok(result);
                }
                _logger.LogError($"Customer role cannot be assigned to {applicationUserModel.UserName}. Creating of user was failed.");
                return BadRequest();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error on creating customer.");
                return BadRequest();
            }
        }

        [HttpPost]
        [Route("Login")]
        //POST to /api/ApplicationUser/Login
        public async Task<IActionResult> Login(LoginModel loginModel)
        {
            try
            {
                if (loginModel == null)
                {
                    throw new ArgumentNullException(nameof(loginModel));
                }
                if (string.IsNullOrWhiteSpace(loginModel.UserName))
                {
                    throw new ArgumentNullException(nameof(loginModel.UserName));
                }
                if (string.IsNullOrWhiteSpace(loginModel.Password))
                {
                    throw new ArgumentNullException(nameof(loginModel.Password));
                }

                ApplicationUser user = await _userManager.FindByNameAsync(loginModel.UserName);
                if (user == null || !await _userManager.CheckPasswordAsync(user, loginModel.Password))
                {
                    _logger.LogError($"User {loginModel.UserName} tried to login but fails because username or password is incorrect.");
                    return BadRequest(new { message = "Username or password is incorrect." });
                }

                //Get the role
                IList<string> role = await _userManager.GetRolesAsync(user);
                IdentityOptions _options = new IdentityOptions();
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new[] {
                        new Claim("UserID", user.Id), new Claim(_options.ClaimsIdentity.RoleClaimType, role.FirstOrDefault())
                    }),
                    Expires = DateTime.UtcNow.AddDays(_appSettings.Token_Expiration),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_appSettings.JWT_Secret)),
                        SecurityAlgorithms.HmacSha256Signature)
                };
                JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
                SecurityToken securityToken = tokenHandler.CreateToken(tokenDescriptor);
                string token = tokenHandler.WriteToken(securityToken);
                _logger.LogInformation($"User {loginModel.UserName} logged in successfully.");
                return Ok(new { token, role });
            }
            catch (ArgumentNullException ex)
            {
                _logger.LogError(ex, "Error on login because of null parameters.");
                return BadRequest(new { message = "Errors occured on log in." });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Login of user {loginModel.UserName} not be performed successfully.");
                return BadRequest(new { message = "Errors occured on log in." });
            }

        }

    }
}
