using System.Windows;
using System.Windows.Controls;
using Warczynski.Zbaszyniak.GroceryApp.Core;
using Warczynski.Zbaszyniak.GroceryApp.Interfaces;

namespace GroceryApp.WPF;

public partial class AddProductControl : UserControl
{
    public event EventHandler<ProductEventArgs> ProductSaved;
    
    public AddProductControl()
    {
        InitializeComponent();
    }
    
    private void Save_Click(object sender, RoutedEventArgs e)
    {
        //
        //
        // TODO: Validate input 
        var product = new Product
        {
            Name = txtName.Text,
            Price = float.Parse(txtPrice.Text),
            Category = (ProductCategory)cmbCategory.SelectedItem,
            Magnesium = float.Parse(txtMagnesium.Text),
            Potassium = float.Parse(txtPotassium.Text),
            Sodium = float.Parse(txtSodium.Text),
            // Grocery = (GroceryType)cmbGrocery.SelectedItem
        };

        // Raise the event to notify the main page about the saved product
        ProductSaved?.Invoke(this, new ProductEventArgs(product));

        // Close the UserControl
        Visibility = Visibility.Collapsed;
    }

    private void TextBox_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
    {
        // Allow only numeric input in TextBoxes
        e.Handled = !double.TryParse(e.Text, out _);
    }
}