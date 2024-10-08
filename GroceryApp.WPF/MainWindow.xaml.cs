﻿using System.Windows;
using GroceryApp.WPF.Pages.Groceries;
using GroceryApp.WPF.Pages.Products;

namespace GroceryApp.WPF;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        MainFrame.Navigate(new ProductsPage());
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