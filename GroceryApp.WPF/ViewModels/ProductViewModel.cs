using Warczynski.Zbaszyniak.GroceryApp.BLC;
using Warczynski.Zbaszyniak.GroceryApp.Interfaces;

namespace GroceryApp.WPF;

public class ProductViewModel
{
    public IProduct Product { get; set; }
    public IEnumerable<IGrocery> Groceries => BLCContainer.Instance.GetAllGroceries();
}