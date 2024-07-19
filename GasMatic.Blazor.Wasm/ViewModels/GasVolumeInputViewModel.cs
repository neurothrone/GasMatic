using System.ComponentModel.DataAnnotations;
using GasMatic.Shared.Domain;

namespace GasMatic.Blazor.Wasm.ViewModels;

public class GasVolumeInputViewModel
{
    private const string DoubleValueRegex = @"^\d+(\.\d+)?$";

    public NominalPipeSize NominalPipeSize { get; set; } = NominalPipeSize.Twenty;
    public Pressure SelectedPressure { get; set; } = Pressure.Thirty;

    [Required]
    [RegularExpression(DoubleValueRegex, ErrorMessage = "That is not a valid number.")]
    public string Length { get; set; } = string.Empty;

    [Required(ErrorMessage = "The Custom Pressure field is required.")]
    [RegularExpression(DoubleValueRegex, ErrorMessage = "That is not a valid number.")]
    public string CustomPressure { get; set; } = string.Empty;

    public bool IsValid { get; set; }
}