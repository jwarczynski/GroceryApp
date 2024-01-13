using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using GroceryApp.WPF.Commands;
using Warczynski.Zbaszyniak.GroceryApp.BLC;
using Warczynski.Zbaszyniak.GroceryApp.Interfaces;

namespace GroceryApp.WPF.ViewModels;

public class GroceriesViewModel : INotifyCollectionChanged, INotifyPropertyChanged
{
    public event Action<IGrocery>? RequestNavigation;
   
    public event NotifyCollectionChangedEventHandler? CollectionChanged;
    public event PropertyChangedEventHandler? PropertyChanged;
    
    public RelayCommand RemoveCommand { get; }
    public RelayCommand UpdateGroceryCommand { get; }
    public RelayCommand AddCommand { get; }
    
    private ObservableCollection<IGrocery> _groceries;
    public ObservableCollection<IGrocery> Groceries
    {
        get => _groceries;
        private set
        {
            if (_groceries != value)
            {
                _groceries = value;
                OnPropertyChanged(nameof(Groceries));
            }
        }
    }
    public IGrocery? SelectedGrocery { get; set; }
    private readonly BLC _blc = BLCContainer.Instance;
    
    public GroceriesViewModel()
    {
        _groceries = new ObservableCollection<IGrocery>(_blc.GetAllGroceries());
        RemoveCommand = new RelayCommand(RemoveGrocery);
        AddCommand = new RelayCommand(AddGrocery);
        UpdateGroceryCommand = new RelayCommand(UpdateGrocery);
    }

    private void RemoveGrocery(object parameter)
    {
        if (parameter is IGrocery Grocery)
        {
            _blc.DeleteGrocery(Grocery);
            Groceries.Remove(Grocery);
        }
    }

    private void UpdateGrocery(object parameter)
    {
        if (parameter is IGrocery grocery)
        {
            RequestNavigation?.Invoke(grocery);
        }
    }
    
    private void AddGrocery(object parameter)
    {
        RequestNavigation?.Invoke(_blc.GetGroceryTemplate());
    }

    public void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
    {
        CollectionChanged?.Invoke(this, e);
    }

    private void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

}