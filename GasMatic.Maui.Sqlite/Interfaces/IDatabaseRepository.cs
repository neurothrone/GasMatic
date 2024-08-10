using GasMatic.Core.Models;

namespace GasMatic.Maui.Sqlite.Interfaces;

public interface IDatabaseRepository
{
    Task<GasVolumeEntity> CreateAsync(GasVolumeEntity entity);
    Task<IReadOnlyCollection<GasVolumeEntity>> FetchAllAsync();
    Task<GasVolumeEntity?> FetchByIdAsync(int id);
    Task<bool> UpdateAsync(GasVolumeEntity entity);
    Task<bool> DeleteByIdAsync(int id);
    Task DeleteAllAsync();
}