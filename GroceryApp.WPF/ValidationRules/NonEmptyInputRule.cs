using System.Globalization;
using System.Windows.Controls;

namespace GroceryApp.WPF.ValidationRules;

public class NonEmptyInputRule : ValidationRule
{
    public string? ElementName { get; set; }

    public override ValidationResult Validate(object value, CultureInfo cultureInfo)
    {
        string input = value as string;

        if (string.IsNullOrWhiteSpace(input))
        {
            return new ValidationResult(false, $"{ElementName} is required");
        }
        else
        {
            return new ValidationResult(true, null);
        }
    }
}
