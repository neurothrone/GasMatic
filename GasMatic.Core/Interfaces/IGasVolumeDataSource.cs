using GasMatic.Core.ViewModels;

namespace GasMatic.Core.Interfaces;

public interface IGasVolumeDataSource
{
    Task<GasVolumeViewModel> CreateAsync(GasVolumeViewModel viewModel);
    Task<List<GasVolumeViewModel>> FetchAllAsync();
    Task<GasVolumeViewModel?> FetchByIdAsync(int id);
    Task<bool> UpdateByIdAsync(int id, GasVolumeViewModel viewModel);
    Task<bool> DeleteByIdAsync(int id);
    Task DeleteAllAsync();
}