using GasMatic.Maui.Shared.Dto;

namespace GasMatic.Maui.Core.Services.Database;

public interface IDatabaseService
{
    Task<GasVolumeRecord> SaveGasVolumeRecord(GasVolumeRecord record);
    Task<List<GasVolumeRecord>> FetchGasVolumeCalculationsAsync();
    Task DeleteGasVolumeCalculationById(int id);
    Task DeleteAllAsync();
}