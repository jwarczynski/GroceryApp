using System.ComponentModel.DataAnnotations.Schema;
using Warczynski.Zbaszyniak.GroceryApp.Core;
using Warczynski.Zbaszyniak.GroceryApp.Interfaces;

namespace Warczynski.Zbaszyniak.GroceryApp.DAOMock1.BO;

public class Product : IProduct
{
    public int? Id { get; set; }
    public string Name { get; set; }
    public float Price { get; set; }
    public ProductCategory Category { get; set; }
    public float Magnesium { get; set; }
    public float Potassium { get; set; }
    public float Sodium { get; set; }
    public Grocery GroceryEntity { get; set; }

    [NotMapped]
    public IGrocery Grocery
    {
        get { return GroceryEntity; }
        set { GroceryEntity = (Grocery)value; }
    }
}