using GasMatic.Blazor.Wasm.Data;
using GasMatic.Blazor.Wasm.ViewModels;
using SqliteWasmHelper;

namespace GasMatic.Blazor.Wasm.Services;

public class GasVolumeLocalDataSource : IGasVolumeDataSource
{
    private readonly ISqliteWasmDbContextFactory<GasMaticDbContext> _factory;

    public GasVolumeLocalDataSource(ISqliteWasmDbContextFactory<GasMaticDbContext> factory)
    {
        _factory = factory;
    }

    public async Task<List<GasVolumeViewModel>> FetchAllAsync()
    {
        await using var dbContext = await _factory.CreateDbContextAsync();
        return dbContext.GasVolumeEntities
            .Select(e => new GasVolumeViewModel
            {
                Id = e.Id,
                NominalPipeSize = e.NominalPipeSize,
                Length = e.Length,
                Pressure = e.Pressure,
                GasVolume = e.GasVolume,
                CalculatedAt = e.CalculatedAt,
            })
            .ToList();
    }

    public async Task<GasVolumeViewModel?> FetchByIdAsync(int id)
    {
        await using var dbContext = await _factory.CreateDbContextAsync();
        return dbContext.GasVolumeEntities
            .Where(e => e.Id == id)
            .Select(e => new GasVolumeViewModel
            {
                Id = e.Id,
                NominalPipeSize = e.NominalPipeSize,
                Length = e.Length,
                Pressure = e.Pressure,
                GasVolume = e.GasVolume,
                CalculatedAt = e.CalculatedAt,
            })
            .FirstOrDefault();
    }

    public async Task<GasVolumeViewModel> CreateAsync(GasVolumeViewModel viewModel)
    {
        await using var dbContext = await _factory.CreateDbContextAsync();
        var entity = new GasVolumeEntity
        {
            NominalPipeSize = viewModel.NominalPipeSize,
            Length = viewModel.Length,
            Pressure = viewModel.Pressure,
            GasVolume = viewModel.GasVolume,
            CalculatedAt = viewModel.CalculatedAt
        };
        dbContext.GasVolumeEntities.Add(entity);
        await dbContext.SaveChangesAsync();

        viewModel.Id = entity.Id;
        return viewModel;
    }

    public async Task<bool> UpdateByIdAsync(int id, GasVolumeViewModel viewModel)
    {
        await using var dbContext = await _factory.CreateDbContextAsync();
        var entity = dbContext.GasVolumeEntities.FirstOrDefault(e => e.Id == id);
        if (entity is null)
            return false;

        entity.NominalPipeSize = viewModel.NominalPipeSize;
        entity.Length = viewModel.Length;
        entity.Pressure = viewModel.Pressure;
        entity.GasVolume = viewModel.GasVolume;
        entity.CalculatedAt = viewModel.CalculatedAt;
        await dbContext.SaveChangesAsync();

        return true;
    }

    public async Task<bool> DeleteByIdAsync(int id)
    {
        await using var dbContext = await _factory.CreateDbContextAsync();
        var entity = dbContext.GasVolumeEntities.FirstOrDefault(e => e.Id == id);
        if (entity is null) return false;

        dbContext.GasVolumeEntities.Remove(entity);
        await dbContext.SaveChangesAsync();

        return true;
    }

    public async Task DeleteAllAsync()
    {
        await using var dbContext = await _factory.CreateDbContextAsync();
        dbContext.GasVolumeEntities.RemoveRange(dbContext.GasVolumeEntities);
        await dbContext.SaveChangesAsync();
    }
}