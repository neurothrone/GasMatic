using GasMatic.Core.Interfaces;
using GasMatic.Core.ViewModels;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace GasMatic.Components.Pages.GasVolume;

public partial class GasVolumeCalculator
{
    [Inject]
    private IGasVolumeDataSource GasVolumeDataSource { get; set; } = null!;

    [Inject]
    private IGasVolumeService GasVolumeService { get; set; } = null!;

    [Parameter]
    public GasVolumeInputViewModel InputViewModel { get; set; } = null!;

    private ErrorBoundary? _errorBoundary;

    private const int RoundToDecimals = 3;

    private bool _useCustomPressure;
    private double _gasVolume;

    private void ValidateInput()
    {
        if (!double.TryParse(InputViewModel.Length, out _))
        {
            InputViewModel.IsValid = false;
            return;
        }

        if (_useCustomPressure && !double.TryParse(InputViewModel.CustomPressure, out _))
        {
            InputViewModel.IsValid = false;
            return;
        }

        InputViewModel.IsValid = true;
    }

    private async Task CalculateGasVolume()
    {
        if (!double.TryParse(InputViewModel.Length, out double length))
            return;

        var pressureString = _useCustomPressure
            ? InputViewModel.CustomPressure
            : ((int)InputViewModel.SelectedPressure).ToString();
        if (!double.TryParse(pressureString, out double pressure))
            return;

        var gasVolume = GasVolumeService.CalculateGasVolume(
            (int)InputViewModel.NominalPipeSize,
            length,
            pressure);
        var roundedGasVolume = Math.Round(gasVolume, RoundToDecimals);
        _gasVolume = roundedGasVolume;

        var viewModel = new GasVolumeViewModel
        {
            NominalPipeSize = (int)InputViewModel.NominalPipeSize,
            Length = length,
            Pressure = pressure,
            GasVolume = _gasVolume,
            CalculatedDate = DateTime.Now
        };
        await GasVolumeDataSource.CreateAsync(viewModel);
    }

    private void Recover()
    {
        _errorBoundary?.Recover();
    }
}