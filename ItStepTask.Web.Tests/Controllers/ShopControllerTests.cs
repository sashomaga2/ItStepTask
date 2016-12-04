using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using ItStepTask.Services;
using ItStepTask.Services.Contracts;
using ItStepTask.Web.Controllers;
using Moq;
using NUnit.Framework;

namespace ItStepTask.Web.Tests.Controllers
{
    [TestFixture]
    public class ShopCOntrollerTests
    {
        [Test]
        public void CategoryChange_ShouldReturnJsonResult()
        {

            var shopService = new Mock<IShopService>();
            // Arrange
            var controller = new ShopController(shopService.Object);

            controller.SetFakeControllerContext();

            // Act
            var result = controller.CategoryChange(1) as JsonResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf(typeof(JsonResult), result);
        }

        [Test]
        public void CategoryChange_ShouldSetSessionCategoryId_Number()
        {

            var shopService = new Mock<IShopService>();
            // Arrange
            var controller = new ShopController(shopService.Object);
            var selectedCategory = 1;

            controller.SetFakeControllerContext();

            // Act
            controller.CategoryChange(selectedCategory);

            // Assert
            Assert.AreEqual(selectedCategory, controller.Session["categoryId"]);
        }

        [Test]
        public void CategoryChange_ShouldSetSessionCategoryId_Null()
        {
            var shopService = new Mock<IShopService>();
            // Arrange
            var controller = new ShopController(shopService.Object);
            int? selectedCategory = null;

            controller.SetFakeControllerContext();

            // Act
            controller.CategoryChange(selectedCategory);

            // Assert
            Assert.AreEqual(selectedCategory, controller.Session["categoryId"]);
        }

    }
}
