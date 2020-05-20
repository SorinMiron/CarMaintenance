using System;
using System.Security.Claims;
using System.Threading.Tasks;

using CarMaintenance.Controllers;
using CarMaintenance.Models.Customer;
using CarMaintenance.Models.User;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using Moq;

using NUnit.Framework;


namespace CarMaintenanceUnitTests.ControllerTests
{
    internal class UserProfileControllerTests : ControllerTestsShared<UserProfileController>
    {
        
        [SetUp]
        public void SetUp()
        {
            _userManager = GetMockUserManager();
            _logger = new Mock<ILogger<UserProfileController>>();
            _controller = new UserProfileController(_userManager.Object, _logger.Object);
            MockUserForController(ref _controller);
        }

        [Test]
        public void GetUserProfile_Fail()
        {
            _userManager.Setup(m => m.FindByIdAsync(It.IsAny<string>())).Throws(new Exception());
            Task<Object> result = _controller.GetUserProfile();
            _userManager.VerifyAll();
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Result, Is.TypeOf<BadRequestResult>());
        }

        [Test]
        public void GetUserProfile_Success()
        {
            _userManager.Setup(m => m.FindByIdAsync(It.IsAny<string>())).Returns(Task.FromResult(new ApplicationUser()));
            Task<Object> result = _controller.GetUserProfile();
            _userManager.VerifyAll();
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Result, Is.TypeOf<CustomerModel>());
        }
       
    }
}
