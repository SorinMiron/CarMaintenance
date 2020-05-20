using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

using CarMaintenance.Controllers;
using CarMaintenance.Managers.Car;
using CarMaintenance.Managers.ServiceCalendar;
using CarMaintenance.Models.Car;
using CarMaintenance.Models.Periodicity;
using CarMaintenance.Models.ServiceCalendar;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using Moq;

using NUnit.Framework;


namespace CarMaintenanceUnitTests.ControllerTests
{
    internal class ServiceCalendarControllerTests : ControllerTestsShared<ServiceCalendarController>
    {
        [SetUp]
        public void SetUp()
        {
            _carManager = new Mock<ICarManager>();
            _serviceCalendarManager = new Mock<IServiceCalendarManager>();
            _logger = new Mock<ILogger<ServiceCalendarController>>();
            _controller = new ServiceCalendarController(_carManager.Object, _serviceCalendarManager.Object, _logger.Object);
            MockUserForController(ref _controller);
        }

        [Test]
        public void GetServiceCalendar_Fail()
        {
            _carManager.Setup(m => m.GetCarsByUserId(It.IsAny<string>())).Throws(new Exception());
            List<ServiceCalendarModel> result = _controller.GetServiceCalendar();
            Assert.That(result, Is.Null);
            _carManager.VerifyAll();
        }

        [Test]
        public void GetServiceCalendar_Success()
        {
            _carManager.Setup(m => m.GetCarsByUserId(It.IsAny<string>())).Returns(new List<CarDetails>());
            _serviceCalendarManager.Setup(m => m.GetServiceCalendarModels(It.IsAny<List<CarDetails>>()))
                .Returns(new List<ServiceCalendarModel>());
            List<ServiceCalendarModel> result = _controller.GetServiceCalendar();
            Assert.That(result, Is.Not.Null);
            _carManager.VerifyAll();
        }

    }
}
