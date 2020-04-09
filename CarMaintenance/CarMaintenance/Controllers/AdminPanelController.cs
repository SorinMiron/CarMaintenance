using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using CarMaintenance.Models;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

using Newtonsoft.Json;

using PainlessHttp.Serializer.JsonNet;


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
            //return bad request
           IList<ApplicationUser> customers =  await _userManager.GetUsersInRoleAsync("Customer");
           return customers.Select(customer => new CustomerModel(customer.Id, customer.UserName, customer.FullName, customer.Email)).ToList();
        }

        [HttpPost]
        [Authorize]
        [Route("RemoveCustomer")]
        //post /api/AdminPanel/RemoveCustomer
        public async Task<object> RemoveCustomer(object customerId)
        {
            //todo add validations
            //return bad request
            try {
                ApplicationUser customer = await _userManager.FindByIdAsync(customerId.ToString());
                return await _userManager.DeleteAsync(customer);
            } catch (Exception ex) {
                //handle exception
                return BadRequest(ex.Message);
            }
        }

    }
}