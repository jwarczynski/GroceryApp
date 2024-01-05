using Warczynski.Zbaszyniak.GroceryApp.Interfaces;

namespace GroceryApp.WPF;

public class ProductEventArgs : EventArgs
{
    public IProduct Product { get; }

    public ProductEventArgs(IProduct product)
    {
        Product = product ?? throw new ArgumentNullException(nameof(product));
    }
}