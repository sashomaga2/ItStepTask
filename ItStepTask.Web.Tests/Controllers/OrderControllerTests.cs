using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using ItStepTask.Services;
using ItStepTask.Services.Contracts;
using ItStepTask.Web;
using ItStepTask.Web.Controllers;
using Moq;
using NUnit.Framework;

namespace ItStepTask.Web.Tests.Controllers
{
    [TestFixture]
    public class OrderControllerTests
    {
        [Test]
        public void Place_WithNullViewModel_ShouldReturnHttpNotFound()
        {

            var ordersService = new Mock<IOrdersService>();
            var itemsService = new Mock<IItemsService>();
            var usersService = new Mock<IUsersService>();

            // Arrange
            var controller = new OrderController(ordersService.Object, itemsService.Object, usersService.Object);

            // Act
            var result = controller.Place(null) as HttpNotFoundResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf(typeof(HttpNotFoundResult), result);
        }

    }
}
