using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
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
        // Get all TextBoxes in the form
        var textBoxes = FindVisualChildren<TextBox>(this);

        // Check each TextBox for validation errors
        foreach (var textBox in textBoxes)
        {
            if (Validation.GetHasError(textBox) || string.IsNullOrWhiteSpace(textBox.Text))
            {
                MessageBox.Show("Please correct the errors before saving.");
                return;
            }
        }
        if (ViewModel.Product.Grocery == null)
        {
            MessageBox.Show("Please select a grocery.");
            return;
        }
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
    
    public static IEnumerable<T> FindVisualChildren<T>(DependencyObject depObj) where T : DependencyObject
    {
        if (depObj != null)
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(depObj, i);
                if (child != null && child is T)
                {
                    yield return (T)child;
                }

                foreach (T childOfChild in FindVisualChildren<T>(child))
                {
                    yield return childOfChild;
                }
            }
        }
    }

}