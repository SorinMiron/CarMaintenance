using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

using CarMaintenance.Managers.Car;
using CarMaintenance.Models.Car;
using CarMaintenance.Models.Customer;
using CarMaintenance.Models.User;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CarMaintenance.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarController : ControllerBase
    {
        private UserManager<ApplicationUser> _userManager;
        private CarManager _carManager;

        public CarController(UserManager<ApplicationUser> userManager, CarManager carManager)
        {
            _userManager = userManager;
            _carManager = carManager;
        }


        [HttpPost]

        [Route("InsertCar")]
        //post /api/Car/InsertCar
        public async Task<object> InsertCar(CarDetailsModel carDetails)
        {
            //todo add validations
            //return bad request
            try {
                string userId = User.Claims.First(c => c.Type == "UserID").Value;
                return await _carManager.InsertCar(new CarDetails(userId, carDetails));
            }
            catch (Exception ex) {

                return BadRequest(ex);
            }
        }


    }
}