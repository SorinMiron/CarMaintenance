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
    internal class UserProfileControllerTests
    {
        private Mock<UserManager<ApplicationUser>> _userManager;

        private Mock<ILogger<UserProfileController>> _logger;
        private UserProfileController _controller;

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
       
        private Mock<UserManager<ApplicationUser>> GetMockUserManager()
        {
            Mock<IUserStore<ApplicationUser>> userStoreMock = new Mock<IUserStore<ApplicationUser>>();
            return new Mock<UserManager<ApplicationUser>>(
                userStoreMock.Object, null, null, null, null, null, null, null, null);
        }
        private void MockUserForController(ref UserProfileController controller)
        {
            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Name, "example name"),
                new Claim(ClaimTypes.NameIdentifier, "1"),
                new Claim("UserID", "guid"),
            }, "mock"));

            controller.ControllerContext = new ControllerContext()
            {
                HttpContext = new DefaultHttpContext { User = user }
            };
        }
    }
}
