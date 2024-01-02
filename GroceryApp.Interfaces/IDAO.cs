namespace Warczynski.Zbaszyniak.GroceryApp.Interfaces;

public interface IDAO
{
    IEnumerable<IGrocery> GetAllGroceries();
    IEnumerable<IProduct> GetAllProducts();
    IGrocery CreateGrocery();
    IProduct CreateProduct();
}