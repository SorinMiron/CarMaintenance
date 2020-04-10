using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

using CarMaintenance.Models;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;


namespace CarMaintenance.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicationUserController : ControllerBase
    {
        private UserManager<ApplicationUser> _userManager;
        private SignInManager<ApplicationUser> _signInManager;
        private readonly ApplicationSettings _appSettings;

        public ApplicationUserController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IOptions<ApplicationSettings> appSettings)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _appSettings = appSettings.Value;
        }

        [HttpPost]
        [Route("Register")]
        //POST to /api/ApplicationUser/Register
        public async Task<Object> PostApplicationUser(ApplicationUserModel applicationUserModel)
        {
            //todo check if model is null or not
            //todo tests

            //default role on login: Customer
            applicationUserModel.Role = "Customer";
            ApplicationUser applicationUser = new ApplicationUser
            {
                UserName = applicationUserModel.UserName,
                Email = applicationUserModel.Email,
                FullName = applicationUserModel.FullName
            };

            try
            {
                var result = await _userManager.CreateAsync(applicationUser, applicationUserModel.Password);
                await _userManager.AddToRoleAsync(applicationUser, applicationUserModel.Role);
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        [Route("Login")]
        //POST to /api/ApplicationUser/Login
        public async Task<IActionResult> Login(LoginModel loginModel)
        {
            //todo check if model is null or not
            //todo tests
            ApplicationUser user = await _userManager.FindByNameAsync(loginModel.UserName);
            if (user != null && await _userManager.CheckPasswordAsync(user, loginModel.Password))
            {
                //Get the role
                IList<string> role = await _userManager.GetRolesAsync(user);
                IdentityOptions _options = new IdentityOptions();

                var tokenDescriptor = new SecurityTokenDescriptor {
                    Subject = new ClaimsIdentity(new[] {
                        new Claim("UserID", user.Id),
                        new Claim(_options.ClaimsIdentity.RoleClaimType, role.FirstOrDefault())
                    }),
                    Expires = DateTime.UtcNow.AddDays(_appSettings.Token_Expiration),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_appSettings.JWT_Secret)),SecurityAlgorithms.HmacSha256Signature)
                };
                JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
                SecurityToken securityToken = tokenHandler.CreateToken(tokenDescriptor);
                string token = tokenHandler.WriteToken(securityToken);
                return Ok(new { token, role });
            }

            return BadRequest(new { message = "Username or password is incorrect." });
        }

    }
}
