using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using GasMatic.Client.Core.ViewModels;

namespace GasMatic.Client.Core.Validation;

public class ValidCustomPressureAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        var instance = (GasVolumeCalculatorViewModel)validationContext.ObjectInstance;
        if (!instance.IsCustomPressure)
        {
            return ValidationResult.Success; // If IsCustomPressure is set to false, skip validation.
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