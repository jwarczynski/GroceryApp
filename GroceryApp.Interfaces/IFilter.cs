using Warczynski.Zbaszyniak.GroceryApp.Core;

namespace Warczynski.Zbaszyniak.GroceryApp.Interfaces;

public interface IFilter
{
    public ICollection<ProductCategory>? Categories { get; }
    public string? Name { get; }
    public float? MinPrice { get; }
    public float? MaxPrice { get; }
}