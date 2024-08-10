using GasMatic.Core.Models;
using GasMatic.Maui.Sqlite.Data;
using GasMatic.Maui.Sqlite.Interfaces;
using SQLite;

namespace GasMatic.Maui.Sqlite.Repositories;

public class DatabaseRepository : IDatabaseRepository
{
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private SQLiteAsyncConnection _database;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    private async Task Connect()
    {
        // ReSharper disable once ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract
        if (_database is not null)
            return;

        _database = new SQLiteAsyncConnection(
            DatabaseConstants.DatabasePath,
            DatabaseConstants.Flags
        );
        await _database.CreateTableAsync<GasVolumeEntity>();
    }

    public async Task<GasVolumeEntity> CreateAsync(GasVolumeEntity entity)
    {
        await Connect();
        await _database.InsertAsync(entity);
        return entity;
    }

    public async Task<IReadOnlyCollection<GasVolumeEntity>> FetchAllAsync()
    {
        await Connect();
        return await _database
            .Table<GasVolumeEntity>()
            .OrderBy(e => e.CalculatedDate)
            .ToArrayAsync();
    }

    public async Task<GasVolumeEntity?> FetchByIdAsync(int id)
    {
        await Connect();
        return await _database
            .Table<GasVolumeEntity>()
            .FirstOrDefaultAsync();
    }

    public async Task<bool> UpdateAsync(GasVolumeEntity entity)
    {
        await Connect();
        return await _database.UpdateAsync(entity) > 0;
    }

    public async Task<bool> DeleteByIdAsync(int id)
    {
        await Connect();
        return await _database.DeleteAsync<GasVolumeEntity>(id) > 0;
    }

    public async Task DeleteAllAsync()
    {
        await Connect();
        await _database.DeleteAllAsync<GasVolumeEntity>();
    }
}