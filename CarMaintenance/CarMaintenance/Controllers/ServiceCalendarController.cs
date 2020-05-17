using System;
using System.Collections.Generic;
using System.Linq;

using CarMaintenance.Managers.Car;
using CarMaintenance.Managers.ServiceCalendar;
using CarMaintenance.Models.Car;
using CarMaintenance.Models.ServiceCalendar;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;


namespace CarMaintenance.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceCalendarController : ControllerBase
    {

        private readonly CarManager _carManager;
        private readonly ServiceCalendarManager _serviceCalendarManager;
        private readonly ILogger<ServiceCalendarController> _logger;

        public ServiceCalendarController(CarManager carManager, ServiceCalendarManager serviceCalendarManager, ILogger<ServiceCalendarController> logger)
        {
            _carManager = carManager;
            _serviceCalendarManager = serviceCalendarManager;
            _logger = logger;
        }

        [HttpGet]
        [Authorize(Roles = "Customer")]
        [Route("GetServiceCalendar")]
        //post /api/ServiceCalendar/GetServiceCalendar
        public List<ServiceCalendarModel> GetServiceCalendar()
        {
            try
            {
                string userId = User.Claims.First(c => c.Type == "UserID").Value;
                List<CarDetails> carsDetails = _carManager.GetCarsByUserId(userId);
                return _serviceCalendarManager.GetServiceCalendarModels(carsDetails);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error when get service calendar.");
                return null;
            }
        }

    }
}