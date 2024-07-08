using GasMatic.Blazor.Wasm.Models;
using GasMatic.Blazor.Wasm.Services;
using GasMatic.Blazor.Wasm.ViewModels;
using GasMatic.Shared.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace GasMatic.Blazor.Wasm.Components.Pages.GasVolume;

public partial class GasVolumeCalculator
{
    [Inject]
    private IGasVolumeDataSource GasVolumeDataSource { get; set; } = null!;

    [Inject]
    private IGasVolumeService GasVolumeService { get; set; } = null!;

    private ErrorBoundary? _errorBoundary;

    private const int RoundToDecimals = 3;
    private GasVolumeInput _input = new();
    private bool _useCustomPressure;
    private bool _isFormValid;
    private double _gasVolume;

    private void ValidateInput()
    {
        if (!double.TryParse(_input.Length, out _))
        {
            _isFormValid = false;
            return;
        }

        if (_useCustomPressure && !double.TryParse(_input.CustomPressure, out _))
        {
            _isFormValid = false;
            return;
        }

        _isFormValid = true;
    }

    private async Task CalculateGasVolume()
    {
        // throw new SystemException();

        if (!double.TryParse(_input.Length, out double length))
        {
            return;
        }

        var pressureString = _useCustomPressure ? _input.CustomPressure : ((int)_input.SelectedPressure).ToString();
        if (!double.TryParse(pressureString, out double pressure))
        {
            return;
        }

        var gasVolume = GasVolumeService.CalculateGasVolume(
            (int)_input.NominalPipeSize,
            length,
            pressure);
        var roundedGasVolume = Math.Round(gasVolume, RoundToDecimals);
        _gasVolume = roundedGasVolume;

        var viewModel = new GasVolumeViewModel
        {
            NominalPipeSize = (int)_input.NominalPipeSize,
            Length = length,
            Pressure = pressure,
            GasVolume = _gasVolume,
            CalculatedAt = DateTime.Now
        };
        await GasVolumeDataSource.CreateAsync(viewModel);
    }

    private void Recover()
    {
        _errorBoundary?.Recover();
    }
}