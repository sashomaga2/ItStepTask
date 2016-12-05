using AutoMapper;
using ItStepTask.Entity;
using ItStepTask.Services.Contracts;
using ItStepTask.Web.Controllers;
using ItStepTask.Web.Mapping;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ItStepTask.Web.Tests.Controllers
{
    [TestFixture]
    class ShoppingCartControllerTests
    {
        private IMapper mapper;
        private Mock<IShoppingCartService> shoppingCartService;
        private Mock<IItemsService> itemsService;
        private Mock<IUsersService> usersService;
        private ShoppingCartController controller;

        [SetUp]
        public void Init()
        {
            AutoMapperConfiguration.Configure();
            mapper = AutoMapperConfiguration.Config.CreateMapper();
            shoppingCartService = new Mock<IShoppingCartService>();
            itemsService = new Mock<IItemsService>();
            usersService = new Mock<IUsersService>();

            controller = new ShoppingCartController(mapper, shoppingCartService.Object, itemsService.Object, usersService.Object);
        }

        [TearDown]
        public void Dispose()
        {
            controller.Dispose();
        }

        [Test]
        public void Index_SessionShoppingCartItemsIsNull_RedirectToHome()
        {
            // Arrange
            controller.SetFakeControllerContext();
            controller.Session["ShoppingCartItems"] = null;

            // Act
            var result = controller.Index() as RedirectToRouteResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf(typeof(RedirectToRouteResult), result);
            Assert.AreEqual("Index", result.RouteValues["action"]);
            Assert.AreEqual("Home", result.RouteValues["controller"]);
        }

        [Test]
        public void Index_SessionShoppingCartItemsCountIsZero_RedirectToHome()
        {
            // Arrange
            controller.SetFakeControllerContext();
            controller.Session["ShoppingCartItems"] = new HashSet<int>();

            // Act
            var result = controller.Index() as RedirectToRouteResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf(typeof(RedirectToRouteResult), result);
            Assert.AreEqual("Index", result.RouteValues["action"]);
            Assert.AreEqual("Home", result.RouteValues["controller"]);
        }

        [Test]
        public void Index_ItemServiceThrowException_ReturnHttpStatusCodeResult()
        {
            // Arrange
            controller.SetFakeControllerContext();
            controller.Session["ShoppingCartItems"] = new HashSet<int> { 2,3 };
            var itemId = 2;
            itemsService.Setup(x => x.Find(itemId)).Throws<Exception>();

            // Act
            var result = controller.Index() as HttpStatusCodeResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf(typeof(HttpStatusCodeResult), result);
            Assert.AreEqual(HttpStatusCode.InternalServerError, (HttpStatusCode)result.StatusCode);
        }

        [Test]
        public void Index_ReturnsViewResult()
        {
            // Arrange
            controller.SetFakeControllerContext();
            var itemId = 2;
            controller.Session["ShoppingCartItems"] = new HashSet<int> { itemId };
            itemsService.Setup(x => x.Find(itemId)).Returns(
                new Item {  Id = itemId,
                            Name = "TestItem",
                            Category = new Category { Id = 1 },
                            Price = 2,
                            Supplier = new Supplier { Id = 1 },
                            Quantity = 2 });
                
            // Act
            var result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf(typeof(ViewResult), result);
        }

        [Test]
        public void Delete_WithNullItemId_ShouldReturnJsonResultSuccessFalse()
        {
            // Act
            var result = controller.Delete(null) as JsonResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf(typeof(JsonResult), result);
            Assert.AreEqual(@"{ success = False }", result.Data.ToString());
        }

        [Test]
        public void Delete_ItemServiceThrowException_ShouldReturnReturnHttpStatusCodeResult()
        {
            var itemId = 3;
            // Arrange
            itemsService.Setup(x => x.Find(itemId)).Throws<Exception>();
            // Act
            var result = controller.Delete(itemId) as HttpStatusCodeResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf(typeof(HttpStatusCodeResult), result);
            Assert.AreEqual(HttpStatusCode.InternalServerError, (HttpStatusCode)result.StatusCode);
        }

        [Test]
        public void Delete_ItemServiceReturnNull_ShouldReturnReturnJsonResultSuccessFalse()
        {
            // Arrange
            var itemId = 3;
            itemsService.Setup(x => x.Find(itemId)).Returns(() => null);

            // Act
            var result = controller.Delete(itemId) as JsonResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf(typeof(JsonResult), result);
            Assert.AreEqual(@"{ success = False }", result.Data.ToString());
        }

        [Test]
        public void Delete_SessionShoppingCartItemsIsNull_ShouldReturnReturnJsonResultSuccessFalse()
        {
            // Arrange
            var itemId = 3;
            controller.SetFakeControllerContext();
            controller.Session["ShoppingCartItems"] = null;
            itemsService.Setup(x => x.Find(itemId)).Returns(() => new Item());

            // Act
            var result = controller.Delete(itemId) as JsonResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf(typeof(JsonResult), result);
            Assert.AreEqual(@"{ success = False }", result.Data.ToString());
        }

        [Test]
        public void Delete_ShouldReturnJsonSuccessTrue()
        {
            // Arrange
            var itemId = 3;
            controller.SetFakeControllerContext();
            controller.Session["ShoppingCartItems"] = new HashSet<int> { itemId };
            itemsService.Setup(x => x.Find(itemId)).Returns(new Item
            {
                Id = itemId,
                Name = "TestItem",
                Category = new Category { Id = 1 },
                Price = 2,
                Supplier = new Supplier { Id = 1 },
                Quantity = 2
            });

            // Act
            var result = controller.Delete(itemId) as JsonResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf(typeof(JsonResult), result);
            Assert.AreEqual(0, ((HashSet<int>)controller.Session["ShoppingCartItems"]).Count);
            Assert.AreEqual(@"{ success = True }", result.Data.ToString());
        }

        [Test]
        public void Put_ItemServiceFindIsNull_ShouldReturnJsonSuccessFalse()
        {
            // Arrange
            var itemId = 3;
            itemsService.Setup(x => x.Find(It.IsAny<int>())).Returns(() => null);

            // Act
            var result = controller.Put(itemId) as JsonResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf(typeof(JsonResult), result);
            Assert.AreEqual(@"{ success = False }", result.Data.ToString());
        }

        [Test]
        public void Put_ItemServiceException_ShouldReturnHttpStatusCodeResult()
        {
            // Arrange
            var itemId = 3;
            itemsService.Setup(x => x.Find(itemId)).Throws<Exception>();

            // Act
            var result = controller.Put(itemId) as HttpStatusCodeResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf(typeof(HttpStatusCodeResult), result);
            Assert.AreEqual(HttpStatusCode.InternalServerError, (HttpStatusCode)result.StatusCode);
        }

        [Test]
        public void Put_ItemAlreadyAdded_ShouldReturnJsonResultSuccessFalse()
        {
            // Arrange
            var itemId = 3;
            controller.SetFakeControllerContext();
            controller.Session["ShoppingCartItems"] = new HashSet<int> { itemId };
            itemsService.Setup(x => x.Find(itemId)).Returns(new Item());

            // Act
            var result = controller.Put(itemId) as JsonResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf(typeof(JsonResult), result);
            Assert.AreEqual(@"{ success = False, message = Already added! }", result.Data.ToString());
        }

        [Test]
        public void Put_SessionShoppingCartItemsIsNull_ShouldReturnJsonResultSuccess()
        {
            // Arrange
            var itemId = 2;
            controller.SetFakeControllerContext();
            controller.Session["ShoppingCartItems"] = null;
            itemsService.Setup(x => x.Find(itemId)).Returns(new Item());

            // Act
            var result = controller.Put(itemId) as JsonResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf(typeof(JsonResult), result);
            Assert.IsTrue(((HashSet<int>)controller.Session["ShoppingCartItems"]).Contains(itemId));
            Assert.AreEqual(@"{ success = True }", result.Data.ToString());
        }

        [Test]
        public void Put_SessionShoppingCartItemsNotEmpty_ShouldReturnJsonResultSuccess()
        {
            // Arrange
            var itemId = 2;
            controller.SetFakeControllerContext();
            controller.Session["ShoppingCartItems"] = new HashSet<int> { 4 };
            itemsService.Setup(x => x.Find(itemId)).Returns(new Item());

            // Act
            var result = controller.Put(itemId) as JsonResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf(typeof(JsonResult), result);
            Assert.IsTrue(((HashSet<int>)controller.Session["ShoppingCartItems"]).Contains(itemId));
            Assert.AreEqual(@"{ success = True }", result.Data.ToString());
        }
    }
}
