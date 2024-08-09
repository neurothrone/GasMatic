using GasMatic.Blazor.Wasm.Data;
using GasMatic.Core.ViewModels;

namespace GasMatic.Blazor.Wasm.Mappers;

public static class GasVolumeViewModelMapper
{
    public static GasVolumeViewModel ToViewModel(this GasVolumeEntity entity)
    {
        return new GasVolumeViewModel
        {
            Id = entity.Id,
            NominalPipeSize = entity.NominalPipeSize,
            Length = entity.Length,
            Pressure = entity.Pressure,
            GasVolume = entity.GasVolume,
            CalculatedDate = entity.CalculatedAt,
        };
    }
}