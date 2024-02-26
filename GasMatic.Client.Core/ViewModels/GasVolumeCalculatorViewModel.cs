using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using DevExpress.Maui.Controls;
using GasMatic.Client.Core.Features.GasVolume;
using GasMatic.Client.Core.Messages;
using GasMatic.Client.Core.Services.Database;
using GasMatic.Client.Core.Validation;
using GasMatic.Shared.Domain;
using GasMatic.Shared.Dto;

namespace GasMatic.Client.Core.ViewModels;

public partial class GasVolumeCalculatorViewModel : ObservableValidator, IDisposable
{
    private readonly IDatabaseService _databaseService;
    private readonly IGasVolumeService _gasVolumeService;

    private const int RoundToDecimals = 3;
    public const string DoubleValueRegex = @"^\d+(\.\d+)?$";
    // public const string DoubleValueRegexNegativesAllowed = @"^-?\d+(\.\d+)?$";

    private string _length = string.Empty;

    [Required]
    [RegularExpression(DoubleValueRegex)]
    public string Length
    {
        get => _length;
        set => SetProperty(ref _length, value, true);
    }

    private bool _isCustomPressure;

    public bool IsCustomPressure
    {
        get => _isCustomPressure;
        set
        {
            SetProperty(ref _isCustomPressure, value);
            ValidateAllProperties();
        }
    }

    private string _customPressure = string.Empty;

    [ValidCustomPressure]
    public string CustomPressure
    {
        get => _customPressure;
        set => SetProperty(ref _customPressure, value, IsCustomPressure);
    }

    [ObservableProperty] private double _gasVolume;
    [ObservableProperty] private bool _isFormValid;

    [ObservableProperty] private string _selectedNominalPipeSize = string.Empty;
    [ObservableProperty] private string _selectedPressure = string.Empty;

    [ObservableProperty] private BottomSheetState _npsSheetState = BottomSheetState.Hidden;
    [ObservableProperty] private BottomSheetState _pressureSheetState = BottomSheetState.Hidden;


    private ObservableCollection<SelectionItemViewModel> _nominalPipeSizeList = [];

    public ObservableCollection<SelectionItemViewModel> NominalPipeSizeList
    {
        get => _nominalPipeSizeList;
        set
        {
            _nominalPipeSizeList = value;
            OnPropertyChanged();
        }
    }

    private ObservableCollection<SelectionItemViewModel> _pressureList = [];

    public ObservableCollection<SelectionItemViewModel> PressureList
    {
        get => _pressureList;
        set
        {
            _pressureList = value;
            OnPropertyChanged();
        }
    }

    public AsyncRelayCommand SubmitCommand { get; }

    public GasVolumeCalculatorViewModel(
        IDatabaseService databaseService,
        IGasVolumeService gasVolumeService)
    {
        _databaseService = databaseService;
        _gasVolumeService = gasVolumeService;

        SubmitCommand = new AsyncRelayCommand(OnSubmit, () => !HasErrors);
        ResetInputFields();
        ErrorsChanged += GasVolumeCalculatorViewModel_ErrorsChanged;

        LoadSheetLists();
    }

    void IDisposable.Dispose()
    {
        ErrorsChanged -= GasVolumeCalculatorViewModel_ErrorsChanged;
    }

    private void ResetInputFields()
    {
        Length = string.Empty;
        IsCustomPressure = false;
        CustomPressure = string.Empty;
    }

    private void GasVolumeCalculatorViewModel_ErrorsChanged(object? sender, DataErrorsChangedEventArgs e)
    {
        SubmitCommand.NotifyCanExecuteChanged();
    }

    private async Task OnSubmit()
    {
        await CalculateGasVolume();
    }

    private async Task CalculateGasVolume()
    {
        var nominalPipeSize = NominalPipeSizeExtensions.FromString(SelectedNominalPipeSize);

        if (!double.TryParse(Length, out double length))
        {
            // Console.WriteLine("❌ -> Invalid Length.");
            return;
        }

        var pressureString = IsCustomPressure ? CustomPressure : SelectedPressure;
        if (!double.TryParse(pressureString, out double pressure))
        {
            // Console.WriteLine("❌ -> Invalid Pressure.");
            return;
        }

        var gasVolume = _gasVolumeService.CalculateGasVolume(
            nominalPipeSize,
            length,
            pressure
        );
        var roundedGasVolume = Math.Round(gasVolume, RoundToDecimals);
        GasVolume = roundedGasVolume;

        await SaveCalculation(nominalPipeSize, length, pressure, roundedGasVolume);
    }

    private async Task SaveCalculation(
        NominalPipeSize nominalPipeSize,
        double length,
        double pressure,
        double gasVolume
    )
    {
        var savedRecord = await _databaseService.SaveGasVolumeRecord(
            new GasVolumeRecord(
                (int)nominalPipeSize,
                length,
                pressure,
                gasVolume,
                DateTime.Now
            )
        );

        WeakReferenceMessenger.Default.Send(new CalculationCompletedMessage(savedRecord));
    }

    private void LoadSheetLists()
    {
        var npsChoices = NominalPipeSizeExtensions.ToStringArray();
        var pressureChoices = PressureExtensions.ToStringArray();
        SelectedNominalPipeSize = npsChoices.First();
        SelectedPressure = pressureChoices.First();

        NominalPipeSizeList = new ObservableCollection<SelectionItemViewModel>(npsChoices
            .Select(c => new SelectionItemViewModel(c, string.Equals(c, SelectedNominalPipeSize)))
            .ToList());

        PressureList = new ObservableCollection<SelectionItemViewModel>(pressureChoices
            .Select(c => new SelectionItemViewModel(c, string.Equals(c, SelectedPressure)))
            .ToList());
    }

    [RelayCommand]
    private void ShowNominalPipeSizeSelectionSheet()
    {
        NpsSheetState = BottomSheetState.FullExpanded;
    }

    [RelayCommand]
    private void SelectNominalPipeSize(string selection)
    {
        var currentChoice = NominalPipeSizeList.First(p => p.Item == SelectedNominalPipeSize);
        currentChoice.IsSelected = false;

        SelectedNominalPipeSize = selection;

        var newChoice = NominalPipeSizeList.First(p => p.Item == SelectedNominalPipeSize);
        newChoice.IsSelected = true;

        NpsSheetState = BottomSheetState.Hidden;
    }

    [RelayCommand]
    private void ShowPressureSelectionSheet()
    {
        PressureSheetState = BottomSheetState.HalfExpanded;
    }

    [RelayCommand]
    private void SelectPressure(string selection)
    {
        // Set IsSelected of old choice to false
        var currentChoice = PressureList.First(p => p.Item == SelectedPressure);
        currentChoice.IsSelected = false;

        SelectedPressure = selection;

        // Set IsSelected of new choice to true
        var newChoice = PressureList.First(p => p.Item == SelectedPressure);
        newChoice.IsSelected = true;

        PressureSheetState = BottomSheetState.Hidden;
    }
}