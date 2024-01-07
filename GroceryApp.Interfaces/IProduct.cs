using Warczynski.Zbaszyniak.GroceryApp.Core;

namespace Warczynski.Zbaszyniak.GroceryApp.Interfaces;

public interface IProduct
{
    int? Id { get; set; }
    string Name { get; set; }
    float Price { get; set; }
    ProductCategory Category { get; set; }
    float Magnesium { get; set; }
    float Potassium { get; set; }
    float Sodium { get; set; }
    IGrocery Grocery { get; set; }
}