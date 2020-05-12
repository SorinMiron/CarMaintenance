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
        private readonly CarManager _carManager;

        public CarController(CarManager carManager)
        {
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

        [HttpPost]
        [Authorize(Roles = "Customer")]
        [Route("UpdateCar")]
        //post /api/Car/UpdateCar
        public async Task<object> UpdateCar(CarUpdateModel carUpdateModel)
        {
            try
            {
                string userId = User.Claims.First(c => c.Type == "UserID").Value;
                return await _carManager.UpdateCar(userId, carUpdateModel);
            }
            catch (Exception ex)
            {

                return BadRequest(ex);
            }
        }
    }

}