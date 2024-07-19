using GasMatic.Blazor.Wasm.Data;
using GasMatic.Blazor.Wasm.ViewModels;

namespace GasMatic.Blazor.Wasm.Mappers;

public static class GasVolumeEntityMapper
{
    public static GasVolumeEntity ToEntity(this GasVolumeViewModel viewModel)
    {
        return new GasVolumeEntity
        {
            NominalPipeSize = viewModel.NominalPipeSize,
            Length = viewModel.Length,
            Pressure = viewModel.Pressure,
            GasVolume = viewModel.GasVolume,
            CalculatedAt = viewModel.CalculatedDate
        };
    }

    public static void UpdateFromViewModel(this GasVolumeEntity entity, GasVolumeViewModel viewModel)
    {
        entity.NominalPipeSize = viewModel.NominalPipeSize;
        entity.Length = viewModel.Length;
        entity.Pressure = viewModel.Pressure;
        entity.GasVolume = viewModel.GasVolume;
        entity.CalculatedAt = viewModel.CalculatedDate;
    }
}