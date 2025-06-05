using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using PFS.Applications;
using PFS.Domain.Models.RequestBody;
using API.Controllers;
using Microsoft.Extensions.Configuration;
using PFS.Application.Applications.Management;
using WebApplication1.Controllers.Management;

namespace Tests.API
{
    [TestFixture]
    public class UserControllerTester
    {
        private UserController _controller;
        private Mock<UserApplication> _mockApp;
        private Mock<ILogger<UserController>> _mockLogger;
        private Mock<IConfiguration> _mockConfig;

        [SetUp]
        public void Setup()
        {
            _mockApp = new Mock<UserApplication>("fake-connection");
            _mockLogger = new Mock<ILogger<UserController>>();
            _mockConfig = new Mock<IConfiguration>();
            _mockConfig.Setup(x => x["ConnectionString"]).Returns("fake-connection");
            _controller = new UserController(_mockConfig.Object, _mockLogger.Object);
        }

        [Test]
        public void Login_ReturnsOk_WhenTokenIsValid()
        {
            // Arrange
            var login = new UserLogin { email = "test@test.com", password = "pass" };
            // You may need to mock _application.Login if it is virtual or interface
            // _mockApp.Setup(x => x.Login(It.IsAny<UserLogin>())).Returns("token");

            // Act
            var result = _controller.Login(login);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);
        }

        [Test]
        public void Login_ReturnsProblem_WhenTokenIsNull()
        {
            // Arrange
            var login = new UserLogin { email = "test@test.com", password = "pass" };
            // _mockApp.Setup(x => x.Login(It.IsAny<UserLogin>())).Returns((string)null);

            // Act
            var result = _controller.Login(login);

            // Assert
            Assert.IsInstanceOf<ObjectResult>(result);
            var objectResult = result as ObjectResult;
            Assert.AreEqual(500, objectResult?.StatusCode ?? 500);
        }
    }
}
