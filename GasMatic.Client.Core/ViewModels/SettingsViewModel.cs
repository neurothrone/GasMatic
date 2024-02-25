using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using DevExpress.Maui.Controls;
using GasMatic.Client.Core.Features.GasVolume.Database;
using GasMatic.Client.Core.Messages;
using GasMatic.Client.Core.Services.Alerts;
using GasMatic.Client.Core.Services.Interactions;

namespace GasMatic.Client.Core.ViewModels;

public partial class SettingsViewModel(
    IAppInteractionsService appInteractionsService,
    IGasVolumeDatabase gasVolumeDatabase,
    IAlertService alertService) : ObservableObject
{
    private const string NominalPipeSizeUrl = "https://en.wikipedia.org/wiki/Nominal_Pipe_Size";

    [ObservableProperty] private BottomSheetState _deleteDataSheetState = BottomSheetState.Hidden;
    [ObservableProperty] private double _deleteSliderValue;
    [ObservableProperty] private bool _isLoading;

    [RelayCommand]
    private async Task OpenNpsWebLink() => await appInteractionsService.OpenBrowserAsync(NominalPipeSizeUrl);

    [RelayCommand]
    private void ShowDeleteDataSheet()
    {
        DeleteDataSheetState = BottomSheetState.HalfExpanded;
    }

    [RelayCommand]
    private async Task DeleteAllData()
    {
        IsLoading = true;
        await gasVolumeDatabase.DeleteAllAsync();

        DeleteDataSheetState = BottomSheetState.Hidden;
        DeleteSliderValue = 0;
        IsLoading = false;

        WeakReferenceMessenger.Default.Send(new GasVolumeDataDeletedMessage());
        await alertService.ShowSnackbarAsync("Your data has successfully been deleted.");
    }
}