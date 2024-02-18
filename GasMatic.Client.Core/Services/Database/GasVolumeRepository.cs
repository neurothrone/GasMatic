using GasMatic.Client.Core.Services.Domain;
using SQLite;

namespace GasMatic.Client.Core.Services.Database;

public class GasVolumeRepository : IGasVolumeRepository
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
        await _database.CreateTableAsync<GasVolumeCalculation>();
    }

    public async Task<GasVolumeRecord> SaveGasVolumeRecord(GasVolumeRecord record)
    {
        await Connect();

        // TODO: extension methods to transform GasVolumeCalculation to Record and vice versa?
        var calculation = new GasVolumeCalculation
        {
            NominalPipeSize = record.NominalPipeSize,
            Length = record.Length,
            Pressure = record.Pressure,
            GasVolume = record.GasVolume,
            CalculatedAt = record.CalculatedAt
        };

        await _database.InsertAsync(calculation);

        return new GasVolumeRecord(
            calculation.NominalPipeSize,
            calculation.Length,
            calculation.Pressure,
            calculation.GasVolume,
            calculation.CalculatedAt,
            calculation.Id
        );
    }

    public async Task<List<GasVolumeRecord>> FetchGasVolumeCalculationsAsync()
    {
        await Connect();

        var calculations = await _database.Table<GasVolumeCalculation>().ToListAsync() ?? [];
        return calculations.Select(c =>
            new GasVolumeRecord(c.NominalPipeSize, c.Length, c.Pressure, c.GasVolume, c.CalculatedAt, c.Id)
        ).ToList();
    }

    public async Task DeleteGasVolumeCalculationById(int id)
    {
        await Connect();
        await _database.DeleteAsync<GasVolumeCalculation>(id);
    }

    public async Task DeleteAllAsync()
    {
        await Connect();
        await _database.DeleteAllAsync<GasVolumeCalculation>();
    }
}