using System.Collections.ObjectModel;
using GroceryApp.WPF.ViewModels;
using Warczynski.Zbaszyniak.GroceryApp.BLC;
using Warczynski.Zbaszyniak.GroceryApp.Interfaces;

namespace GroceryApp.WPF.Pages.Products;

public partial class EditProductPage
{
    ObservableCollection<IProduct> Products { get; set; }

    public EditProductPage(IProduct product, ObservableCollection<IProduct> products)
    {
        InitializeComponent();
        Products = products;
        var productToEdit = product.Id == null ? product : BLCContainer.Instance.GetProductWithGrocery(product.Id.Value);
        var viewModel = new ProductViewModel() { Product = productToEdit};
        viewModel.RequestNavigation += NavigateBack;
        DataContext = viewModel;
    }
    
    private void NavigateBack(IProduct product)
    {
        if (product.Id != 0 && product.Id != null && Products.All(p => p.Id != product.Id))
        {
            Products.Add(product);
        }
        NavigationService?.GoBack();
    }

}