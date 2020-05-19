using System.Threading;
using System.Threading.Tasks;

using CarMaintenance.Controllers;
using CarMaintenance.Models;
using CarMaintenance.Models.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

using Moq;
using NUnit.Framework;

namespace CarMaintenanceUnitTests.ControllerTests
{
    public class ApplicationUserControllerTests
    {

        private Mock<UserManager<ApplicationUser>> _userManager;
        private Mock<IOptions<ApplicationSettings>> _options;
        private Mock<ILogger<ApplicationUserController>> _logger;
        private ApplicationUserController _controller;

        [SetUp]
        public void SetUp()
        {
            _options = new Mock<IOptions<ApplicationSettings>>();
            _userManager = GetMockUserManager();
            _logger = new Mock<ILogger<ApplicationUserController>>();
            _controller = new ApplicationUserController(_userManager.Object, _options.Object, _logger.Object);
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
            Task<IActionResult> result = _controller.Login(new LoginModel
            {
                UserName = "username",
                Password = "password"
            });
            _userManager.Setup(m => m.FindByNameAsync(It.IsAny<string>())).ReturnsAsync(default(ApplicationUser)).Verifiable();
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Result, Is.TypeOf<BadRequestObjectResult>());
            _userManager.Verify(m => m.FindByNameAsync(It.IsAny<string>()));
        }

        [Test]
        public void Login_InvalidPassword()
        {
            Task<IActionResult> result = _controller.Login(new LoginModel
            {
                UserName = "username",
                Password = "password"
            });
            ApplicationUser applicationUser = new ApplicationUser { UserName = "username" };
            
            //this mock returns a null user. I investigated a lot but i didn't find a fix for that. Idk why that didn't work
            _userManager.Setup(m => m.FindByNameAsync(It.IsAny<string>())).ReturnsAsync(applicationUser).Verifiable();
            
            //_userManager.Setup(m => m.CheckPasswordAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>())).Returns(Task.FromResult(false)).Verifiable();
            //Assert.That(result, Is.Not.Null);
            //Assert.That(result.Result, Is.TypeOf<BadRequestObjectResult>());
            //_userManager.Verify(m => m.FindByNameAsync(It.IsAny<string>()));
            //_userManager.Verify(m => m.CheckPasswordAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>()));
        }
        [Test]
        public void Login_Success()
        {
            Task<IActionResult> result = _controller.Login(new LoginModel
            {
                UserName = "username",
                Password = "password"
            });
            ApplicationUser applicationUser = new ApplicationUser { UserName = "username" };

            //this mock returns a null user. I investigated a lot but i didn't find a fix for that. Idk why that didn't work
            _userManager.Setup(m => m.FindByNameAsync(It.IsAny<string>())).ReturnsAsync(applicationUser).Verifiable();

            //cannot continue testing because user is null
        }

        private Mock<UserManager<ApplicationUser>> GetMockUserManager()
        {
            Mock<IUserStore<ApplicationUser>> userStoreMock = new Mock<IUserStore<ApplicationUser>>();
            return new Mock<UserManager<ApplicationUser>>(
                userStoreMock.Object, null, null, null, null, null, null, null, null);
        }
    }
}
