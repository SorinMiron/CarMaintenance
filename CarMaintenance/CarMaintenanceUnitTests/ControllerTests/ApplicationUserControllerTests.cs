using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

using CarMaintenance.Controllers;
using CarMaintenance.Models;
using CarMaintenance.Models.User;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

using Moq;
using NUnit.Framework;

namespace CarMaintenanceUnitTests.ControllerTests
{
    internal class ApplicationUserControllerTests : ControllerTestsShared<ApplicationUserController>
    {

        [SetUp]
        public void SetUp()
        {
            _appSettings = new Mock<IOptions<ApplicationSettings>>();
            ApplicationSettings appSettings = new ApplicationSettings { JWT_Secret = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIxMjM0NTY3ODkwIiwibmFtZSI6IkpvaG4gRG9lIiwiaWF0IjoxNTE2MjM5MDIyfQ.SflKxwRJSMeKKF2QT4fwpMeJf36POk6yJV_adQssw5c", Token_Expiration = 30 };
            _appSettings.Setup(m => m.Value).Returns(appSettings);

            _userManager = GetMockUserManager();
            _logger = new Mock<ILogger<ApplicationUserController>>();
            _controller = new ApplicationUserController(_userManager.Object, _appSettings.Object, _logger.Object);

        }

        [Test]
        public void PostApplicationUser_NullApplicationUserModel()
        {
            Task<object> result = _controller.PostApplicationUser(null);
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Result, Is.TypeOf<BadRequestResult>());

        }
        [Test]
        public void PostApplicationUser_NullOrWhitespaceUsername([Values(null, "")] string userName)
        {
            Task<object> result = _controller.PostApplicationUser(new ApplicationUserModel()
            {
                UserName = userName,
                Password = "password"
            });
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Result, Is.TypeOf<BadRequestResult>());
        }
        [Test]
        public void PostApplicationUser_NullOrWhitespacePassword([Values(null, "")] string password)
        {
            Task<object> result = _controller.PostApplicationUser(new ApplicationUserModel()
            {
                UserName = "username",
                Password = password
            });
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Result, Is.TypeOf<BadRequestResult>());
        }

        [Test]
        public void PostApplicationUser_ErrorOnCreateUser([Values(null, "")] string password)
        {
            _userManager.Setup(m => m.CreateAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>())).ReturnsAsync(IdentityResult.Failed()).Verifiable();
            Task<object> result = _controller.PostApplicationUser(new ApplicationUserModel()
            {
                UserName = "username",
                Password = "password"
            });
            _userManager.VerifyAll();
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Result, Is.TypeOf<BadRequestResult>());
        }

        [Test]
        public void PostApplicationUser_ErrorOnAddRole()
        {
            _userManager.Setup(m => m.CreateAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>())).ReturnsAsync(IdentityResult.Success).Verifiable();
            _userManager.Setup(m => m.AddToRoleAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>())).ReturnsAsync(IdentityResult.Failed()).Verifiable();
            Task<object> result = _controller.PostApplicationUser(new ApplicationUserModel()
            {
                UserName = "username",
                Password = "password"
            });
            _userManager.VerifyAll();
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Result, Is.TypeOf<BadRequestResult>());
        }

        [Test]
        public void PostApplicationUser_Success()
        {
            _userManager.Setup(m => m.CreateAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>())).ReturnsAsync(IdentityResult.Success).Verifiable();
            _userManager.Setup(m => m.AddToRoleAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>())).ReturnsAsync(IdentityResult.Success).Verifiable();
            Task<object> result = _controller.PostApplicationUser(new ApplicationUserModel()
            {
                UserName = "username",
                Password = "password"
            });
            _userManager.VerifyAll();
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Result, Is.TypeOf<OkObjectResult>());
        }

        [Test]
        public void Login_NullLoginModel()
        {
            Task<IActionResult> result = _controller.Login(null);
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Result, Is.TypeOf<BadRequestObjectResult>());
        }

        [Test]
        public void Login_NullOrWhitespaceUsername([Values(null, "")] string userName)
        {
            Task<IActionResult> result = _controller.Login(new LoginModel
            {
                UserName = userName,
                Password = "password"
            });
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Result, Is.TypeOf<BadRequestObjectResult>());
        }
        [Test]
        public void Login_NullOrWhitespacePassword([Values(null, "")] string password)
        {
            Task<IActionResult> result = _controller.Login(new LoginModel
            {
                UserName = "username",
                Password = password
            });
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Result, Is.TypeOf<BadRequestObjectResult>());
        }

        [Test]
        public void Login_NotFoundUser()
        {
            
            _userManager.Setup(m => m.FindByNameAsync(It.IsAny<string>())).Verifiable();
            Task<IActionResult> result = _controller.Login(new LoginModel
            {
                UserName = "username",
                Password = "password"
            });
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Result, Is.TypeOf<BadRequestObjectResult>());
            _userManager.Verify(m => m.FindByNameAsync(It.IsAny<string>()));
        }

        [Test]
        public void Login_NotFoundUser_Exception()
        {

            _userManager.Setup(m => m.FindByNameAsync(It.IsAny<string>())).Throws(new Exception()).Verifiable();
            Task<IActionResult> result = _controller.Login(new LoginModel
            {
                UserName = "username",
                Password = "password"
            });
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Result, Is.TypeOf<BadRequestObjectResult>());
            _userManager.Verify(m => m.FindByNameAsync(It.IsAny<string>()));
        }

        [Test]
        public void Login_InvalidPassword()
        {
            ApplicationUser applicationUser = new ApplicationUser { UserName = "username" };
            _userManager.Setup(m => m.FindByNameAsync(It.IsAny<string>())).ReturnsAsync(applicationUser).Verifiable();
            _userManager.Setup(m => m.CheckPasswordAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>())).ReturnsAsync(false).Verifiable();
            Task<IActionResult> result = _controller.Login(new LoginModel
            {
                UserName = "username",
                Password = "password"
            });

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Result, Is.TypeOf<BadRequestObjectResult>());
            _userManager.VerifyAll();
        }

        [Test]
        public void Login_Success()
        {
            ApplicationUser applicationUser = new ApplicationUser { UserName = "username", Id="userId" };
            _userManager.Setup(m => m.FindByNameAsync(It.IsAny<string>())).ReturnsAsync(applicationUser).Verifiable();
            _userManager.Setup(m => m.CheckPasswordAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>())).ReturnsAsync(true).Verifiable();
            _userManager.Setup(m => m.GetRolesAsync(It.IsAny<ApplicationUser>())).ReturnsAsync(new List<string> {
                "role"
            }).Verifiable();

            Task<IActionResult> result = _controller.Login(new LoginModel
            {
                UserName = "username",
                Password = "password"
            });

            _userManager.VerifyAll();
            _appSettings.VerifyAll();

            Assert.That(result,Is.Not.Null);
            Assert.That(result.Result,Is.TypeOf<OkObjectResult>());

            OkObjectResult resultObject = result.Result as OkObjectResult;
            Assert.That(resultObject, Is.Not.Null);
            Assert.That(resultObject.Value, Is.Not.Null);

        }

    }
}
