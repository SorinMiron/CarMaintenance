using System;
using System.Collections.Generic;
using System.Linq;

using CarMaintenance.Managers.Car;
using CarMaintenance.Managers.ServiceCalendar;
using CarMaintenance.Models.Car;
using CarMaintenance.Models.ServiceCalendar;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarMaintenance.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceCalendarController : ControllerBase
    {

        private readonly CarManager _carManager;
        private readonly ServiceCalendarManager _serviceCalendarManager;

        public ServiceCalendarController(CarManager carManager, ServiceCalendarManager serviceCalendarManager)
        {
            _carManager = carManager;
            _serviceCalendarManager = serviceCalendarManager;
        }

        [HttpGet]
        [Authorize(Roles = "Customer")]
        [Route("GetServiceCalendar")]
        //post /api/ServiceCalendar/GetServiceCalendar
        public List<ServiceCalendarModel> GetServiceCalendar()
        {
            //todo add validations
            //return bad request
            try
            {
                string userId = User.Claims.First(c => c.Type == "UserID").Value;
                List<CarDetails> carsDetails = _carManager.GetCarsByUserId(userId);
                //todo check periodicity, check carDetails
                return _serviceCalendarManager.GetServiceCalendarModels(carsDetails);
            }
            catch (Exception ex)
            {
                //todo log exception
                return null;
            }
        }

    }
}