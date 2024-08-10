using GasMatic.Core.Interfaces;
using GasMatic.Core.ViewModels;

namespace GasMatic.Blazor.Hybrid.Services;

public class GasVolumeLocalDataSource : IGasVolumeDataSource
{
    public Task<List<GasVolumeViewModel>> FetchAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<GasVolumeViewModel?> FetchByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<GasVolumeViewModel> CreateAsync(GasVolumeViewModel viewModel)
    {
        throw new NotImplementedException();
    }

    public Task<bool> UpdateByIdAsync(int id, GasVolumeViewModel viewModel)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAllAsync()
    {
        throw new NotImplementedException();
    }
}