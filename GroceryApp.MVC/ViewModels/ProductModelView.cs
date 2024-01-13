using Warczynski.Zbaszyniak.GroceryApp.Interfaces;
using Warczynski.Zbaszyniak.GroceryApp.MVC.ViewModels;

namespace Warczynski.Zbaszyniak.GroceryApp.MVC.ViewModels
{
    public class ProductModelView
    {
        public Product Product { get; set; } = new Product();
        public IEnumerable<int> GroceriesIds { get; set; } = new List<int>();
    }
}
