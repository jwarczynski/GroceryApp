﻿using Warczynski.Zbaszyniak.GroceryApp.DAOMock1.BO;
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
        _context.Groceries.Add((Grocery)grocery);
        _context.SaveChanges();
        return grocery;
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
        
        _context.Products.Add(productModel);
        _context.SaveChanges();
        return product;
    }
}