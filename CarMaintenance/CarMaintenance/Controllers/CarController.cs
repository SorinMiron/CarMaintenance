using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarMaintenance.Managers.Car;
using CarMaintenance.Models.Car;
using CarMaintenance.Models.Periodicity;
using CarMaintenance.Models.User;
using Microsoft.AspNetCore.Authorization;
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
                return await _carManager.InsertCar(new CarDetails(userId, carDetails, new CarPeriodicity()));
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

        [HttpGet]
        [Authorize(Roles = "Customer")]
        [Route("GetCarsPeriodicityByUserId")]
        //post /api/Car/GetCarsPeriodicityByUserId
        public List<CarPeriodicityModel> GetCarsPeriodicityByUserId()
        {
            //todo add validations
            //return bad request
            try
            {
                string userId = User.Claims.First(c => c.Type == "UserID").Value;
                return _carManager.GetCarsPeriodicityByUserId(userId);
            }
            catch (Exception ex)
            {
                //todo log exception
                return null;
            }
        }

        [HttpPost]
        [Authorize(Roles = "Customer")]
        [Route("UpdatePeriodicity")]
        //post /api/Car/UpdatePeriodicity
        public async Task<object> UpdatePeriodicity(CarPeriodicityModel carPeriodicityModel)
        {
            //todo add server-side validations: numerical(1.000 - 50.000 , months: 1-36)
            //return bad request
            try
            { 
                string userId = User.Claims.First(c => c.Type == "UserID").Value;
                return await _carManager.UpdateCarPeriodicity(userId, carPeriodicityModel);
            }
            catch (Exception ex)
            {

                return BadRequest(ex);
            }
        }
    }

}