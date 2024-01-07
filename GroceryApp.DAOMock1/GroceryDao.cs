using Warczynski.Zbaszyniak.GroceryApp.DAOMock1.BO;
using Warczynski.Zbaszyniak.GroceryApp.DAOMock1.DataAccess;
using Warczynski.Zbaszyniak.GroceryApp.Interfaces;

namespace Warczynski.Zbaszyniak.GroceryApp.DAOMock1;

public class GroceryDao : IDAO
{
    private readonly GroceryContext _context;

    public GroceryDao()
    {
        _context = new GroceryContext();
    }

    public IEnumerable<IGrocery> GetAllGroceries()
    {
        return _context.Groceries.ToList();
    }

    public IEnumerable<IProduct> GetAllProducts()
    {
        return _context.Products.AsEnumerable();
    }

    public IGrocery SaveGrocery(IGrocery grocery)
    {
        var saved = _context.Groceries.Add((Grocery)grocery);
        _context.SaveChanges();
        return saved.Entity;
    }

    public IProduct SaveProduct(IProduct product)
    {
        var grocery = _context.Groceries.ToList()[0];
        Product productModel = new Product()
        {
            Name = product.Name,
            Price = product.Price,
            Category = product.Category,
            Magnesium = product.Magnesium,
            Potassium = product.Potassium,
            Sodium = product.Sodium,
            GroceryEntity = grocery
        };

        var saved = _context.Products.Add(productModel);
        _context.SaveChanges();
        return saved.Entity;
    }

    public IGrocery EditGrocery(IGrocery grocery)
    {
        var updated = _context.Groceries.Update((Grocery)grocery);
        _context.SaveChanges();
        return updated.Entity;
    }
    public IProduct EditProduct(IProduct product)
    {
        var updated = _context.Products.Update((Product)product);
        _context.SaveChanges();
        return updated.Entity;
    }

    public void DeleteGrocery(IGrocery grocery)
    {
        _context.Groceries.Remove((Grocery)grocery);
    }

    public IEnumerable<IProduct> GetProductsByFilter(IFilter filter)
    {
        var query = GetQuery(filter);
        return query.ToList();
    }

    private IQueryable<Product> GetQuery(IFilter filter)
    {
        IQueryable<Product> query = _context.Products;

        if (!string.IsNullOrEmpty(filter.Name))
        {
            query = query.Where(p => p.Name == filter.Name);
        }

        if (filter.Categories != null && filter.Categories.Any())
        {
            query = query.Where(p => filter.Categories.Contains(p.Category));
        }

        if (filter.MinPrice.HasValue)
        {
            query = query.Where(p => p.Price >= filter.MinPrice.Value);
        }

        if (filter.MaxPrice.HasValue)
        {
            query = query.Where(p => p.Price <= filter.MaxPrice.Value);
        }

        return query;
    }

    public void DeleteProduct(IProduct product)
    {
        var productToRemove = _context.Products.FirstOrDefault(p => p.Id == product.Id);
        if (productToRemove != null)
        {
            _context.Products.Remove(productToRemove);
            _context.SaveChanges();
        }
    }
}