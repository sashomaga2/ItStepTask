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
        public void Create_ErrorShouldReturnHttpStatusCodeResult()
        {
            // Arrange
            categoryService.Setup(x => x.GetAll()).Throws<Exception>();
            
            // Act
            var result = controller.Create() as HttpStatusCodeResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf(typeof(HttpStatusCodeResult), result);
        }


        [Test]
        public void Index_ShouldReturnView()
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
        }
    }
}
