using GroceryApp.Core;
using GroceryApp.DAOMock2.BO;
using GroceryApp.Interfaces;

namespace GroceryApp.DAOMock2;

public class DAOMock:IDAO
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
                Name = "Tesco",
                Address = "456 Second St"
            },
            new Grocery
            {
                Id = 2,
                Name = "Aldi",
                Address = "893 Third St"
            },
        };
        _products = new List<IProduct>()
        {
            new Product
            {
                Id = 0,
                Name = "Pineapple",
                Price = 4.99f,
                Category = ProductCategory.Fruit,
                Magnesium = 0,
                Potassium = 0,
                Sodium = 0,
                Grocery = _groceries[0]
            },
            new Product
            {
                Id = 1,
                Name = "Grapefruit",
                Price = 5.99f,
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