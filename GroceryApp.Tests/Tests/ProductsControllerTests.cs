using Microsoft.AspNetCore.Mvc;
using Moq;
using Warczynski.Zbaszyniak.GroceryApp.Interfaces;
using Warczynski.Zbaszyniak.GroceryApp.MVC.Controllers;
using Warczynski.Zbaszyniak.GroceryApp.MVC.Models;

namespace GroceryApp.Tests;

[TestFixture]
public class ProductsControllerTests
{
    private Mock<IDAO> _daoMock = null!;
    private ProductsController _controller = null!;

    [SetUp]
    public void Setup()
    {
        _daoMock = new Mock<IDAO>();
        _daoMock.Setup(d => d.GetAllProducts()).Returns(new List<IProduct>());

        var productsViewModel = new ProductsViewModel(_daoMock.Object);
        var groceriesViewModelMock = new Mock<GroceriesViewModel>(_daoMock.Object);
        var applicationUsersViewModel = new Mock<ApplicationUsersViewModel>(_daoMock.Object);
        _controller = new ProductsController(
            productsViewModel, 
            groceriesViewModelMock.Object,
            applicationUsersViewModel.Object);
    }

    [Test]
    public async Task Index_ReturnsViewWithProductsViewModel_Assertion()
    {
        var result = await _controller.Index("", null);

        Assert.That(result, Is.InstanceOf<ViewResult>());
        var viewResult = (ViewResult)result;
        Assert.That(viewResult.Model, Is.InstanceOf<ProductsViewModel>());
    }

    [Test]
    public async Task Index_CallsDAO_GetAllProducts_Once()
    {
        await _controller.Index("", null);
        _daoMock.Verify(d => d.GetAllProducts(), Times.Once);
    }
}
