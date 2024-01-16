using Warczynski.Zbaszyniak.GroceryApp.Interfaces;

namespace Models
{
    public class ProductViewModel
    {
        public Product Product { get; set; } = new Product();
        public IEnumerable<IGrocery> Groceries { get; set; } = new List<Grocery>();
    }
}
