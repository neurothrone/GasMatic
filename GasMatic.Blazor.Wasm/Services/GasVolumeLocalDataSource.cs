using GasMatic.Blazor.Wasm.Data;
using GasMatic.Blazor.Wasm.Mappers;
using GasMatic.Core.Interfaces;
using GasMatic.Core.ViewModels;
using Microsoft.EntityFrameworkCore;
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
        return await dbContext.GasVolumeEntities
            .Select(e => e.ToViewModel())
            .ToListAsync();
    }

    public async Task<GasVolumeViewModel?> FetchByIdAsync(int id)
    {
        await using var dbContext = await _factory.CreateDbContextAsync();
        return await dbContext.GasVolumeEntities
            .Where(e => e.Id == id)
            .Select(e => e.ToViewModel())
            .FirstOrDefaultAsync();
    }

    public async Task<GasVolumeViewModel> CreateAsync(GasVolumeViewModel viewModel)
    {
        await using var dbContext = await _factory.CreateDbContextAsync();
        var entity = viewModel.ToEntity();
        await dbContext.GasVolumeEntities.AddAsync(entity);
        await dbContext.SaveChangesAsync();

        viewModel.Id = entity.Id;
        return viewModel;
    }

    public async Task<bool> UpdateByIdAsync(int id, GasVolumeViewModel viewModel)
    {
        await using var dbContext = await _factory.CreateDbContextAsync();
        var entity = await dbContext.GasVolumeEntities.FirstOrDefaultAsync(e => e.Id == id);
        if (entity is null)
            return false;

        entity.UpdateFromViewModel(viewModel);
        await dbContext.SaveChangesAsync();

        return true;
    }

    public async Task<bool> DeleteByIdAsync(int id)
    {
        await using var dbContext = await _factory.CreateDbContextAsync();
        var entity = await dbContext.GasVolumeEntities.FirstOrDefaultAsync(e => e.Id == id);
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