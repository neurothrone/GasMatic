using GasMatic.Core.Interfaces;
using GasMatic.Core.Mappers;
using GasMatic.Core.ViewModels;
using GasMatic.Maui.Sqlite.Interfaces;

namespace GasMatic.Maui.Sqlite.Services;

public class GasVolumeDataSource : IGasVolumeDataSource
{
    private readonly IDatabaseRepository _databaseRepository;

    public GasVolumeDataSource(IDatabaseRepository databaseRepository)
    {
        _databaseRepository = databaseRepository;
    }

    public async Task<GasVolumeViewModel> CreateAsync(GasVolumeViewModel viewModel)
    {
        var entity = await _databaseRepository.CreateAsync(viewModel.ToEntity());
        return entity.ToViewModel();
    }

    public async Task<List<GasVolumeViewModel>> FetchAllAsync()
    {
        var entities = await _databaseRepository.FetchAllAsync();
        return entities
            .Select(e => e.ToViewModel())
            .ToList();
    }

    public async Task<GasVolumeViewModel?> FetchByIdAsync(int id)
    {
        var entity = await _databaseRepository.FetchByIdAsync(id);
        return entity?.ToViewModel();
    }

    public async Task<bool> UpdateByIdAsync(int id, GasVolumeViewModel viewModel)
    {
        var entity = await _databaseRepository.FetchByIdAsync(id);
        if (entity is null)
            return false;

        entity.UpdateFromViewModel(viewModel);
        return await _databaseRepository.UpdateAsync(entity);
    }

    public async Task<bool> DeleteByIdAsync(int id)
    {
        return await _databaseRepository.DeleteByIdAsync(id);
    }

    public async Task DeleteAllAsync()
    {
        await _databaseRepository.DeleteAllAsync();
    }
}