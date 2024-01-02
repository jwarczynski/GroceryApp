using Warczynski.Zbaszyniak.GroceryApp.Core;
using Warczynski.Zbaszyniak.GroceryApp.DAOMock1.BO;
using Warczynski.Zbaszyniak.GroceryApp.Interfaces;

namespace Warczynski.Zbaszyniak.GroceryApp.DAOMock1;

public class DAOMock : IDAO
{
    private List<IGrocery> _groceries;
    private List<IProduct> _products;

    public DAOMock()
    {
        _groceries = new List<IGrocery>()
        {
            new Grocery
            {
                Id = 1,
                Name = "Lidl",
            },
            new Grocery
            {
                Id = 2,
                Name = "Walmart",
                Address = "123 Main St"
            },
        };
        _products = new List<IProduct>()
        {
            new Product
            {
                Id = 0,
                Name = "Banana",
                Price = 3.99f,
                Category = ProductCategory.Fruit,
                Magnesium = 0,
                Potassium = 0,
                Sodium = 0,
                Grocery = _groceries[0]
            },
            new Product
            {
                Id = 1,
                Name = "Apple",
                Price = 1.99f,
                Category = ProductCategory.Fruit,
                Magnesium = 0.1f,
                Potassium = 0.1f,
                Sodium = 0.1f,
                Grocery = _groceries[1]
            },
        };
    }

    public IEnumerable<IGrocery> GetAllGroceries()
    {
        return _groceries;
    }

    public IEnumerable<IProduct> GetAllProducts()
    {
        return _products;
    }

    public IGrocery CreateGrocery()
    {
        return new Grocery();
    }

    public IProduct CreateProduct()
    {
        return new Product();
    }
}