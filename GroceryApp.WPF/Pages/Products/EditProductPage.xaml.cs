using System.Windows;
using System.Windows.Controls;
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
        var saved = ViewModel.Product.Id == null ? BLCContainer.Instance.SaveProduct(ViewModel.Product) : BLCContainer.Instance.EditProduct(ViewModel.Product);
        ShowSuccessMessage(saved);
        NavigationService?.GoBack();
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