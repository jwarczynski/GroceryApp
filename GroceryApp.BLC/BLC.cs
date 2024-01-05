using System.Reflection;
using Warczynski.Zbaszyniak.GroceryApp.Interfaces;

namespace Warczynski.Zbaszyniak.GroceryApp.BLC;

public class BLC
{
    private IDAO _dao;

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
    
    public IProduct SaveProduct(IProduct product)
    {
        return _dao.SaveProduct(product);
    }
}