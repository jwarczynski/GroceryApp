using Moq;
using Warczynski.Zbaszyniak.GroceryApp.BLC;
using Warczynski.Zbaszyniak.GroceryApp.Interfaces;

namespace GroceryApp.Tests.Tests
{
    [TestFixture]
    public class BLCTests
    {
        private Mock<IDAO> _daoMock = null!;
        private BLC _blc = null!;

        [SetUp]
        public void Setup()
        {
            _daoMock = new Mock<IDAO>();
            _blc = new BLC(_daoMock.Object);
        }

        [Test]
        public void GetAllGroceries_ShouldCallDAO_GetAllGroceries_Once()
        {
            _blc.GetAllGroceries();

            _daoMock.Verify(d => d.GetAllGroceries(), Times.Once);
        }

        [Test]
        public void SaveGrocery_ShouldCallDAO_SaveGrocery_Once()
        {
            var grocery = new Mock<IGrocery>();

            _blc.SaveGrocery(grocery.Object);

            _daoMock.Verify(d => d.SaveGrocery(grocery.Object), Times.Once);
        }

        [Test]
        public void GetProductsByFilter_ShouldCallDAO_GetProductsByFilter_Once()
        {
            var filter = new Mock<IFilter>();

            _blc.GetProductsByFilter(filter.Object);

            _daoMock.Verify(d => d.GetProductsByFilter(filter.Object), Times.Once);
        }

    }
}