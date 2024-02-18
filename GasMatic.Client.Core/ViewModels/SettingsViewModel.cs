using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using DevExpress.Maui.Controls;
using GasMatic.Client.Core.Features.GasVolume.Database;
using GasMatic.Client.Core.Messages;
using GasMatic.Client.Core.Services;

namespace GasMatic.Client.Core.ViewModels;

public partial class SettingsViewModel : ObservableObject
{
    private readonly IAppInteractionsService _appInteractionsService;
    private readonly IGasVolumeDatabase _gasVolumeDatabase;

    private const string NominalPipeSizeUrl = "https://en.wikipedia.org/wiki/Nominal_Pipe_Size";

    [ObservableProperty] private BottomSheetState _deleteDataSheetState = BottomSheetState.Hidden;
    [ObservableProperty] private double _deleteSliderValue;

    public SettingsViewModel(
        IAppInteractionsService appInteractionsService,
        IGasVolumeDatabase gasVolumeDatabase)
    {
        _appInteractionsService = appInteractionsService;
        _gasVolumeDatabase = gasVolumeDatabase;
    }

    [RelayCommand]
    private async Task OpenNpsWebLink() => await _appInteractionsService.OpenBrowserAsync(NominalPipeSizeUrl);

    [RelayCommand]
    private void ShowDeleteDataSheet()
    {
        DeleteDataSheetState = BottomSheetState.HalfExpanded;
    }

    [RelayCommand]
    private async Task DeleteAllData()
    {
        // TODO: use loading?
        // await delete
        await _gasVolumeDatabase.DeleteAllAsync();

        DeleteDataSheetState = BottomSheetState.Hidden;
        DeleteSliderValue = 0;

        // TODO: Send Message to Gas History VM that all data locally has been deleted.
        WeakReferenceMessenger.Default.Send(new GasVolumeDataDeletedMessage());
    }
}