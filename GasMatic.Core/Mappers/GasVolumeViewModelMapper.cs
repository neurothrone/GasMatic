using GasMatic.Core.Models;
using GasMatic.Core.ViewModels;

namespace GasMatic.Core.Mappers;

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
            CalculatedDate = entity.CalculatedDate,
        };
    }
}