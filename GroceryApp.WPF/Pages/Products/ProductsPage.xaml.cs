using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using GroceryApp.WPF.ExtensionClass;
using GroceryApp.WPF.Filters;
using Warczynski.Zbaszyniak.GroceryApp.BLC;
using Warczynski.Zbaszyniak.GroceryApp.Core;
using Warczynski.Zbaszyniak.GroceryApp.Interfaces;

namespace GroceryApp.WPF;

public partial class ProductsPage : Page
{
    private readonly BLC _blc = BLCContainer.Instance;
    private readonly ObservableCollection<IProduct> _products;

    public static ProductCategory[] ProductCategoryEnumValues =>
        (ProductCategory[])Enum.GetValues(typeof(ProductCategory));

    public ProductCategory SelectedCategory { get; set; }
    private List<ProductCategory> SelectedCategories { get; set; } = new List<ProductCategory>();
    
    public string? NameFilter { get; set; }
    public float MinPrice { get; set;}
    public float MaxPrice { get; set; }

    public ProductsPage()
    {
        InitializeComponent();
        _products = new ObservableCollection<IProduct>(_blc.GetAllProducts());
        ProductsDataGrid.ItemsSource = _products;
        IsVisibleChanged += ProductsPage_IsVisibleChanged;
    }

    private void Add_Click(object sender, RoutedEventArgs e)
    {
        var detailPage = new EditProductPage(_blc.GetProductTemplate());
        NavigationService?.Navigate(detailPage);
        
        // AddProductControl.Visibility = Visibility.Visible;
    }

    private void Remove_Click(object sender, RoutedEventArgs e)
    {
        var button = (Button)sender;
        var product = (IProduct)button.DataContext;
        _blc.DeleteProduct(product);
        _products.Remove(product);
    }

    private void Update_Click(object sender, RoutedEventArgs e)
    {
        var button = (Button)sender;
        var product = (IProduct)button.DataContext;
        var detailPage = new EditProductPage(product);
        
        NavigationService.Navigate(detailPage);
    }

    private void ApplyFilters_Click(object sender, RoutedEventArgs e)
    {
        _products.Clear();
        _products.AddRange(_blc.GetProductsByFilter(CreateFilter()));
    }

    private Filter CreateFilter()
    {
        Filter myFilter = new Filter
        {
            // Categories = new List<ProductCategory> { SelectedCategory },
            Categories = SelectedCategories,
            Name = NameFilter,
            MinPrice = MinPrice,
            MaxPrice = MaxPrice
        };
        return myFilter;
    }

    private void AddProductControl_ProductSaved(object? sender, ProductEventArgs e)
    {
        var saved = _blc.SaveProduct(e.Product);
        _products.Add(saved);
        MessageBox.Show($"Product saved: {saved.Name}");
    }
    
    private void ProductsPage_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
    {
        if ((bool)e.NewValue)
        {
            _products.Clear();
            _products.AddRange(_blc.GetAllProducts());
        }
    }

    private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        SelectedCategories = ((ListBox)sender).SelectedItems.Cast<ProductCategory>().ToList();;
    }
}