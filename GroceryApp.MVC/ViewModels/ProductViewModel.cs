using Warczynski.Zbaszyniak.GroceryApp.Interfaces;
using Warczynski.Zbaszyniak.GroceryApp.MVC.ViewModels;

namespace Warczynski.Zbaszyniak.GroceryApp.MVC.ViewModels
{
    public class ProductViewModel
    {
        public Product Product { get; set; } = new Product();
        public IEnumerable<IGrocery> Groceries { get; set; } = new List<Grocery>();
    }
}
