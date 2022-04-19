using CSC237_SportsPro_LV5_start.Controllers;
using CSC237_SportsPro_LV5_start.Models;
using Microsoft.AspNetCore.Mvc.ViewFeatures;  // for ITempDataDictionary
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using Xunit;

namespace SportsPro.Tests
{
    public class ProductControllerTests
    {
        [Fact]
        public void List_ReturnsAViewResult()
        {
            // arrange
            var rep = new Mock<IRepository<Product>>();
            rep.Setup(m => m.List(It.IsAny<QueryOptions<Product>>())).Returns(new List<Product>());
            var controller = new ProductController(rep.Object);

            // act
            var result = controller.List();

            // assert
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void List_ModelIsACollectionOfProducts()
        {
            // arrange
            var rep = new Mock<IRepository<Product>>();
            rep.Setup(m => m.List(It.IsAny<QueryOptions<Product>>())).Returns(new List<Product>());
            var controller = new ProductController(rep.Object);

            // act
            var model = controller.List().ViewData.Model as IEnumerable<Product>;

            // assert
            Assert.IsType<List<Product>>(model);
        }

        [Fact]
        public void Add_GET_ModelIsAProductObject()
        {
            // arrange
            var rep = new Mock<IRepository<Product>>();
            var controller = new ProductController(rep.Object);

            // act
            var model = controller.Add().ViewData.Model as Product;

            // assert
            Assert.IsType<Product>(model);
        }

        [Fact]
        public void Add_GET_ValueOfViewBagActionPropertyIsAdd()
        {
            // arrange
            var rep = new Mock<IRepository<Product>>();
            var controller = new ProductController(rep.Object);
            string expected = "Add";

            // act
            ViewResult result = controller.Add();

            // assert
            Assert.Equal(expected, result.ViewData["action"]?.ToString());
        }

        [Fact]
        public void Edit_GET_ModelIsAProductObject()
        {
            // arrange
            var rep = new Mock<IRepository<Product>>();
            rep.Setup(m => m.Get(It.IsAny<int>())).Returns(new Product());
            var controller = new ProductController(rep.Object);

            // act
            var model = controller.Edit(1).ViewData.Model as Product;

            // assert
            Assert.IsType<Product>(model);
        }

        [Fact]
        public void Edit_GET_ValueOfViewBagActionPropertyIsEdit()
        {
            // arrange
            var rep = new Mock<IRepository<Product>>();
            rep.Setup(m => m.Get(It.IsAny<int>())).Returns(new Product());
            var controller = new ProductController(rep.Object);
            string expected = "Edit";

            // act
            ViewResult result = controller.Edit(1);

            // assert
            Assert.Equal(expected, result.ViewData["action"]?.ToString());
        }

        [Fact]
        public void Save_ReturnsViewResultIfModelStateIsInvalid()
        {
            // arrange
            var rep = new Mock<IRepository<Product>>();
            var controller = new ProductController(rep.Object);

            controller.ModelState.AddModelError("", "Error");
            var productToSave = new Product();

            // act
            var result = controller.Save(productToSave);

            // assert
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void Save_ReturnsRedirectToActionResultIfModelStateIsValid()
        {
            // arrange
            var rep = new Mock<IRepository<Product>>();
            var temp = new Mock<ITempDataDictionary>();

            var controller = new ProductController(rep.Object)
            {
                TempData = temp.Object
            };
            var productToSave = new Product();

            // act
            var result = controller.Save(productToSave);

            // assert
            Assert.IsType<RedirectToActionResult>(result);
        }

        [Fact]
        public void Save_RedirectsToListActionMethodOnSuccess()
        {
            // arrange
            var rep = new Mock<IRepository<Product>>();
            var temp = new Mock<ITempDataDictionary>();

            var controller = new ProductController(rep.Object)
            {
                TempData = temp.Object
            };
            var productToSave = new Product();
            string expected = "List";

            // act  
            var result = (RedirectToActionResult)controller.Save(productToSave);

            // assert
            Assert.Equal(expected, result.ActionName);
        }

        [Fact]
        public void Delete_GET_ReturnsAViewResult()
        {
            // arrange
            var rep = new Mock<IRepository<Product>>();
            rep.Setup(m => m.Get(It.IsAny<int>())).Returns(new Product());
            var controller = new ProductController(rep.Object);

            // act
            var result = controller.Delete(1);

            // assert
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void Delete_GET_ModelIsAProductObject()
        {
            // arrange
            var rep = new Mock<IRepository<Product>>();
            rep.Setup(m => m.Get(It.IsAny<int>())).Returns(new Product());
            var controller = new ProductController(rep.Object);

            // act
            var model = controller.Delete(1).ViewData.Model as Product;

            // assert
            Assert.IsType<Product>(model);
        }

        [Fact]
        public void Delete_POST_ReturnsARedirectToActionResult()
        {
            // arrange
            var rep = new Mock<IRepository<Product>>();
            rep.Setup(m => m.Get(It.IsAny<int>())).Returns(new Product());
            var temp = new Mock<ITempDataDictionary>();

            var controller = new ProductController(rep.Object)
            {
                TempData = temp.Object
            };
            var productToDelete = new Product();

            // act
            var result = controller.Delete(productToDelete);

            // assert
            Assert.IsType<RedirectToActionResult>(result);
        }
    }
}
