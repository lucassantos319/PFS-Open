using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using API.Controllers;

namespace Tests.API
{
    [TestFixture]
    public class DashboardControllerTester
    {
        private DashboardController _controller;

        [SetUp]
        public void Setup()
        {
            _controller = new DashboardController();
        }

        [Test]
        public void GetInfo_ReturnsOk()
        {
            // Act
            var result = _controller.GetInfo(1);

            // Assert
            Assert.IsInstanceOf<OkResult>(result);
        }
    }
}
