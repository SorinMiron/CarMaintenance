using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

using CarMaintenance.Managers.Car;
using CarMaintenance.Models.Car;
using CarMaintenance.Models.Customer;
using CarMaintenance.Models.User;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;


namespace CarMaintenance.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminPanelController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ICarManager _carManager;
        private readonly ILogger<AdminPanelController> _logger;

        public AdminPanelController(UserManager<ApplicationUser> userManager, ICarManager carManager, ILogger<AdminPanelController> logger)
        {
            _userManager = userManager;
            _carManager = carManager;
            _logger = logger;
        }

        [HttpGet]
        [Authorize]
        [Route("GetCustomers")]
        //get /api/AdminPanel/Customers
        public async Task<object> GetCustomers()
        {
            try
            {
                IList<ApplicationUser> customers = await _userManager.GetUsersInRoleAsync("Customer");
                List<CustomerModel> customerModels = new List<CustomerModel>();
                foreach (ApplicationUser customer in customers)
                {
                    List<CarDetails> cars = _carManager.GetCarsByUserId(customer.Id);
                    StringBuilder carsAsString = new StringBuilder();
                    foreach (CarDetails car in cars)
                    {
                        carsAsString.Append($"{car.Name} {car.Year}");
                        carsAsString.Append(", ");
                    }
                    //remove last comma and space if cars were added
                    if (carsAsString.Length != 0)
                    {
                        carsAsString.Remove(carsAsString.Length - 2, 2);
                    }
                    customerModels.Add(new CustomerModel(customer.Id, customer.UserName, customer.FullName, customer.Email, carsAsString.ToString()));
                }
                _logger.LogInformation("Get customers was made successfully.");
                return customerModels;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error on get customers.");
                return BadRequest();
            }
        }

        [HttpPost]
        [Authorize]
        [Route("RemoveCustomer")]
        //post /api/AdminPanel/RemoveCustomer
        public async Task<object> RemoveCustomer(object customerId)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(customerId.ToString()) || string.IsNullOrEmpty(customerId.ToString()))
                {
                    throw new ArgumentNullException(nameof(customerId));
                }
                ApplicationUser customer = await _userManager.FindByIdAsync(customerId.ToString());
                _carManager.RemoveCarsByUserId(customerId.ToString());
                IdentityResult result = await _userManager.DeleteAsync(customer);
                if (!result.Succeeded) return BadRequest();
                _logger.LogInformation($"{customer.UserName} was deleted successfully");
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error on removing customer.");
                return BadRequest();
            }
        }

    }
}