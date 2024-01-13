using GroceryApp.WPF.ViewModels;
using Warczynski.Zbaszyniak.GroceryApp.Interfaces;

namespace GroceryApp.WPF.Pages.Groceries;

public partial class GroceriesPage
{
    
    public GroceriesPage()
    {
        InitializeComponent();
        var viewModel = (GroceriesViewModel)DataContext;
        viewModel.RequestNavigation += NavigateToGroceriesDetail;
    }

    private void NavigateToGroceriesDetail(IGrocery grocery)
    {
        var viewModel = (GroceriesViewModel)DataContext;
        var detailPage = new EditGroceryPage(grocery, viewModel.Groceries);
        NavigationService?.Navigate(detailPage);
    }
}