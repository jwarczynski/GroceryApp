using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using Warczynski.Zbaszyniak.GroceryApp.Interfaces;
using Warczynski.Zbaszyniak.GroceryApp.MVC.Commands;

namespace Warczynski.Zbaszyniak.GroceryApp.MVC.Models
{
    public class GroceriesViewModel
    {
        public event NotifyCollectionChangedEventHandler? CollectionChanged;
        public event PropertyChangedEventHandler? PropertyChanged;

        public RelayCommand ApplyFiltersCommand { get; }
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
        private readonly IDAO _blc;

        public GroceriesViewModel(IDAO blc)
        {
            _blc = blc;
            _groceries = new ObservableCollection<IGrocery>(_blc.GetAllGroceries());
            ApplyFiltersCommand = new RelayCommand(ApplyFilters);
            RemoveCommand = new RelayCommand(RemoveGrocery);
            AddCommand = new RelayCommand(AddGrocery);
            UpdateGroceryCommand = new RelayCommand(UpdateGrocery);
        }

        private void ApplyFilters(object parameter)
        {
            if (parameter is string s)
            {
                if (!string.IsNullOrEmpty(s))
                Groceries = new ObservableCollection<IGrocery>(Groceries.Where(g => g.Name.Contains(s)));
                OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
            }
        }

        private void RemoveGrocery(object parameter)
        {
            if (parameter is IGrocery grocery)
            {
                _blc.DeleteGrocery(grocery);
                Groceries.Remove(grocery);
            }
        }

        private void UpdateGrocery(object parameter)
        {
            if (parameter is IGrocery grocery)
            {
                _blc.EditGrocery(grocery);
                Groceries.Remove(Groceries.Where(g => g.Id == grocery.Id).Single());
                Groceries.Add(grocery);
            }
        }

        private void AddGrocery(object parameter)
        {
            if (parameter is IGrocery grocery)
            {
                _blc.SaveGrocery(grocery);
                Groceries.Add(grocery);
            }
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
}
