using System.Windows;
using System.Windows.Controls;
using GroceryApp.WPF.Validations;
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
        if (!ValidationHelper.AreAllTextBoxesValid(this))
        {
            return;
        }
        if (Grocery.Address == null || string.IsNullOrWhiteSpace(Grocery.Address))
        {
            MessageBox.Show("PLease enter address.");
            return;
        }
        if (Grocery.Name == null || string.IsNullOrWhiteSpace(Grocery.Name))
        {
            MessageBox.Show("PLease enter name.");
            return;
        }
        
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