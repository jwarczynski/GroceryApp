using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using GroceryApp.WPF.Commands;
using Warczynski.Zbaszyniak.GroceryApp.BLC;
using Warczynski.Zbaszyniak.GroceryApp.Core;
using Warczynski.Zbaszyniak.GroceryApp.Interfaces;

namespace GroceryApp.WPF.ViewModels;

public class Product: IProduct, INotifyPropertyChanged
{
    public int? Id { get; set; }
    public string Name { get; set; }
    public float Price { get; set; }
    public ProductCategory Category { get; set; }
    public float Magnesium { get; set; }
    public float Potassium { get; set; }
    public float Sodium { get; set; }
    public IGrocery Grocery { get; set; }
    public event PropertyChangedEventHandler? PropertyChanged;
    
    public Product(IProduct product)
    {
        Id = product.Id;
        Name = product.Name;
        Price = product.Price;
        Category = product.Category;
        Magnesium = product.Magnesium;
        Potassium = product.Potassium;
        Sodium = product.Sodium;
        Grocery = product.Grocery;
    }


    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    private void UpdateProduct()
    {
        // Your logic to update the product
        // You can use 'this' to refer to the current product instance

        // Notify about changes to the properties
        OnPropertyChanged(nameof(Name));
        OnPropertyChanged(nameof(Price));
        // Add similar lines for other properties
    }
}