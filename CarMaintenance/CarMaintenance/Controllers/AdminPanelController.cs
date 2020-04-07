using System;
using System.Collections.Generic;
using System.Linq;
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
    public class AdminPanelController : ControllerBase
    {
        private UserManager<ApplicationUser> _userManager;

        public AdminPanelController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }


        [HttpGet]
        [Authorize]
        [Route("GetCustomers")]
        //get /api/AdminPanel/Customers
        public async Task<object> GetCustomers()
        {
            //todo add validations
           IList<ApplicationUser> customers =  await _userManager.GetUsersInRoleAsync("Customer");
           List<object> customersToReturn = new List<object>();
           foreach (ApplicationUser customer in customers) {
               customersToReturn.Add(new { customer.UserName, customer.FullName, customer.Email});
           }
           return customersToReturn;
        }
    }
}