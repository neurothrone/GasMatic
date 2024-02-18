using GasMatic.Client.Core.Features.GasVolume.Domain;

namespace GasMatic.Client.Core.Features.GasVolume.Database;

public interface IGasVolumeDatabase
{
    Task<GasVolumeRecord> SaveGasVolumeRecord(GasVolumeRecord record);
    Task<List<GasVolumeRecord>> FetchGasVolumeCalculationsAsync();
    Task DeleteGasVolumeCalculationById(int id);
    Task DeleteAllAsync();
}