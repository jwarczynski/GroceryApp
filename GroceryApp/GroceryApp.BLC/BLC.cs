using GroceryApp.Interfaces;
using System.Reflection;

namespace GroceryApp.BLC;

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
        
        _dao = (IDAO) Activator.CreateInstance(typeToCreate, null);
    }
    
    public IEnumerable<IGrocery> GetAllGroceries()
    {
        return _dao.GetAllGroceries();
    }
    
    public IEnumerable<IProduct> GetAllProducts()
    {
        return _dao.GetAllProducts();
    }
}