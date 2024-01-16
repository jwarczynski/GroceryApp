using Warczynski.Zbaszyniak.GroceryApp.Interfaces;
using Warczynski.Zbaszyniak.GroceryApp.Core;

namespace Warczynski.Zbaszyniak.GroceryApp.MVC.Filters
{
    public class Filter : IFilter
    {
        public ICollection<ProductCategory>? Categories { get; set; } = new List<ProductCategory>();
        public string? Name { get; init; }

        public float? MinPrice { get; init; }
        public float? MaxPrice { get; init; }
    }
}
