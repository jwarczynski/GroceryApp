using Warczynski.Zbaszyniak.GroceryApp.Core;
using Warczynski.Zbaszyniak.GroceryApp.DAOMock2.BO;
using Warczynski.Zbaszyniak.GroceryApp.Interfaces;

namespace Warczynski.Zbaszyniak.GroceryApp.DAOMock2;

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

    public IGrocery SaveGrocery(IGrocery grocery)
    {
        _groceries.Add(grocery);
        return grocery;
    }

    public IProduct SaveProduct(IProduct product)
    {
        _products.Add(product);
        return product;
    }

    public void EditGrocery(IGrocery grocery)
    {
        var groceryToDelete = _groceries.FirstOrDefault(g => g.Id == grocery.Id);
        if (groceryToDelete == null) return;
        DeleteGrocery(groceryToDelete);
        _groceries.Add(grocery);
    }

    public void EditProduct(IProduct product)
    {
        var productToDelete = _products.FirstOrDefault(g => g.Id == product.Id);
        if (productToDelete == null) return; 
        DeleteProduct(productToDelete);
        _products.Add(product);
    }

    public void DeleteGrocery(IGrocery grocery)
    {
        _groceries.Remove(grocery);
    }

    public void DeleteProduct(IProduct product)
    {
        _products.Remove(product);
    }

    public IEnumerable<IProduct> GetProductsByFilter(IFilter filter)
    {
        throw new NotImplementedException();
    }

    public void RemoveProduct(IProduct product)
    {
        throw new NotImplementedException();
    }
}