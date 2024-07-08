using System.Collections.ObjectModel;
using System.Globalization;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using DevExpress.Maui.Controls;
using GasMatic.Maui.Core.Messages;
using GasMatic.Maui.Core.Services.Alerts;
using GasMatic.Maui.Core.Services.Database;
using GasMatic.Maui.Core.Services.Interactions;
using Localization;

namespace GasMatic.Maui.Core.ViewModels;

public partial class SettingsViewModel : ObservableObject
{
    private const string NominalPipeSizeUrl = "https://en.wikipedia.org/wiki/Nominal_Pipe_Size";

    private readonly IAppInteractionsService _appInteractionsService;
    private readonly IDatabaseService _databaseService;
    private readonly IAlertService _alertService;
    private readonly ILocalizationManager _localizationManager;
    private readonly ILocalizedResourcesProvider _resources;

    [ObservableProperty] private BottomSheetState _deleteDataSheetState = BottomSheetState.Hidden;
    [ObservableProperty] private BottomSheetState _changeLanguageSheetState = BottomSheetState.Hidden;
    [ObservableProperty] private double _deleteSliderValue;
    [ObservableProperty] private bool _isLoading;

    private static IEnumerable<string> SupportedLanguages =>
    [
        "en-SE",
        "sv-SE"
    ];

    [ObservableProperty] private string _currentLanguage;
    private ObservableCollection<Maui.Core.ViewModels.SelectionItemViewModel> _languageList = [];

    public ObservableCollection<Maui.Core.ViewModels.SelectionItemViewModel> LanguageList
    {
        get => _languageList;
        set
        {
            _languageList = value;
            OnPropertyChanged();
        }
    }

    public SettingsViewModel(IAppInteractionsService appInteractionsService,
        IDatabaseService databaseService,
        IAlertService alertService,
        ILocalizationManager localizationManager,
        ILocalizedResourcesProvider resources)
    {
        _appInteractionsService = appInteractionsService;
        _databaseService = databaseService;
        _alertService = alertService;
        _localizationManager = localizationManager;
        _resources = resources;

        _currentLanguage = CultureInfo.CurrentCulture.Name;
        LoadSupportedLanguages();
    }

    private void LoadSupportedLanguages()
    {
        LanguageList = new ObservableCollection<Maui.Core.ViewModels.SelectionItemViewModel>(SupportedLanguages
            .Select(language =>
                new Maui.Core.ViewModels.SelectionItemViewModel(language, string.Equals(language, CurrentLanguage)))
            .ToList());
    }

    private void SwitchLanguage(string newLanguage)
    {
        var currentChoice = LanguageList.First(p => p.Item == CurrentLanguage);
        currentChoice.IsSelected = false;

        CurrentLanguage = newLanguage;

        var newChoice = LanguageList.First(p => p.Item == CurrentLanguage);
        newChoice.IsSelected = true;

        var newCulture = new CultureInfo(newLanguage);
        _localizationManager.UpdateUserCulture(newCulture);
    }

    [RelayCommand]
    private async Task OpenNpsWebLink() => await _appInteractionsService.OpenBrowserAsync(NominalPipeSizeUrl);

    [RelayCommand]
    private void ShowChangeLanguageSheet()
    {
        ChangeLanguageSheetState = BottomSheetState.HalfExpanded;
    }

    [RelayCommand]
    private async Task ChangeLanguage(string newLanguage)
    {
        if (string.Equals(CurrentLanguage, newLanguage))
            return;

        var hasConfirmed = await _alertService.ShowConfirmationPromptAsync(
            _resources["ChangeLanguage"],
            _resources["ChangeLanguageDialogMessage"],
            _resources["AcceptButton"],
            _resources["CancelButton"]);

        if (hasConfirmed)
        {
            SwitchLanguage(newLanguage);
            await _alertService.ShowSnackbarAsync(_resources["ChangeLanguageSuccessMessage"]);
        }

        ChangeLanguageSheetState = BottomSheetState.Hidden;
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
        await _databaseService.DeleteAllAsync();
        WeakReferenceMessenger.Default.Send(new GasVolumeDataDeletedMessage());
        IsLoading = false;

        DeleteDataSheetState = BottomSheetState.Hidden;
        DeleteSliderValue = 0;

        await _alertService.ShowSnackbarAsync(_resources["DeleteAllDataSuccessMessage"]);
    }
}