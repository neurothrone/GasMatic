using GasMatic.Blazor.Wasm.IndexedDb.Data;
using GasMatic.Core.Interfaces;
using GasMatic.Core.Mappers;
using GasMatic.Core.ViewModels;
using IndexedDB.Blazor;

namespace GasMatic.Blazor.Wasm.IndexedDb.Services;

public class GasVolumeIndexedDbDataSource : IGasVolumeDataSource
{
    private readonly IIndexedDbFactory _dbFactory;

    public GasVolumeIndexedDbDataSource(IIndexedDbFactory dbFactory)
    {
        _dbFactory = dbFactory;
        _dbFactory.Create<GasVolumeDb>();
    }

    public async Task<GasVolumeViewModel> CreateAsync(GasVolumeViewModel viewModel)
    {
        using var db = await _dbFactory.Create<GasVolumeDb>();
        var entity = viewModel.ToEntity();
        db.GasVolumeEntities.Add(entity);
        await db.SaveChanges();

        viewModel.Id = entity.Id;
        return viewModel;
    }

    public async Task<List<GasVolumeViewModel>> FetchAllAsync()
    {
        using var db = await _dbFactory.Create<GasVolumeDb>();
        return db.GasVolumeEntities
            .Select(e => e.ToViewModel())
            .ToList();
    }

    public async Task<GasVolumeViewModel?> FetchByIdAsync(int id)
    {
        using var db = await _dbFactory.Create<GasVolumeDb>();
        return db.GasVolumeEntities
            .Where(e => e.Id.Equals(id))
            .Select(e => e.ToViewModel())
            .FirstOrDefault();
    }

    public async Task<bool> UpdateByIdAsync(int id, GasVolumeViewModel viewModel)
    {
        using var db = await _dbFactory.Create<GasVolumeDb>();
        var entity = db.GasVolumeEntities
            .FirstOrDefault(e => e.Id.Equals(id));
        if (entity is null)
            return false;

        entity.UpdateFromViewModel(viewModel);
        await db.SaveChanges();
        return true;
    }

    public async Task<bool> DeleteByIdAsync(int id)
    {
        using var db = await _dbFactory.Create<GasVolumeDb>();
        var entity = db.GasVolumeEntities.FirstOrDefault();
        if (entity is null)
            return false;

        var wasRemoved = db.GasVolumeEntities.Remove(entity);
        await db.SaveChanges();
        return wasRemoved;
    }

    public async Task DeleteAllAsync()
    {
        using var db = await _dbFactory.Create<GasVolumeDb>();
        db.GasVolumeEntities.Clear();
        await db.SaveChanges();
    }
}