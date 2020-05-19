using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

using CarMaintenance.Controllers;
using CarMaintenance.Managers.Car;
using CarMaintenance.Models;
using CarMaintenance.Models.Car;
using CarMaintenance.Models.Customer;
using CarMaintenance.Models.Periodicity;
using CarMaintenance.Models.User;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using Moq;

using NUnit.Framework;


namespace CarMaintenanceUnitTests.ControllerTests
{
    class AdminPanelControllerTests
    {
        private Mock<UserManager<ApplicationUser>> _userManager;
        private Mock<ICarManager> _carManager;
        private Mock<ILogger<AdminPanelController>> _logger;
        private AdminPanelController _controller;

        [SetUp]
        public void SetUp()
        {
            _carManager = new Mock<ICarManager>();
            _userManager = GetMockUserManager();
            _logger = new Mock<ILogger<AdminPanelController>>();
            _controller = new AdminPanelController(_userManager.Object, _carManager.Object, _logger.Object);
        }
        [Test]
        public void GetCustomers_Success()
        {
            string userId = Guid.NewGuid().ToString();
            IList<ApplicationUser> customers = new List<ApplicationUser> { new ApplicationUser {
                UserName = "username",
                Email = "email",
                FullName = "fullname",
                Id = userId
            } };
            CarDetailsModel firstCarDetailsModel = new CarDetailsModel
            {
                Name = "name1",
                Year = 2010,
                Details = "details2",
                ActualKilometers = 200000,
                LastRevisionKm = 190000,
                LastRevisionDate = new Date(2019, 01, 01),
                LastPti = new Date(2019, 01, 01),
                LastVig = new Date(2019, 01, 01),
                LastInsurance = new Date(2019, 01, 01)
            };

            CarDetailsModel secondCarDetailsModel = new CarDetailsModel
            {
                Name = "name2",
                Year = 2008,
                Details = "details2",
                ActualKilometers = 200000,
                LastRevisionKm = 190000,
                LastRevisionDate = new Date(2019, 01, 01),
                LastPti = new Date(2019, 01, 01),
                LastVig = new Date(2019, 01, 01),
                LastInsurance = new Date(2019, 01, 01)
            };

            _userManager.Setup(m => m.GetUsersInRoleAsync(It.IsAny<string>())).Returns(Task.FromResult(customers)).Verifiable();
            _carManager.Setup(m => m.GetCarsByUserId(It.IsAny<string>())).Returns(new List<CarDetails> {
                new CarDetails(userId, firstCarDetailsModel, new CarPeriodicity()),
                new CarDetails(userId, secondCarDetailsModel, new CarPeriodicity())
            }).Verifiable();

            Task<object> result = _controller.GetCustomers();

            _userManager.VerifyAll();
            _carManager.VerifyAll();
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Result, Is.TypeOf<List<CustomerModel>>());

            List<CustomerModel> customerModels = result.Result as List<CustomerModel>;
            Assert.That(customerModels, Is.Not.Null);
            Assert.That(customerModels.Count, Is.EqualTo(1));

            Assert.That(customerModels[0].UserName, Is.EqualTo("username"));
            Assert.That(customerModels[0].FullName, Is.EqualTo("fullname"));
            Assert.That(customerModels[0].Email, Is.EqualTo("email"));
            Assert.That(customerModels[0].Id, Is.EqualTo(userId));
            Assert.That(customerModels[0].CarList, Is.EqualTo("name1 2010, name2 2008"));

        }

        [Test]
        public void GetCustomers_ReturnsBadRequest()
        {
            _userManager.Setup(m => m.GetUsersInRoleAsync(It.IsAny<string>())).Throws(new Exception()).Verifiable();

            AdminPanelController controller = new AdminPanelController(_userManager.Object, _carManager.Object, _logger.Object);
            Task<object> result = controller.GetCustomers();

            _userManager.VerifyAll();
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Result, Is.TypeOf<BadRequestResult>());
        }

        [Test]
        public void RemoveCustomer_Succeeded()
        {
            ApplicationUser customerForRemoving = new ApplicationUser { Id = Guid.NewGuid().ToString() };
            _userManager.Setup(m => m.FindByIdAsync(It.IsAny<string>())).Returns(Task.FromResult(customerForRemoving)).Verifiable();
            _carManager.Setup(m => m.RemoveCarsByUserId(It.IsAny<string>())).Verifiable();
            _userManager.Setup(m => m.DeleteAsync(It.IsAny<ApplicationUser>())).ReturnsAsync(IdentityResult.Success).Verifiable();

            Task<object> result = _controller.RemoveCustomer("customerId");

            _userManager.VerifyAll();
            _carManager.VerifyAll();

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Result, Is.Not.Null);
            Assert.That(result.Result, Is.TypeOf<IdentityResult>());

            IdentityResult identityResult = result.Result as IdentityResult;
            Assert.That(identityResult, Is.Not.Null);
            Assert.That(identityResult.Succeeded, Is.True);
        }

        [Test]
        public void RemoveCustomer_NullOrWhitespaceCustomerId([Values("", null)] string customerId)
        {
            Task<object> result = _controller.RemoveCustomer(customerId);

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Result, Is.Not.Null);
            Assert.That(result.Result, Is.TypeOf<BadRequestResult>());

        }

        [Test]
        public void RemoveCustomer_FailDelete()
        {
            ApplicationUser customerForRemoving = new ApplicationUser { Id = Guid.NewGuid().ToString() };
            _userManager.Setup(m => m.FindByIdAsync(It.IsAny<string>())).Returns(Task.FromResult(customerForRemoving)).Verifiable();
            _carManager.Setup(m => m.RemoveCarsByUserId(It.IsAny<string>())).Verifiable();
            _userManager.Setup(m => m.DeleteAsync(It.IsAny<ApplicationUser>())).ReturnsAsync(IdentityResult.Failed()).Verifiable();

            Task<object> result = _controller.RemoveCustomer("customerId");

            _userManager.VerifyAll();
            _carManager.VerifyAll();

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Result, Is.Not.Null);
            Assert.That(result.Result, Is.TypeOf<BadRequestResult>());
        }


        private Mock<UserManager<ApplicationUser>> GetMockUserManager()
        {
            Mock<IUserStore<ApplicationUser>> userStoreMock = new Mock<IUserStore<ApplicationUser>>();
            return new Mock<UserManager<ApplicationUser>>(
                userStoreMock.Object, null, null, null, null, null, null, null, null);
        }
    }
}
