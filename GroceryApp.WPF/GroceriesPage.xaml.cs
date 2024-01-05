using System.Collections.ObjectModel;
using System.Windows.Controls;
using Warczynski.Zbaszyniak.GroceryApp.BLC;
using Warczynski.Zbaszyniak.GroceryApp.Interfaces;

namespace GroceryApp.WPF;

public partial class GroceriesPage : Page
{
    public GroceriesPage()
    {
        InitializeComponent();
        BLC blc = new BLC("GroceryApp.DAOMock1.dll");
        GroceriesDataGrid.ItemsSource = new ObservableCollection<IGrocery>(blc.GetAllGroceries());
    }

}