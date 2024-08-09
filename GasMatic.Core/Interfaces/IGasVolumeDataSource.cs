using GasMatic.Core.ViewModels;

namespace GasMatic.Core.Interfaces;

public interface IGasVolumeDataSource
{
    Task<List<GasVolumeViewModel>> FetchAllAsync();
    Task<GasVolumeViewModel?> FetchByIdAsync(int id);
    Task<GasVolumeViewModel> CreateAsync(GasVolumeViewModel viewModel);
    Task<bool> UpdateByIdAsync(int id, GasVolumeViewModel viewModel);
    Task<bool> DeleteByIdAsync(int id);
    Task DeleteAllAsync();
}