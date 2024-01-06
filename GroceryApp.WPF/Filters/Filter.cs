using Warczynski.Zbaszyniak.GroceryApp.Core;
using Warczynski.Zbaszyniak.GroceryApp.Interfaces;

namespace GroceryApp.WPF.Filters;

public record Filter: IFilter
{
    public ICollection<ProductCategory>? Categories { get; init; } = new List<ProductCategory>();
    public string? Name { get; init; }
    
    public float? MinPrice { get; init; }
    public float? MaxPrice { get; init; }
}
