using Microsoft.AspNetCore.Mvc;
using Moq;
using Warczynski.Zbaszyniak.GroceryApp.Interfaces;
using Warczynski.Zbaszyniak.GroceryApp.MVC.Controllers;
using Warczynski.Zbaszyniak.GroceryApp.MVC.Models;

namespace GroceryApp.Tests.Tests
{
    [TestFixture]
    public class GroceriesControllerTests
    {
        private Mock<IDAO> _daoMock = null!;
        private GroceriesController _controller = null!;

        [SetUp]
        public void Setup()
        {
            _daoMock = new Mock<IDAO>();
            _daoMock.Setup(d => d.GetAllGroceries()).Returns(new List<Grocery>());

            var groceriesViewModel = new GroceriesViewModel(_daoMock.Object);
            var applicationUsersViewModel = new Mock<ApplicationUsersViewModel>(_daoMock.Object);
            _controller = new GroceriesController(groceriesViewModel, applicationUsersViewModel.Object);
        }

        [Test]
        public async Task Index_ReturnsViewWithGroceriesViewModel_Assertion()
        {
            var result = await _controller.Index("");

            Assert.That(result, Is.InstanceOf<ViewResult>());
            var viewResult = (ViewResult)result;
            Assert.That(viewResult.Model, Is.InstanceOf<GroceriesViewModel>());
        }

        [Test]
        public async Task Index_CallsDAO_GetAllGroceries_Once()
        {
            await _controller.Index("");
            _daoMock.Verify(d => d.GetAllGroceries(), Times.Once);
        }

    }
}