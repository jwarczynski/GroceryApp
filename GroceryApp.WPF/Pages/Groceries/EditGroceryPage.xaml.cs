using System.Collections.ObjectModel;
using GroceryApp.WPF.ViewModels;
using Warczynski.Zbaszyniak.GroceryApp.Interfaces;

namespace GroceryApp.WPF.Pages.Groceries;

public partial class EditGroceryPage
{
    ObservableCollection<IGrocery> Groceries { get; set; }

    public EditGroceryPage(IGrocery grocery, ObservableCollection<IGrocery> groceries)
    {
        InitializeComponent();
        Groceries = groceries;
        var viewModel = new GroceryViewModel() {Grocery = grocery};
        viewModel.RequestNavigation += NavigateBack;
        DataContext = viewModel;
    }
    
    private void NavigateBack(IGrocery grocery)
    {
        if (grocery.Id != 0 && grocery.Id != null && Groceries.All(g => g.Id != grocery.Id))
        {
            Groceries.Add(grocery);
        }
        NavigationService?.GoBack();
    }

}