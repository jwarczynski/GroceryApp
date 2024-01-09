using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace GroceryApp.WPF.Validations;

public static class ValidationHelper
{
    public static bool AreAllTextBoxesValid(DependencyObject depObj)
    {
        // Get all TextBoxes in the form
        var textBoxes = FindVisualChildren<TextBox>(depObj);

        // Check each TextBox for validation errors
        foreach (var textBox in textBoxes)
        {
            if (Validation.GetHasError(textBox) || string.IsNullOrWhiteSpace(textBox.Text))
            {
                MessageBox.Show("Please correct the errors before saving.");
                return false;
            }
        }

        return true;
    }
    
    private static IEnumerable<T> FindVisualChildren<T>(DependencyObject depObj) where T : DependencyObject
    {
        if (depObj != null)
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(depObj, i);
                if (child != null && child is T)
                {
                    yield return (T)child;
                }

                foreach (T childOfChild in FindVisualChildren<T>(child))
                {
                    yield return childOfChild;
                }
            }
        }
    }
}