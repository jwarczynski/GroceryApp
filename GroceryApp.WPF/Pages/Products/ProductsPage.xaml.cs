using System.Windows.Controls;
using GroceryApp.WPF.ViewModels;
using Warczynski.Zbaszyniak.GroceryApp.BLC;
using Warczynski.Zbaszyniak.GroceryApp.Core;
using Warczynski.Zbaszyniak.GroceryApp.Interfaces;

namespace GroceryApp.WPF.Pages.Products;

public partial class ProductsPage
{
    private readonly BLC _blc = BLCContainer.Instance;

    public static ProductCategory[] ProductCategoryEnumValues =>
        (ProductCategory[])Enum.GetValues(typeof(ProductCategory));

    public ProductsPage()
    {
        InitializeComponent();
        var viewModel = (ProductsViewModel)DataContext;
        viewModel.RequestNavigation += NavigateToProductDetail;
    }
    
    private void NavigateToProductDetail(IProduct product)
    {
        var viewModel = (ProductsViewModel)DataContext;
        var detailPage = new EditProductPage(product, viewModel.Products);
        NavigationService?.Navigate(detailPage);
    }
    
    // Property SelectedItems is readonly, so we need to handle the event
    private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var model = (ProductsViewModel)DataContext;
        model.Filter.Categories = ((ListBox)sender).SelectedItems.Cast<ProductCategory>().ToList();
    }
}