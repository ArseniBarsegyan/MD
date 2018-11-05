using MD.CoreMVC.Controllers;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace MD.CoreMVC.Tests
{
    public class HomeControllerTests
    {
        [Fact]
        public void IndexReturnsAViewResult()
        {
            var homeController = new HomeController();
            var result = homeController.Index();
            Assert.IsType<ViewResult>(result);
        }
    }
}