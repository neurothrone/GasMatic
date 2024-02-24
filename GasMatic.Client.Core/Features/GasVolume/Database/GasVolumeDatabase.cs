using GasMatic.Client.Core.Features.GasVolume.Domain;
using SQLite;

namespace GasMatic.Client.Core.Features.GasVolume.Database;

public class GasVolumeDatabase : IGasVolumeDatabase
{
    private SQLiteAsyncConnection _database;

    private async Task Connect()
    {
        if (_database is not null)
            return;

        _database = new SQLiteAsyncConnection(
            DatabaseConstants.DatabasePath,
            DatabaseConstants.Flags
        );
        await _database.CreateTableAsync<GasVolumeEntity>();
    }

    public async Task<GasVolumeRecord> SaveGasVolumeRecord(GasVolumeRecord record)
    {
        await Connect();

        // TODO: extension methods to transform GasVolumeCalculation to Record and vice versa?
        var calculation = new GasVolumeEntity
        {
            NominalPipeSize = record.NominalPipeSize,
            Length = record.Length,
            Pressure = record.Pressure,
            GasVolume = record.GasVolume,
            CalculatedDate = record.CalculatedDate
        };

        await _database.InsertAsync(calculation);

        return record with { Id = calculation.Id };
    }

    public async Task<List<GasVolumeRecord>> FetchGasVolumeCalculationsAsync()
    {
        await Connect();

        var calculations = await _database.Table<GasVolumeEntity>().ToListAsync() ?? [];
        return calculations.Select(c =>
            new GasVolumeRecord(c.NominalPipeSize, c.Length, c.Pressure, c.GasVolume, c.CalculatedDate, c.Id)
        ).ToList();
    }

    public async Task DeleteGasVolumeCalculationById(int id)
    {
        await Connect();
        await _database.DeleteAsync<GasVolumeEntity>(id);
    }

    public async Task DeleteAllAsync()
    {
        await Connect();
        await _database.DeleteAllAsync<GasVolumeEntity>();
    }
}