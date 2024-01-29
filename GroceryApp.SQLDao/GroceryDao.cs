using Microsoft.EntityFrameworkCore;
using Warczynski.Zbaszyniak.GroceryApp.Interfaces;
using Warczynski.Zbaszyniak.GroceryApp.SQLDao.BO;
using Warczynski.Zbaszyniak.GroceryApp.SQLDao.DataAccess;

namespace Warczynski.Zbaszyniak.GroceryApp.SQLDao;

public class GroceryDao : IDAO
{
    private readonly GroceryContext _context;

    public GroceryDao()
    {
        _context = new GroceryContext();
    }

    public IEnumerable<IGrocery> GetAllGroceries()
    {
        return _context.Groceries.AsEnumerable();
    }

    public IEnumerable<IProduct> GetAllProducts()
    {
        var products = new List<IProduct>();
        foreach (Product product in _context.Products)
        {
            if (product.Id.HasValue)
            {
                products.Add(GetProductWithGrocery((int)product.Id));
            }
            else
            {
                products.Add(product);
            }
        }
        return products;
    }

    public IGrocery SaveGrocery(IGrocery grocery)
    {
        var saved = _context.Groceries.Add(CastToGrocery(grocery));
        _context.SaveChanges();
        return saved.Entity;
    }

    public IProduct SaveProduct(IProduct product)
    {   
        var saved = _context.Products.Add(CastToProduct(product));
        _context.SaveChanges();
        return saved.Entity;
    }

    public IGrocery EditGrocery(IGrocery grocery)
    {
        _context.Entry(_context.Groceries.Where(g => g.Id == grocery.Id).Single()).State = EntityState.Detached;
        var updated = _context.Groceries.Update(CastToGrocery(grocery));
        _context.SaveChanges();
        return updated.Entity;
    }
    public IProduct EditProduct(IProduct product)
    {
        _context.Entry(_context.Products.Where(p => p.Id == product.Id).Single()).State = EntityState.Detached;
        var productModel = CastToProduct(product);
        var updated = _context.Products.Update(productModel);
        _context.SaveChanges();
        return updated.Entity;
    }

    public void DeleteGrocery(IGrocery grocery)
    {
        var groceryToRemove = _context.Groceries.FirstOrDefault(g => g.Id == grocery.Id);
        if (groceryToRemove != null)
        {
            _context.Groceries.Remove(groceryToRemove);
            _context.SaveChanges();
        }
    }

    public IEnumerable<IProduct> GetProductsByFilter(IFilter filter)
    {
        var query = GetQuery(filter);
        var products = query.ToList();
        products.ForEach(p => GetProductWithGrocery((int)p.Id));
        return products;
    }

    private IQueryable<Product> GetQuery(IFilter filter)
    {
        IQueryable<Product> query = _context.Products;

        if (!string.IsNullOrEmpty(filter.Name))
        {
            query = query.Where(p => p.Name.Contains(filter.Name));
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
    
    public IProduct GetProductWithGrocery(int id)
    {
        return _context.Products
            .Include(p => p.GroceryEntity)
            .FirstOrDefault(p => p.Id == id) ?? throw new InvalidOperationException();
    }

    public IGrocery GetGroceryTemplate()
    {
        return new Grocery();
    }

    public IProduct GetProductTemplate()
    {
        return new Product();
    }

    private Product CastToProduct(IProduct product)
    {
        var newProduct = new Product()
        {
            Id = product.Id,
            Name = product.Name,
            Price = product.Price,
            Category = product.Category,
            Magnesium = product.Magnesium,
            Potassium = product.Potassium,
            Sodium = product.Sodium,
            Grocery = product.Grocery,
        };
        return newProduct;
    }

    private Grocery CastToGrocery(IGrocery grocery)
    {
        var newGrocery =  new Grocery()
        {
            Id = grocery.Id,
            Name = grocery.Name,
            Address = grocery.Address,
        };
        return newGrocery;
    }

    private ApplicationUser CastToApplicationUser(IApplicationUser applicationUser)
    {
        var newApplicationUser = new ApplicationUser()
        {
            Name = applicationUser.Name,
            Password = applicationUser.Password,
        };
        return newApplicationUser;
    }

    public IEnumerable<IApplicationUser> GetApplicationUsers()
    {
        return _context.Users.AsEnumerable();
    }

    public IApplicationUser GetApplicationUser(string name)
    {
        return _context.Users.Where(u => u.Name == name).Single();
    }

    public IApplicationUser? SaveApplicationUser(IApplicationUser user)
    {
        if (_context.Users.Where(u => u.Name == user.Name).Any()) return null;
        var saved = _context.Add(CastToApplicationUser(user));
        _context.SaveChanges();
        return saved.Entity;
    }

    public IApplicationUser EditApplicationUser(IApplicationUser user)
    {
        _context.Entry(_context.Users.Where(u => u.Name == user.Name).Single()).State = EntityState.Detached;
        var applicationUserModel = CastToApplicationUser(user);
        var updated = _context.Users.Update(applicationUserModel);
        _context.SaveChanges();
        return updated.Entity;
    }

    public void DeleteApplicationUser(IApplicationUser user)
    {
        var userToRemove = _context.Users.FirstOrDefault(u => u.Name == user.Name);
        if (userToRemove != null)
        {
            _context.Users.Remove(userToRemove);
            _context.SaveChanges();
        }
    }
}