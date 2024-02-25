using System.Globalization;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using DevExpress.Maui.Controls;
using GasMatic.Client.Core.Features.GasVolume.Database;
using GasMatic.Client.Core.Messages;
using GasMatic.Client.Core.Services.Alerts;
using GasMatic.Client.Core.Services.Interactions;
using Localization;

namespace GasMatic.Client.Core.ViewModels;

public partial class SettingsViewModel(
    IAppInteractionsService appInteractionsService,
    IGasVolumeDatabase gasVolumeDatabase,
    IAlertService alertService,
    ILocalizationManager localizationManager,
    ILocalizedResourcesProvider resources)
    : ObservableObject
{
    private const string NominalPipeSizeUrl = "https://en.wikipedia.org/wiki/Nominal_Pipe_Size";

    [ObservableProperty] private BottomSheetState _deleteDataSheetState = BottomSheetState.Hidden;
    [ObservableProperty] private BottomSheetState _changeLanguageSheetState = BottomSheetState.Hidden;
    [ObservableProperty] private double _deleteSliderValue;
    [ObservableProperty] private bool _isLoading;

    public string[] SupportedLanguages =>
    [
        "en-SE",
        "sv-SE"
    ];

    [ObservableProperty] private string _currentLanguage = CultureInfo.CurrentCulture.Name;

    [RelayCommand]
    private async Task OpenNpsWebLink() => await appInteractionsService.OpenBrowserAsync(NominalPipeSizeUrl);

    [RelayCommand]
    private void ShowChangeLanguageSheet()
    {
        ChangeLanguageSheetState = BottomSheetState.HalfExpanded;
    }

    [RelayCommand]
    private async Task ChangeLanguage(string newLanguage)
    {
        var hasConfirmed = await alertService.ShowConfirmationPromptAsync(
            resources["ChangeLanguageDialogTitle"],
            resources["ChangeLanguageDialogMessage"],
            resources["AcceptButton"],
            resources["CancelButton"]);

        if (hasConfirmed)
        {
            SwitchLanguage(newLanguage);
            await alertService.ShowSnackbarAsync(resources["ChangeLanguageSuccessMessage"]);
        }

        ChangeLanguageSheetState = BottomSheetState.Hidden;
    }

    private void SwitchLanguage(string newLanguage)
    {
        CurrentLanguage = newLanguage;

        var newCulture = new CultureInfo(newLanguage);
        localizationManager.UpdateUserCulture(newCulture);
    }

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
        await alertService.ShowSnackbarAsync(resources["DeleteAllDataSuccessMessage"]);
    }
}