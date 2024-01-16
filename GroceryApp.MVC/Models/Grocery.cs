using Warczynski.Zbaszyniak.GroceryApp.Interfaces;

namespace Warczynski.Zbaszyniak.GroceryApp.MVC.Models
{
    public class Grocery : IGrocery
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }

        public ICollection<Product> Products { get; set; } = new List<Product>();
    }
}
