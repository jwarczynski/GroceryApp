using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Windows.Input;
using GroceryApp.WPF.Commands;
using GroceryApp.WPF.Filters;
using Warczynski.Zbaszyniak.GroceryApp.BLC;
using Warczynski.Zbaszyniak.GroceryApp.Interfaces;

namespace GroceryApp.WPF.ViewModels;

public class ProductsViewModel : INotifyCollectionChanged, INotifyPropertyChanged
{
    public event Action<IProduct>? RequestNavigation;

    public event NotifyCollectionChangedEventHandler? CollectionChanged;
    public event PropertyChangedEventHandler? PropertyChanged;

    public ICommand ApplyFiltersCommand { get; }
    public RelayCommand RemoveCommand { get; }
    public RelayCommand UpdateProductCommand { get; }
    public RelayCommand AddCommand { get; }

    public Filter Filter { get; }
    private ObservableCollection<IProduct> _products;

    public ObservableCollection<IProduct> Products
    {
        get => _products;
        private set
        {
            if (_products != value)
            {
                _products = value;
                OnPropertyChanged(nameof(Products));
            }
        }
    }

    public IProduct? SelectedProduct { get; set; }

    private readonly BLC _blc = BLCContainer.Instance;

    public ProductsViewModel()
    {
        _products = new ObservableCollection<IProduct>(_blc.GetAllProducts());
        RemoveCommand = new RelayCommand(RemoveProduct);
        AddCommand = new RelayCommand(AddProduct);
        UpdateProductCommand = new RelayCommand(UpdateProduct);
        Filter = new Filter() {MaxPrice = 1000};
        ApplyFiltersCommand = new RelayCommand(ApplyFilters);
    }

    private void ApplyFilters(object parameter)
    {
        Products = new ObservableCollection<IProduct>(_blc.GetProductsByFilter(Filter));
        OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
    }
    
    private void RemoveProduct(object parameter)
    {
        if (parameter is IProduct product)
        {
            _blc.DeleteProduct(product);
            Products.Remove(product);
        }
    }

    private void UpdateProduct(object parameter)
    {
        if (parameter is IProduct product)
        {
            RequestNavigation?.Invoke(product);
        }
    }
    
    private void AddProduct(object parameter)
    {
        RequestNavigation?.Invoke(_blc.GetProductTemplate());
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
