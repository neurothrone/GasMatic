using GasMatic.Blazor.Wasm.Services;
using GasMatic.Blazor.Wasm.ViewModels;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.QuickGrid;

namespace GasMatic.Blazor.Wasm.Components.Pages.GasVolume;

public partial class GasVolumeHistory
{
    [Inject]
    private IGasVolumeDataSource GasVolumeDataSource { get; set; } = null!;

    private IQueryable<GasVolumeViewModel> _calculations = null!;
    private PaginationState _pagination = new() { ItemsPerPage = 10 };

    protected override async Task OnInitializedAsync()
    {
        await FetchCalculations();
    }

    private async Task FetchCalculations()
    {
        var calculations = await GasVolumeDataSource.FetchAllAsync();
        _calculations = calculations.AsQueryable();
    }

    private async Task DeleteCalculationAsync(int id)
    {
        if (await GasVolumeDataSource.DeleteByIdAsync(id))
        {
            await FetchCalculations();
        }
    }
}