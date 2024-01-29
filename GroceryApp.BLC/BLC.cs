using System.Reflection;
using Warczynski.Zbaszyniak.GroceryApp.Interfaces;

namespace Warczynski.Zbaszyniak.GroceryApp.BLC;

public class BLC : IDAO
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

    public IGrocery SaveGrocery(IGrocery grocery)
    {
        return _dao.SaveGrocery(grocery);
    }

    public IProduct SaveProduct(IProduct product)
    {
        return _dao.SaveProduct(product);
    }

    public IGrocery EditGrocery(IGrocery grocery)
    {
        return _dao.EditGrocery(grocery);
    }

    public IProduct EditProduct(IProduct product)
    {
        return _dao.EditProduct(product);
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

    public IGrocery GetGroceryTemplate()
    {
        return _dao.GetGroceryTemplate();
    }
    
    public IProduct GetProductWithGrocery(int id)
    {
        return _dao.GetProductWithGrocery(id);
    }

    public IProduct GetProductTemplate()
    {
        return _dao.GetProductTemplate();
    }

    public IEnumerable<IApplicationUser> GetApplicationUsers()
    {
        return _dao.GetApplicationUsers();
    }

    public IApplicationUser GetApplicationUser(int id)
    {
        return _dao.GetApplicationUser(id);
    }

    public IApplicationUser SaveApplicationUser(IApplicationUser user)
    {
        return _dao.SaveApplicationUser(user);
    }

    public IApplicationUser EditApplicationUser(IApplicationUser user)
    {
        return _dao.EditApplicationUser(user);
    }

    public void DeleteApplicationUser(IApplicationUser user)
    {
        _dao.DeleteApplicationUser(user);
    }
}