using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarMaintenance.Managers.Car;
using CarMaintenance.Models.Periodicity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;


namespace CarMaintenance.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PeriodicityController : ControllerBase
    {
        private readonly ICarManager _carManager;
        private readonly ILogger<PeriodicityController> _logger;

        public PeriodicityController(ICarManager carManager, ILogger<PeriodicityController> logger)
        {
            _carManager = carManager;
            _logger = logger;
        }

        [HttpGet]
        [Authorize(Roles = "Customer")]
        [Route("GetCarsPeriodicityByUserId")]
        //post /api/Periodicity/GetCarsPeriodicityByUserId
        public List<CarPeriodicityModel> GetCarsPeriodicityByUserId()
        {
            try
            {
                string userId = User.Claims.First(c => c.Type == "UserID").Value;
                return _carManager.GetCarsPeriodicityByUserId(userId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error on getting cars periodicity.");
                return null;
            }
        }

        [HttpPost]
        [Authorize(Roles = "Customer")]
        [Route("UpdatePeriodicity")]
        //post /api/Periodicity/UpdatePeriodicity
        public async Task<object> UpdatePeriodicity(CarPeriodicityModel carPeriodicityModel)
        {
            try
            {
                _carManager.ValidateCarPeriodicityModel(carPeriodicityModel);
                string userId = User.Claims.First(c => c.Type == "UserID").Value;
                object result = await _carManager.UpdateCarPeriodicity(userId, carPeriodicityModel);
                _logger.LogInformation($"Car with following ID was updated successfully: {carPeriodicityModel.CarId}");
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Update car failed.");
                return BadRequest();
            }
        }
    }


}