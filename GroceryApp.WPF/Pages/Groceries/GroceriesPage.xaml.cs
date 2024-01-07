using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using GroceryApp.WPF.Pages.Groceries;
using Warczynski.Zbaszyniak.GroceryApp.BLC;
using Warczynski.Zbaszyniak.GroceryApp.Interfaces;

namespace GroceryApp.WPF;

public partial class GroceriesPage : Page
{
    private readonly BLC _blc = BLCContainer.Instance;
    private readonly ObservableCollection<IGrocery> _groceries;
    
    public GroceriesPage()
    {
        InitializeComponent();
        _groceries = new ObservableCollection<IGrocery>(_blc.GetAllGroceries());
        GroceriesDataGrid.ItemsSource = _groceries;
    }

    private void Remove_Click(object sender, RoutedEventArgs e)
    {
        var button = (Button)sender;
        var grocery = (IGrocery)button.DataContext;
        _blc.DeleteGrocery(grocery);
        _groceries.Remove(grocery);;
    }

    private void Update_Click(object sender, RoutedEventArgs e)
    {
        var button = (Button)sender;
        var grocery = (IGrocery)button.DataContext;
        var detailPage = new EditGroceryPage(grocery);
        
        NavigationService?.Navigate(detailPage);;
    }

    private void Add_Click(object sender, RoutedEventArgs e)
    {
        var detailPage = new EditGroceryPage(_blc.GetGroceryTemplate());
        NavigationService?.Navigate(detailPage);
    }
}