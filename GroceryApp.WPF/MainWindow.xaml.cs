using System.Windows;

namespace GroceryApp.WPF;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    private void ShowGroceries_Click(object sender, RoutedEventArgs e)
    {
        // Navigate to the GroceriesPage
        MainFrame.Navigate(new GroceriesPage());
    }

    private void ShowProducts_Click(object sender, RoutedEventArgs e)
    {
       // Navigate to the ProductsPage
       MainFrame.Navigate(new ProductsPage());
    }
}