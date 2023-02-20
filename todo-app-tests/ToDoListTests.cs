using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Threading.Tasks;
using todo_apllication.Controllers;
using todo_apllication.Infrastructure;
using todo_apllication.Models;
using todo_domain_entities.Abstractions;
using todo_domain_entities.Context;
using Xunit;


namespace todo_app_tests
{
    public class ToDoListTests
    {
        [Fact]
        public async Task IndexReturnsARedirectToDeleteToDoListWhenIdIsNull()
        {
            // Arrange
            var controller = new ToDoController(context: null, service: null);

            // Act
            var result = await controller.Delete(id: null);

            // Assert
            var redirectToActionResult =
                Assert.IsType<RedirectToActionResult>(result);

            Assert.Equal("Home", redirectToActionResult.ControllerName);
            Assert.Equal("Index", redirectToActionResult.ActionName);
        }

        [Fact]
        public async Task IndexReturnsARedirectToIndexHomeWhenToDoListIdIsNull()
        {
            // Arrange
            var controller = new ToDoController(context: null, service: null);

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
