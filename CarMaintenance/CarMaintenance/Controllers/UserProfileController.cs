using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;

using CarMaintenance.Models;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;


namespace CarMaintenance.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserProfileController : ControllerBase
    {
        private UserManager<ApplicationUser> _userManager;
        public UserProfileController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpGet]
        [Authorize]
        //get /api/UserProfile
        public async Task<object> GetUserProfile()
        {
            //todo add validations
            //return badrequest
            string userId = User.Claims.First(c => c.Type == "UserID").Value;
            ApplicationUser user = await _userManager.FindByIdAsync(userId);
            IList<string> roles = await _userManager.GetRolesAsync(user);
            string role = roles.FirstOrDefault();
            return new CustomerModel(user.Id, user.UserName, user.FullName, null, role);
        }

    }
}