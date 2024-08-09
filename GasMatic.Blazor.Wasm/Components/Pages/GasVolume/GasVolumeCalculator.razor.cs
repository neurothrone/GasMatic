using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using GasMatic.Core.Interfaces;
using GasMatic.Core.ViewModels;

namespace GasMatic.Blazor.Wasm.Components.Pages.GasVolume;

public partial class GasVolumeCalculator
{
    [Inject]
    private IGasVolumeDataSource GasVolumeDataSource { get; set; } = null!;

    [Inject]
    private IGasVolumeService GasVolumeService { get; set; } = null!;

    [Parameter]
    public GasVolumeInputViewModel GasVolumeInputViewModel { get; set; } = null!;

    private ErrorBoundary? _errorBoundary;

    private const int RoundToDecimals = 3;

    private bool _useCustomPressure;
    private double _gasVolume;

    private void ValidateInput()
    {
        if (!double.TryParse(GasVolumeInputViewModel.Length, out _))
        {
            GasVolumeInputViewModel.IsValid = false;
            return;
        }

        if (_useCustomPressure && !double.TryParse(GasVolumeInputViewModel.CustomPressure, out _))
        {
            GasVolumeInputViewModel.IsValid = false;
            return;
        }

        GasVolumeInputViewModel.IsValid = true;
    }

    private async Task CalculateGasVolume()
    {
        // throw new SystemException();

        if (!double.TryParse(GasVolumeInputViewModel.Length, out double length))
        {
            return;
        }

        var pressureString = _useCustomPressure
            ? GasVolumeInputViewModel.CustomPressure
            : ((int)GasVolumeInputViewModel.SelectedPressure).ToString();
        if (!double.TryParse(pressureString, out double pressure))
        {
            return;
        }

        var gasVolume = GasVolumeService.CalculateGasVolume(
            (int)GasVolumeInputViewModel.NominalPipeSize,
            length,
            pressure);
        var roundedGasVolume = Math.Round(gasVolume, RoundToDecimals);
        _gasVolume = roundedGasVolume;

        var viewModel = new GasVolumeViewModel
        {
            NominalPipeSize = (int)GasVolumeInputViewModel.NominalPipeSize,
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