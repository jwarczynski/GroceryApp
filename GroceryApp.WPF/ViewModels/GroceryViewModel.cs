using System.Windows;
using System.Windows.Input;
using GroceryApp.WPF.Commands;
using Warczynski.Zbaszyniak.GroceryApp.BLC;
using Warczynski.Zbaszyniak.GroceryApp.Interfaces;

namespace GroceryApp.WPF.ViewModels;

public class GroceryViewModel
{
    public event Action<IGrocery>? RequestNavigation;

    public IGrocery? Grocery { get; set; }

    public ICommand SaveCommand { get; }

    public GroceryViewModel(IGrocery grocery)
    {
        Grocery = grocery;
        SaveCommand = new RelayCommand(SaveMethod);
    }
    
    public GroceryViewModel()
    {
        SaveCommand = new RelayCommand(SaveMethod);
    }
    
    private void SaveMethod(object parameter)
    {
        if (IsUserInputInvalid()) return;
        var saved = Grocery!.Id == null ? BLCContainer.Instance.SaveGrocery(Grocery) : BLCContainer.Instance.EditGrocery(Grocery);
        ShowSuccessMessage(saved);
        RequestNavigation?.Invoke(saved);
    }

    private bool IsUserInputInvalid()
    {
        if (string.IsNullOrEmpty(Grocery?.Name))
        {
            MessageBox.Show("Please enter a name.");
            return true;
        }
        
        if (string.IsNullOrEmpty(Grocery?.Address))
        {
            MessageBox.Show("Please enter an address.");
            return true;
        }
        return false;
    }

    private static void ShowSuccessMessage(IGrocery saved)
    {
        MessageBox.Show($"Saved Grocery: " + "\n" +
                        $" Id:  {saved.Id}" + "\n" +
                        $" Name: {saved.Name}" + "\n" +
                        $" Address: {saved.Address}");
    }
}