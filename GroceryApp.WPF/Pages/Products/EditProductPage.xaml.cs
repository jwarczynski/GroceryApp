using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using GroceryApp.WPF.Validations;
using Warczynski.Zbaszyniak.GroceryApp.BLC;
using Warczynski.Zbaszyniak.GroceryApp.Interfaces;

namespace GroceryApp.WPF;

public partial class EditProductPage : Page
{
    private ProductViewModel ViewModel => (ProductViewModel)DataContext;

    public EditProductPage(IProduct product)
    {
        InitializeComponent();
        var productToEdit = product.Id == null ? product : BLCContainer.Instance.GetProductWithGrocery(product.Id.Value);
        var viewModel = new ProductViewModel { Product = productToEdit };
        DataContext = viewModel;
    }

    private void Save_Click(object sender, RoutedEventArgs e)
    {
        if (IsUserInputInvalid()) return;
        var saved = ViewModel.Product.Id == null ? BLCContainer.Instance.SaveProduct(ViewModel.Product) : BLCContainer.Instance.EditProduct(ViewModel.Product);
        ShowSuccessMessage(saved);
        NavigationService?.GoBack();
    }

    private bool IsUserInputInvalid()
    {
        if (!ValidationHelper.AreAllTextBoxesValid(this))
        {
            return true;
        }

        if (ViewModel.Product.Grocery == null)
        {
            MessageBox.Show("Please select a grocery.");
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