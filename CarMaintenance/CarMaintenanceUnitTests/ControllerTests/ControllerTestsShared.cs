using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

using CarMaintenance.Controllers;
using CarMaintenance.Managers.Car;
using CarMaintenance.Managers.ServiceCalendar;
using CarMaintenance.Models;
using CarMaintenance.Models.User;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

using Moq;


namespace CarMaintenanceUnitTests.ControllerTests
{
    internal class ControllerTestsShared<T> where T : ControllerBase
    {
        protected Mock<ICarManager> _carManager;
        protected Mock<ILogger<T>> _logger;
        protected T _controller;
        protected Mock<UserManager<ApplicationUser>> _userManager;
        protected Mock<IOptions<ApplicationSettings>> _appSettings;
        protected Mock<IServiceCalendarManager> _serviceCalendarManager;
        protected Mock<UserManager<ApplicationUser>> GetMockUserManager()
        {
            Mock<IUserStore<ApplicationUser>> userStoreMock = new Mock<IUserStore<ApplicationUser>>();
            return new Mock<UserManager<ApplicationUser>>(
                userStoreMock.Object, null, null, null, null, null, null, null, null);
        }

        protected void MockUserForController(ref T controller)
        {
            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Name, "example name"),
                new Claim(ClaimTypes.NameIdentifier, "1"),
                new Claim("UserID", "guid"),
            }, "mock"));

            controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext { User = user }
            };
        }

    }
}
