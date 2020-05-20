using CarMaintenance.Controllers;
using CarMaintenance.Managers.Car;
using CarMaintenance.Models.Periodicity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;


namespace CarMaintenanceUnitTests.ControllerTests
{
    class PeriodicityControllerTests
    {
        private Mock<ICarManager> _carManager;
        private Mock<ILogger<PeriodicityController>> _logger;
        private PeriodicityController _controller;

        [SetUp]
        public void SetUp()
        {
            _carManager = new Mock<ICarManager>();
            _logger = new Mock<ILogger<PeriodicityController>>();
            _controller = new PeriodicityController(_carManager.Object, _logger.Object);
            MockUserForController(ref _controller);
        }

        [Test]
        public void GetCarsPeriodicityByUserId_Fail()
        {
            _carManager.Setup(m => m.GetCarsPeriodicityByUserId(It.IsAny<string>())).Throws(new Exception());
            List<CarPeriodicityModel> result = _controller.GetCarsPeriodicityByUserId();
            Assert.That(result, Is.Null);
            _carManager.VerifyAll();
        }

        [Test]
        public void GetCarsPeriodicityByUserId_Success()
        {
            _carManager.Setup(m => m.GetCarsPeriodicityByUserId(It.IsAny<string>())).Returns(new List<CarPeriodicityModel>());
            List<CarPeriodicityModel> result = _controller.GetCarsPeriodicityByUserId();
            Assert.That(result, Is.Not.Null);
            _carManager.VerifyAll();
        }

        [Test]
        public void UpdatePeriodicity_InvalidCarPeriodicityModel()
        {
            _carManager.Setup(m => m.ValidateCarPeriodicityModel(It.IsAny<CarPeriodicityModel>())).Throws(new ArgumentNullException());
            Task<object> result = _controller.UpdatePeriodicity(new CarPeriodicityModel());
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Result, Is.TypeOf<BadRequestResult>());
            _carManager.VerifyAll();
        }

        [Test]
        public void UpdatePeriodicity_Success()
        {
            _carManager.Setup(m => m.ValidateCarPeriodicityModel(It.IsAny<CarPeriodicityModel>()));
            _carManager.Setup(m => m.UpdateCarPeriodicity(It.IsAny<string>(), It.IsAny<CarPeriodicityModel>())).ReturnsAsync(IdentityResult.Success);
            Task<object> result = _controller.UpdatePeriodicity(new CarPeriodicityModel()
            {
                CarNameAndYear = "Car name and year",
                CarId = 1,
                RevisionKm = 10000
            });
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Result, Is.TypeOf<IdentityResult>());
            _carManager.VerifyAll();
        }


        private void MockUserForController(ref PeriodicityController controller)
        {
            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Name, "example name"),
                new Claim(ClaimTypes.NameIdentifier, "1"),
                new Claim("UserID", "guid"),
            }, "mock"));

            controller.ControllerContext = new ControllerContext()
            {
                HttpContext = new DefaultHttpContext() { User = user }
            };
        }
    }
}
