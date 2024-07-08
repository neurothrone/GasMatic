using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using GasVolumeCalculatorViewModel = GasMatic.Maui.Core.ViewModels.GasVolumeCalculatorViewModel;

namespace GasMatic.Maui.Core.Validation;

public class ValidCustomPressureAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        var instance = (GasVolumeCalculatorViewModel)validationContext.ObjectInstance;
        if (!instance.IsCustomPressure)
        {
            return ValidationResult.Success;
        }

        if (value is string valueAsString)
        {
            var match = new Regex(GasVolumeCalculatorViewModel.DoubleValueRegex).Match(valueAsString);

            if (match.Success)
            {
                return ValidationResult.Success;
            }
        }

        return new ValidationResult("Invalid custom pressure input!");
    }
}