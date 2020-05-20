using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

using CarMaintenance.Controllers;
using CarMaintenance.Managers.Car;
using CarMaintenance.Models;
using CarMaintenance.Models.Car;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using Moq;

using NUnit.Framework;


namespace CarMaintenanceUnitTests.ControllerTests
{
    internal class CarControllerTests
    {
        private Mock<ICarManager> _carManager;
        private Mock<ILogger<CarController>> _logger;
        private CarController _controller;

        [SetUp]
        public void SetUp()
        {
            _carManager = new Mock<ICarManager>();
            _logger = new Mock<ILogger<CarController>>();
            _controller = new CarController(_carManager.Object, _logger.Object);
            MockUserForController(ref _controller);
        }

        [Test]
        public void InsertCar_InvalidCarDetailsModel()
        {
            Task<object> result = _controller.InsertCar(new CarDetailsModel()
            {
                Name = null
            });
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Result, Is.TypeOf<BadRequestResult>());
        }

        [Test]
        public void InsertCar_Success()
        {
            _carManager.Setup(m => m.InsertCar(It.IsAny<CarDetails>())).ReturnsAsync(IdentityResult.Success);
            Task<object> result = _controller.InsertCar(new CarDetailsModel()
            {
                Name = "name",
                Details = "details",
                Year = 1998,
                LastRevisionDate = new Date(2000, 2, 2),
                LastRevisionKm = 123456,
                LastPti = new Date(2000, 2, 2),
                LastVig = new Date(2000, 2, 2),
                LastInsurance = new Date(2000, 2, 2)

            });
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Result, Is.TypeOf<IdentityResult>());
            _carManager.VerifyAll();
        }

        [Test]
        public void GetCarsByUserId_Fail()
        {
            _carManager.Setup(m => m.GetCarsByUserId(It.IsAny<string>())).Throws(new Exception());
            List<CarDetails> carDetails = _controller.GetCarsByUserId();
            Assert.That(carDetails, Is.Null);
        }

        [Test]
        public void GetCarsByUserId_Success()
        {
            _carManager.Setup(m => m.GetCarsByUserId(It.IsAny<string>())).Returns(new List<CarDetails>());
            List<CarDetails> carDetails = _controller.GetCarsByUserId();
            Assert.That(carDetails, Is.Not.Null);
            _carManager.VerifyAll();
        }

        [Test]
        public void RemoveCar_NullOrWhitespaceCarId([Values("", null)] string carId)
        {
            Task<object> result = _controller.RemoveCar(carId);
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Result, Is.TypeOf<BadRequestResult>());
        }

        [Test]
        public void RemoveCar_NotNumberId()
        {
            Task<object> result = _controller.RemoveCar("NotNumberHere");
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Result, Is.TypeOf<BadRequestResult>());
        }

        [Test]
        public void RemoveCar_Success()
        {
            _carManager.Setup(m => m.RemoveCar(It.IsAny<int>())).ReturnsAsync(IdentityResult.Success);
            Task<object> result = _controller.RemoveCar("10");
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Result, Is.TypeOf<IdentityResult>());
            _carManager.VerifyAll();
        }

        [Test]
        public void UpdateCar_NullCarUpdateModel()
        {
            Task<object> result = _controller.UpdateCar(null);
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Result, Is.TypeOf<BadRequestResult>());
        }

        [Test]
        public void UpdateCar_Success()
        {
            _carManager.Setup(m => m.UpdateCar(It.IsAny<string>(), It.IsAny<CarUpdateModel>())).ReturnsAsync(IdentityResult.Success);
            Task<object> result = _controller.UpdateCar(new CarUpdateModel()
            {
                ActualKilometers = 20000,
                LastPti = new DateTime(2000, 02, 02),
                LastVig = new DateTime(2000, 02, 02),
                LastInsurance = new DateTime(2000, 02, 02),
                CarId = 10
            });
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Result, Is.TypeOf<IdentityResult>());
            _carManager.VerifyAll();
        }

        private void MockUserForController(ref CarController controller)
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
