using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using todo_apllication.Controllers;
using todo_domain_entities.Abstractions;
using todo_domain_entities.Implementations;
using todo_domain_entities.POCO;
using Xunit;

namespace todo_app_tests
{
    public class MyTaskTests
    {
        [Fact]
        public async Task IndexReturnsARedirectToDeleteMyTaskWhenIdIsNull()
        {
            // Arrange
            var controller = new MyTaskController(context: null, service: null);

            // Act
            var result = await controller.Delete(id: null);

            // Assert
            var redirectToActionResult =
                Assert.IsType<RedirectToActionResult>(result);

            Assert.Equal("Home", redirectToActionResult.ControllerName);
            Assert.Equal("Index", redirectToActionResult.ActionName);
        }

        [Fact]
        public async Task IndexReturnsARedirectToIndexHomeWhenMyTaskIdIsNull()
        {
            // Arrange
            var controller = new MyTaskController(context: null, service: null);

            // Act
            var result = await controller.Details(id: null);

            // Assert
            var redirectToActionResult =
                Assert.IsType<RedirectToActionResult>(result);

            Assert.Equal("Home", redirectToActionResult.ControllerName);
            Assert.Equal("Index", redirectToActionResult.ActionName);
        }
    }
}
