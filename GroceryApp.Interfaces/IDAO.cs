namespace Warczynski.Zbaszyniak.GroceryApp.Interfaces;

public interface IDAO
{
    IEnumerable<IGrocery> GetAllGroceries();
    IEnumerable<IProduct> GetAllProducts();
    IGrocery SaveGrocery(IGrocery grocery);
    IProduct SaveProduct(IProduct product);
    IEnumerable<IProduct> GetProductsByFilter(IFilter filter);
}