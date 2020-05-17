using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarMaintenance.Managers.Car;
using CarMaintenance.Models.Car;
using CarMaintenance.Models.Periodicity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;


namespace CarMaintenance.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarController : ControllerBase
    {
        private readonly ICarManager _carManager;
        private readonly ILogger<CarController> _logger;

        public CarController(ICarManager carManager, ILogger<CarController> logger)
        {
            _carManager = carManager;
            _logger = logger;
        }

        [HttpPost]
        [Authorize(Roles = "Customer")]
        [Route("InsertCar")]
        //post /api/Car/InsertCar
        public async Task<object> InsertCar(CarDetailsModel carDetails)
        {

            try
            {
                _carManager.ValidateCarDetailsModel(carDetails);
                string userId = User.Claims.First(c => c.Type == "UserID").Value;
                object result = await _carManager.InsertCar(new CarDetails(userId, carDetails, new CarPeriodicity()));
                _logger.LogInformation($"{carDetails.Name} was added.");
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Car {carDetails.Name} cannot be inserted.");
                return BadRequest();
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
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occured during getting cars.");
                return null;
            }
        }

        [HttpPost]
        [Authorize(Roles = "Customer")]
        [Route("RemoveCar")]
        //post /api/Car/RemoveCar
        public async Task<object> RemoveCar(object id)
        {
            try
            {
                if (id == null || string.IsNullOrWhiteSpace(id.ToString())) {
                    throw new ArgumentNullException(nameof(id));
                }
                object result =  await _carManager.RemoveCar(int.Parse(id.ToString()));
                _logger.LogInformation($"Car with following id was deleted: {int.Parse(id.ToString())}");
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Problem occured during removing car");
                return BadRequest();
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
                _carManager.ValidateCarUpdateModel(carUpdateModel);
                string userId = User.Claims.First(c => c.Type == "UserID").Value;
                object result = await _carManager.UpdateCar(userId, carUpdateModel);
                _logger.LogInformation($"Car with following id was updated: {carUpdateModel.CarId}");
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error on updating car.");
                return BadRequest();
            }
        }

    }

}