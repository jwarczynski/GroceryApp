using System.Globalization;
using System.Windows.Controls;

namespace GroceryApp.WPF.ValidationRules;

public class GreaterThanZeroRule : ValidationRule
{
    public string? ElementName { get; set; }

    public override ValidationResult Validate(object value, CultureInfo cultureInfo)
    {
        double number = 0;

        try
        {
            if (((string)value).Length > 0)
                number = Double.Parse((String)value);
            else
            {
                return new ValidationResult(false, $"{ElementName} is required");
            }
        }
        catch (Exception e)
        {
            return new ValidationResult(false, $"{ElementName} should be a number");
        }

        if (number < 0)
        {
            return new ValidationResult(false, $"{ElementName} should be greater than 0");
        }
        else
        {
            return new ValidationResult(true, null);
        }
    }
}
