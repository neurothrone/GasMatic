using GasMatic.Client.Core.Services.Domain;

namespace GasMatic.Client.Core.Services.Database;

public interface IGasVolumeRepository
{
    Task<GasVolumeRecord> SaveGasVolumeRecord(GasVolumeRecord record);
    Task<List<GasVolumeRecord>> FetchGasVolumeCalculationsAsync();
    Task DeleteGasVolumeCalculationById(int id);
    Task DeleteAllAsync();
}