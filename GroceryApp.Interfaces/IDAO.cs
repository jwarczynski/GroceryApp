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
}