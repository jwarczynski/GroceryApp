using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using Warczynski.Zbaszyniak.GroceryApp.BLC;
using Warczynski.Zbaszyniak.GroceryApp.Core;
using Warczynski.Zbaszyniak.GroceryApp.Interfaces;

namespace GroceryApp.WPF;

public partial class ProductsPage : Page
{
    private readonly BLC _blc = BLCContainer.Instance;
    private readonly ObservableCollection<IProduct> _products;

    public static ProductCategory[] ProductCategoryEnumValues => (ProductCategory[])Enum.GetValues(typeof(ProductCategory));

    public ProductsPage()
    {
        InitializeComponent();
        BLC blc = new BLC("GroceryApp.DAOMock1.dll");
        _products = new ObservableCollection<IProduct>(blc.GetAllProducts());
        ProductsDataGrid.ItemsSource = _products;
    }

    private void Add_Click(object sender, RoutedEventArgs e)
    {
        // Show the AddProductControl
        addProductControl.Visibility = Visibility.Visible;;
    }

    private void Remove_Click(object sender, RoutedEventArgs e)
    {
        throw new NotImplementedException();
    }

    private void Update_Click(object sender, RoutedEventArgs e)
    {
        throw new NotImplementedException();
    }

    private void ApplyFilters_Click(object sender, RoutedEventArgs e)
    {
        throw new NotImplementedException();
    }

    private void AddProductControl_ProductSaved(object? sender, ProductEventArgs e)
    {
        var product = _blc.SaveProduct(e.Product);
        _products.Add(product);
        
        MessageBox.Show($"Product saved: {product}");;
    }
}