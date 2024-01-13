using System.Windows;
using System.Windows.Input;
using GroceryApp.WPF.Commands;
using Warczynski.Zbaszyniak.GroceryApp.BLC;
using Warczynski.Zbaszyniak.GroceryApp.Interfaces;

namespace GroceryApp.WPF.ViewModels;

public class ProductViewModel
{
    public event Action<IProduct>? RequestNavigation;

    public IProduct? Product { get; init; }
    public IEnumerable<IGrocery> Groceries { get; }

    public ICommand SaveCommand { get; }

    public ProductViewModel()
    {
        SaveCommand = new RelayCommand(SaveMethod);
        Groceries = BLCContainer.Instance.GetAllGroceries();
    }
    
    private void SaveMethod(object parameter)
    {
        if (IsUserInputInvalid()) return;
        var saved = Product!.Id == null ? BLCContainer.Instance.SaveProduct(Product) : BLCContainer.Instance.EditProduct(Product);
        ShowSuccessMessage(saved);
        RequestNavigation?.Invoke(saved);
    }

    private bool IsUserInputInvalid()
    {
        if (Product?.Price == null || Product.Price <= 0)
        {
            MessageBox.Show("Price should be greater than 0.");
            return true;
        }
        
        if (Product?.Magnesium == null || Product.Magnesium <= 0)
        {
            MessageBox.Show("Magnesium should be greater than 0.");
            return true;
        }
        
        if (Product?.Potassium == null || Product.Potassium <= 0)
        {
            MessageBox.Show("Potassium should be greater than 0.");
            return true;
        }
        
        if (Product?.Sodium == null || Product.Sodium <= 0)
        {
            MessageBox.Show("Sodium should be greater than 0.");
            return true;
        }
        
        if (Product?.Grocery == null)
        {
            MessageBox.Show("Please select a grocery.");
            return true;
        }
        
        if (string.IsNullOrEmpty(Product?.Name))
        {
            MessageBox.Show("Please enter a name.");
            return true;
        }
        return false;
    }

    private static void ShowSuccessMessage(IProduct saved)
    {
        MessageBox.Show($"Saved Product: " + "\n" +
                        $" Id:  {saved.Id}" + "\n" +
                        $" Name: {saved.Name}" + "\n" +
                        $" Price: {saved.Price}" + "\n" +
                        $" Category: {saved.Category}" + "\n" +
                        $" Magnesium {saved.Magnesium}" + "\n" +
                        $" Potassium {saved.Potassium}" + "\n" +
                        $" Sodium: {saved.Sodium}");
    }
}