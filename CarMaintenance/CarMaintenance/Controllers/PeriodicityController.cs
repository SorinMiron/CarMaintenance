using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using CarMaintenance.Managers.Car;
using CarMaintenance.Models.Periodicity;
using CarMaintenance.Models.User;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarMaintenance.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PeriodicityController : ControllerBase
    {
        private readonly CarManager _carManager;

        public PeriodicityController(CarManager carManager)
        {
            _carManager = carManager;
        }

        [HttpGet]
        [Authorize(Roles = "Customer")]
        [Route("GetCarsPeriodicityByUserId")]
        //post /api/Periodicity/GetCarsPeriodicityByUserId
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
        //post /api/Periodicity/UpdatePeriodicity
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