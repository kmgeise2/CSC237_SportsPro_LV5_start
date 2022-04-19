using CSC237_SportsPro_LV5_start.Controllers;
using CSC237_SportsPro_LV5_start.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace SportsPro.Tests
{
    public class TechIncidentControllerTests
    {
        [Fact]
        public void Get_ModelIsATechnicianObject()
        {
            // arrange
            var unit = GetMockUnitOfWork();
            var http = GetMockHttpContextAccessor();
            var controller = new TechIncidentController(unit, http);

            // act
            ViewResult result = (ViewResult)controller.Get();  // must cast bc action method has IActionResult return type
            var model = result.ViewData.Model as Technician;

            // assert
            Assert.IsType<Technician>(model);
        }

        [Fact]
        public void List_GET_ReturnsAViewResult()
        {
            // arrange
            var unit = GetMockUnitOfWork();
            var http = GetMockHttpContextAccessor();
            var controller = new TechIncidentController(unit, http);

            // act
            var result = controller.List(1);

            // assert
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void List_POST_ReturnsARedirectToActionResult()
        {
            // arrange
            var unit = GetMockUnitOfWork();
            var http = GetMockHttpContextAccessor();
            var tempData = new Mock<ITempDataDictionary>();
            var controller = new TechIncidentController(unit, http)
            {
                TempData = tempData.Object
            };
            var technician = new Technician();

            // act
            var result = controller.List(technician);

            // assert
            Assert.IsType<RedirectToActionResult>(result);
        }

        // private helper methods
        private IHttpContextAccessor GetMockHttpContextAccessor()
        {
            var accessor = new Mock<IHttpContextAccessor>();
            var context = new DefaultHttpContext();
            var session = new Mock<ISession>();

            accessor.Setup(m => m.HttpContext).Returns(context);
            accessor.Setup(m => m.HttpContext.Session).Returns(session.Object);

            return accessor.Object;
        }

        private ISportsProUnit GetMockUnitOfWork()
        {
            var irep = new Mock<IRepository<Incident>>();
            irep.Setup(m => m.Get(It.IsAny<int>())).Returns(new Incident());
            irep.Setup(m => m.Get(It.IsAny<QueryOptions<Incident>>())).Returns(new Incident());
            irep.Setup(m => m.List(It.IsAny<QueryOptions<Incident>>())).Returns(new List<Incident>());

            var trep = new Mock<IRepository<Technician>>();
            trep.Setup(m => m.Get(It.IsAny<int>())).Returns(new Technician());
            trep.Setup(m => m.List(It.IsAny<QueryOptions<Technician>>())).Returns(new List<Technician>());

            var unit = new Mock<ISportsProUnit>();
            unit.Setup(m => m.Incidents).Returns(irep.Object);
            unit.Setup(m => m.Technicians).Returns(trep.Object);

            return unit.Object;
        }
    }
}
