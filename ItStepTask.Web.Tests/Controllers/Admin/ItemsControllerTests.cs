using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using AutoMapper;
using ItStepTask.Entity;
using ItStepTask.Services.Contracts;
using ItStepTask.Web.Areas.Admin.Controllers;
using ItStepTask.Web.Areas.Admin.ViewModels;
using ItStepTask.Web.Mapping;
using Moq;
using NUnit.Framework;
using System.Net;

namespace ItStepTask.Web.Tests.Controllers.Admin
{
    [TestFixture]
    public class ItemsControllerTests
    {
        private ItemsController controller;
        private Mock<IItemsService> itemsService;
        private Mock<ICategoryService> categoryService;
        private Mock<ISuppliersService> suppliersService;
        private IMapper mapper;

        [SetUp]
        public void Init()
        {
            itemsService = new Mock<IItemsService>();
            categoryService = new Mock<ICategoryService>();
            suppliersService = new Mock<ISuppliersService>();
            AutoMapperConfiguration.Configure();
            mapper = AutoMapperConfiguration.Config.CreateMapper();

            controller = new ItemsController(mapper, itemsService.Object, categoryService.Object, suppliersService.Object);
        }

        [TearDown]
        public void Dispose()
        {
            controller.Dispose();
        }

        [Test]
        public void Create_CategoryServiceException_ShouldReturnHttpStatusCodeResult()
        {
            // Arrange
            categoryService.Setup(x => x.GetAll()).Throws<Exception>();
            
            // Act
            var result = controller.Create() as HttpStatusCodeResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf(typeof(HttpStatusCodeResult), result);
            Assert.AreEqual(500, result.StatusCode);
        }


        [Test]
        public void Index_ShouldReturnViewResult()
        {
            // Arrange
            var data = new List<Item> {new Item { Name = "Pesho" }, new Item { Name = "Cholate" } };

            itemsService.Setup(x => x.GetAll()).Returns(data.AsQueryable());

            // Act
            var result = controller.Create() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf(typeof(ViewResult), result);
        }

        [Test]
        public void Edit_WithNullId_ShouldReturnHttpStatusCodeResult()
        {
            // Act
            var result = controller.Edit(id: null) as HttpStatusCodeResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf(typeof(HttpStatusCodeResult), result);
            Assert.AreEqual(HttpStatusCode.BadRequest, (HttpStatusCode)result.StatusCode);
        }

        [Test]
        public void Edit_WithNullItem_ShouldReturnHttpNotFoundResult()
        {
            // Arrange
            var itemId = 3;
            itemsService.Setup(x => x.Find(itemId)).Returns(() => null);

            // Act
            var result = controller.Edit(id: itemId) as HttpNotFoundResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf(typeof(HttpNotFoundResult), result);
        }

        [Test]
        public void Delete_WithNullItem_ShouldReturnHttpStatusCodeResult()
        {
            // Act
            var result = controller.Delete(id: null) as HttpStatusCodeResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf(typeof(HttpStatusCodeResult), result);
            Assert.AreEqual(HttpStatusCode.BadRequest, (HttpStatusCode)result.StatusCode);
        }

        [Test]
        public void Delete_ItemServiceException_ShouldReturnHttpStatusCodeResult()
        {
            //Arrange
            itemsService.Setup(x => x.GetAll()).Throws<Exception>();

            // Act
            var result = controller.Delete(id: null) as HttpStatusCodeResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf(typeof(HttpStatusCodeResult), result);
            Assert.AreEqual(HttpStatusCode.BadRequest, (HttpStatusCode)result.StatusCode);
        }

        [Test]
        public void Edit_Post_ModelStateError_ShouldReturnViewResult()
        {
            //Arrange
            controller.ModelState.AddModelError("", "fake error message");

            // Act
            var result = controller.Edit(new ItemAdminViewModel()) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf(typeof(ViewResult), result);
        }

        [Test]
        public void Edit_Post_ItemServiceUpdateThrowError_ShouldReturnHttpStatusCodeResult()
        {
            //Arrange
            itemsService.Setup(x => x.Update(It.IsAny<Item>())).Throws<Exception>();

            var model = new ItemAdminViewModel { Id = 1, CategoryName = "test", Name = "test", Price = 4, Quantity = 45, SupplierName = "pesho" };
            // Act
            var result = controller.Edit(model) as HttpStatusCodeResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf(typeof(HttpStatusCodeResult), result);
            Assert.AreEqual(HttpStatusCode.InternalServerError, (HttpStatusCode)result.StatusCode);
        }

        [Test]
        public void Create_ModelStateError_ShouldReturnViewResult()
        {
            //Arrange
            controller.ModelState.AddModelError("", "fake error message");

            // Act
            var result = controller.Create(new CreateItemViewModel(), null) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf(typeof(ViewResult), result);
        }

        [Test]
        public void Create_WithSupplierNotExists_ShouldReturnHttpNotFoundResult()
        {
            //Arrange
            suppliersService.Setup(x => x.Find(It.IsAny<int>())).Returns(() => null);

            // Act
            var result = controller.Create(new CreateItemViewModel { SupplierId = 1 }, null) as HttpNotFoundResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf(typeof(HttpNotFoundResult), result);
        }

        [Test]
        public void Create_WithCategoryNotExists_ShouldReturnHttpNotFoundResult()
        {
            //Arrange
            suppliersService.Setup(x => x.Find(It.IsAny<int>())).Returns(new Supplier());
            categoryService.Setup(x => x.Find(It.IsAny<int>())).Returns(() => null);

            // Act
            var result = controller.Create(new CreateItemViewModel { SupplierId = 1 }, null) as HttpNotFoundResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf(typeof(HttpNotFoundResult), result);
        }

        [Test]
        public void Create_Success_ShouldReturnRedirect()
        {
            //Arrange
            suppliersService.Setup(x => x.Find(It.IsAny<int>())).Returns(new Supplier());
            categoryService.Setup(x => x.Find(It.IsAny<int>())).Returns(new Category());
            itemsService.Setup(x => x.Add(It.IsAny<Item>()));

            // Act
            var result = controller.Create(new CreateItemViewModel { SupplierId = 1 }, null) as RedirectToRouteResult;

            // Assert
            Assert.IsNotNull(result);
            itemsService.Verify(x => x.Add(It.IsAny<Item>()));
            Assert.IsInstanceOf(typeof(RedirectToRouteResult), result);
            Assert.AreEqual("Index", result.RouteValues["action"]);
        }
    }
}
