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
    public class CategoryControllerTests
    {
        [Test]
        public void CategoryChange_ShouldReturnJsonResult()
        {
            // Arrange
            var controller = new CategoryController();

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
            // Arrange
            var controller = new CategoryController();
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
            // Arrange
            var controller = new CategoryController();
            int? selectedCategory = null;

            controller.SetFakeControllerContext();

            // Act
            controller.CategoryChange(selectedCategory);

            // Assert
            Assert.AreEqual(selectedCategory, controller.Session["categoryId"]);
        }

    }
}
