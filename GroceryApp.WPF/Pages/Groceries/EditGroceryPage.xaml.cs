using System.Windows;
using System.Windows.Controls;
using Warczynski.Zbaszyniak.GroceryApp.BLC;
using Warczynski.Zbaszyniak.GroceryApp.Interfaces;

namespace GroceryApp.WPF.Pages.Groceries;

public partial class EditGroceryPage : Page
{
    private IGrocery Grocery { get; }
    public EditGroceryPage(IGrocery grocery)
    {
        InitializeComponent();
        Grocery = grocery;
        DataContext = Grocery;
    }

    private void Save_Click(object sender, RoutedEventArgs e)
    {
        var saved = Grocery.Id == null ? BLCContainer.Instance.SaveGrocery(Grocery) : BLCContainer.Instance.EditGrocery(Grocery);
        ShowSuccessMessage(saved);
        NavigationService?.GoBack();
    }

    private void ShowSuccessMessage(IGrocery saved)
    {
        MessageBox.Show($"Saved Grocery: " + "\n" +
                        $" Id:  {saved.Id}" + "\n" +
                        $" Name: {saved.Name}" + "\n" +
                        $" Address: {saved.Address}");
    }
}