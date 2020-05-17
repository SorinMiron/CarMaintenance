using System;
using System.Linq;
using System.Threading.Tasks;

using CarMaintenance.Models.Customer;
using CarMaintenance.Models.User;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using NLog;

using ILogger = NLog.ILogger;


namespace CarMaintenance.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserProfileController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<UserProfileController> _logger;
        public UserProfileController(UserManager<ApplicationUser> userManager, ILogger<UserProfileController> logger)
        {
            _userManager = userManager;
            _logger = logger;
        }

        [HttpGet]
        [Authorize]
        //get /api/UserProfile
        public async Task<object> GetUserProfile()
        {
            try
            {
                string userId = User.Claims.First(c => c.Type == "UserID").Value;
                ApplicationUser user = await _userManager.FindByIdAsync(userId);
                return new CustomerModel(user.Id, user.UserName, user.FullName);
            }
            catch (Exception ex) {
                _logger.LogError(ex, "Error when get user profile.");
                return BadRequest();
            }
        }

    }
}