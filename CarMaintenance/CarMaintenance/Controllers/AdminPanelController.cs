using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CarMaintenance.Managers.Car;
using CarMaintenance.Models.Car;
using CarMaintenance.Models.Customer;
using CarMaintenance.Models.User;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CarMaintenance.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminPanelController : ControllerBase
    {
        private UserManager<ApplicationUser> _userManager;
        private CarManager _carManager;

        public AdminPanelController(UserManager<ApplicationUser> userManager, CarManager carManager)
        {
            _userManager = userManager;
            _carManager = carManager;
        }

        [HttpGet]
        [Authorize]
        [Route("GetCustomers")]
        //get /api/AdminPanel/Customers
        public async Task<object> GetCustomers()
        {
            //todo add validations
            //return bad request
            IList<ApplicationUser> customers = await _userManager.GetUsersInRoleAsync("Customer");
            List<CustomerModel> customerModels = new List<CustomerModel>();
            foreach (ApplicationUser customer in customers) {
                List<CarDetails> cars = _carManager.GetCarsByUserId(customer.Id);
                StringBuilder carsAsString = new StringBuilder();
                foreach (CarDetails car in cars) {
                    carsAsString.Append($"{car.Name} {car.Year} ,");
                }
                //remove last comma if cars were added
                if (carsAsString.Length != 0) {
                    carsAsString.Remove(carsAsString.Length - 1, 1);
                }
                customerModels.Add(new CustomerModel(customer.Id, customer.UserName, customer.FullName, customer.Email, carsAsString.ToString()));
            }
            return customerModels;
        }

        [HttpPost]
        [Authorize]
        [Route("RemoveCustomer")]
        //post /api/AdminPanel/RemoveCustomer
        public async Task<object> RemoveCustomer(object customerId)
        {
            //todo add validations
            //return bad request
            try
            {
                ApplicationUser customer = await _userManager.FindByIdAsync(customerId.ToString());
                return await _userManager.DeleteAsync(customer);
            }
            catch (Exception ex)
            {
                //handle exception
                return BadRequest(ex.Message);
            }
        }

    }
}