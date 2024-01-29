namespace Warczynski.Zbaszyniak.GroceryApp.Interfaces;

public interface IDAO
{
    IEnumerable<IGrocery> GetAllGroceries();
    IEnumerable<IProduct> GetAllProducts();
    IEnumerable<IProduct> GetProductsByFilter(IFilter filter);
    IGrocery SaveGrocery(IGrocery grocery);
    IProduct SaveProduct(IProduct product);
    IGrocery EditGrocery(IGrocery grocery);
    IProduct EditProduct(IProduct product);
    void DeleteGrocery(IGrocery grocery);
    void DeleteProduct(IProduct product);
    IGrocery GetGroceryTemplate();
    IProduct GetProductWithGrocery(int id);
    IProduct GetProductTemplate();
    IEnumerable<IApplicationUser> GetApplicationUsers();
    IApplicationUser GetApplicationUser(int id);
    IApplicationUser SaveApplicationUser(IApplicationUser user);
    IApplicationUser EditApplicationUser(IApplicationUser user);
    void DeleteApplicationUser(IApplicationUser user);
}