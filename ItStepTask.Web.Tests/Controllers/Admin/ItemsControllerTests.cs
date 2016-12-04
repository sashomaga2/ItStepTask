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
using ItStepTask.Web.Controllers;
using ItStepTask.Web.Mapping;
using Moq;
using NUnit.Framework;

namespace ItStepTask.Web.Tests.Controllers.Admin
{
    [TestFixture]
    public class ItemsControllerTests
    {
        [Test]
        public void Create_ErrorShouldReturnHttpStatusCodeResult()
        {
            // Arrange
            var itemsService = new Mock<IItemsService>();
            var categoryService = new Mock<ICategoryService>();
            var suppliersService = new Mock<ISuppliersService>();
            var mapper = new Mock<IMapper>();

            categoryService.Setup(x => x.GetAll()).Throws<Exception>();
            
            var controller = new ItemsController(mapper.Object, itemsService.Object, categoryService.Object, suppliersService.Object);

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
            var itemsService = new Mock<IItemsService>();
            var categoryService = new Mock<ICategoryService>();
            var suppliersService = new Mock<ISuppliersService>();

            AutoMapperConfiguration.Configure();
            var mapper = AutoMapperConfiguration.Config.CreateMapper();

            var data = new List<Item> {new Item { Name = "Pesho" }, new Item { Name = "Cholate" } };

            itemsService.Setup(x => x.GetAll()).Returns(data.AsQueryable());

            var controller = new ItemsController(mapper, itemsService.Object, categoryService.Object, suppliersService.Object);

            // Act
            var result = controller.Create() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf(typeof(ViewResult), result);
        }
    }
}
