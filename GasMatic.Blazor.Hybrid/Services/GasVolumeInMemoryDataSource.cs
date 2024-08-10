using GasMatic.Core.Interfaces;
using GasMatic.Core.ViewModels;

namespace GasMatic.Blazor.Hybrid.Services;

public class GasVolumeInMemoryDataSource : IGasVolumeDataSource
{
    private readonly List<GasVolumeViewModel> _models = [];

    public Task<List<GasVolumeViewModel>> FetchAllAsync()
    {
        return Task.FromResult(_models);
    }

    public Task<GasVolumeViewModel?> FetchByIdAsync(int id)
    {
        return Task.FromResult(_models.Find(m => m.Id.Equals(id)));
    }

    public Task<GasVolumeViewModel> CreateAsync(GasVolumeViewModel viewModel)
    {
        viewModel.Id = _models.Count.Equals(0) ? 1 : _models.Max(m => m.Id + 1);
        _models.Add(viewModel);
        return Task.FromResult(viewModel);
    }

    public async Task<bool> UpdateByIdAsync(int id, GasVolumeViewModel viewModel)
    {
        var model = await FetchByIdAsync(id);

        if (model is null)
            return false;

        model.NominalPipeSize = viewModel.NominalPipeSize;
        model.Length = viewModel.Length;
        model.Pressure = viewModel.Pressure;
        model.GasVolume = viewModel.GasVolume;
        model.CalculatedDate = viewModel.CalculatedDate;

        return true;
    }

    public async Task<bool> DeleteByIdAsync(int id)
    {
        var model = await FetchByIdAsync(id);

        if (model is null)
            return false;

        _models.Remove(model);
        return true;
    }

    public Task DeleteAllAsync()
    {
        _models.Clear();
        return Task.CompletedTask;
    }
}