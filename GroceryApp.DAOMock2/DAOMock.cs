using Warczynski.Zbaszyniak.GroceryApp.Core;
using Warczynski.Zbaszyniak.GroceryApp.DAOMock2.BO;
using Warczynski.Zbaszyniak.GroceryApp.Interfaces;

namespace Warczynski.Zbaszyniak.GroceryApp.DAOMock2;

public class DAOMock : IDAO
{
    private List<IGrocery> _groceries;
    private List<IProduct> _products;
    private List<IApplicationUser> _users;

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
                Id = 1,
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
                Id = 2,
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
        grocery.Id ??= _groceries.Max(g => g.Id) + 1;
        _groceries.Add(grocery);
        return grocery;
    }

    public IProduct SaveProduct(IProduct product)
    {
        product.Id ??= _products.Max(p => p.Id) + 1;
        _products.Add(product);
        return product;
    }

    public IGrocery EditGrocery(IGrocery grocery)
    {
        var groceryToDelete = _groceries.FirstOrDefault(g => g.Id == grocery.Id);
        if (groceryToDelete == null) return groceryToDelete;
        DeleteGrocery(groceryToDelete);
        _groceries.Add(grocery);
        return groceryToDelete;
    }

    public IProduct EditProduct(IProduct product)
    {
        var productToDelete = _products.FirstOrDefault(g => g.Id == product.Id);
        if (productToDelete == null) return productToDelete; 
        DeleteProduct(productToDelete);
        _products.Add(product);
        return productToDelete;
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
        _products.Remove(product);
    }

    public IGrocery GetGroceryTemplate()
    {
        return new Grocery();
    }

    public IProduct GetProductWithGrocery(int id)
    {
        return _products.FirstOrDefault(p => p.Id == id, null);
    }

    public IProduct GetProductTemplate()
    {
        return new Product();
    }

    public IEnumerable<IApplicationUser> GetApplicationUsers()
    {
        return _users.AsEnumerable();
    }

    public IApplicationUser GetApplicationUser(string name)
    {
        return _users.Where(u => u.Name == name).Single();
    }

    public IApplicationUser? SaveApplicationUser(IApplicationUser user)
    {
        if (_users.Where(u => u.Name == user.Name).Any()) return null;
        _users.Add(user);
        return user;
    }

    public IApplicationUser EditApplicationUser(IApplicationUser user)
    {
        _users.Remove(_users.Where(u => u.Name == user.Name).Single());
        _users.Add(user);
        return user;
    }

    public void DeleteApplicationUser(IApplicationUser user)
    {
        _users.Remove(_users.Where(u => u.Name == user.Name).Single());
    }
}