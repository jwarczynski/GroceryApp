using System.Reflection;
using Warczynski.Zbaszyniak.GroceryApp.Interfaces;

namespace Warczynski.Zbaszyniak.GroceryApp.BLC;

public class BLC
{
    private readonly IDAO _dao;

    public BLC(string libraryName)
    {
        Type? typeToCreate = null;
        Assembly? assembly = Assembly.UnsafeLoadFrom(libraryName);

        if (assembly != null)
        {
            foreach (Type type in assembly.GetExportedTypes())
            {
                if (type.IsAssignableTo(typeof(IDAO)))
                {
                    typeToCreate = type;
                    break;
                }
            }
        }

        _dao = (IDAO)Activator.CreateInstance(typeToCreate, null);
    }

    public IEnumerable<IGrocery> GetAllGroceries()
    {
        return _dao.GetAllGroceries();
    }

    public IEnumerable<IProduct> GetAllProducts()
    {
        return _dao.GetAllProducts();
    }

    public void SaveGrocery(IGrocery grocery)
    {
        _dao.SaveGrocery(grocery);
    }

    public void SaveProduct(IProduct product)
    {
        _dao.SaveProduct(product);
    }

    public void EditGrocery(IGrocery grocery)
    {
        _dao.EditGrocery(grocery);
    }

    public void EditProduct(IProduct product)
    {
        _dao.EditProduct(product);
    }

    public void DeleteGrocery(IGrocery grocery)
    {
        _dao.DeleteGrocery(grocery);
    }

    public void DeleteProduct(IProduct product)
    {
        _dao.DeleteProduct(product);
    }

    public IEnumerable<IProduct> GetProductsByFilter(IFilter filter)
    {
        return _dao.GetProductsByFilter(filter);
    }
}