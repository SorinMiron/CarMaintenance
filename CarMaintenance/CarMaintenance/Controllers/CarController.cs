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
        [Authorize(Roles = "Customer")]
        [Route("InsertCar")]
        //post /api/Car/InsertCar
        public async Task<object> InsertCar(CarDetailsModel carDetails)
        {
            //todo add validations
            //return bad request
            try
            {
                string userId = User.Claims.First(c => c.Type == "UserID").Value;
                return await _carManager.InsertCar(new CarDetails(userId, carDetails));
            }
            catch (Exception ex)
            {

                return BadRequest(ex);
            }
        }


        [HttpGet]
        [Authorize(Roles = "Customer")]
        [Route("GetCarsByUserId")]
        //post /api/Car/GetCars
        public List<CarDetails> GetCarsByUserId()
        {
            //todo add validations
            //return bad request
            try
            {
                string userId = User.Claims.First(c => c.Type == "UserID").Value;
                return _carManager.GetCarsByUserId(userId);
            }
            catch (Exception ex) {
                //todo log exception
                return null;
            }
        }


        [HttpPost]
        [Authorize(Roles = "Customer")]
        [Route("RemoveCar")]
        //post /api/Car/RemoveCar
        public async Task<object> RemoveCar(object id)
        {
            //todo add validations
            //return bad request
            try
            {
                return await _carManager.RemoveCar(int.Parse(id.ToString()));
            }
            catch (Exception ex)
            {
                //todo log exception
                return BadRequest(ex);
            }
        }


    }
}