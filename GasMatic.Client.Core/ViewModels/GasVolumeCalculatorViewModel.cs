using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using DevExpress.Maui.Controls;
using GasMatic.Client.Core.Features.GasVolume;
using GasMatic.Client.Core.Features.GasVolume.Database;
using GasMatic.Client.Core.Features.GasVolume.Domain;
using GasMatic.Client.Core.Messages;
using GasMatic.Client.Core.Validation;

namespace GasMatic.Client.Core.ViewModels;

public partial class GasVolumeCalculatorViewModel : ObservableValidator
{
    private readonly IGasVolumeDatabase _gasVolumeDatabase;
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

    private bool _isCustomPressure = false;

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

    [ObservableProperty] private string _selectedNominalPipeSize;
    [ObservableProperty] private string _selectedPressure;

    [ObservableProperty] private BottomSheetState _npsSheetState = BottomSheetState.Hidden;
    [ObservableProperty] private BottomSheetState _pressureSheetState = BottomSheetState.Hidden;


    public string[] NominalPipeSizeChoices => NominalPipeSizeExtensions.ToStringArray();
    public string[] PressureChoices => PressureExtensions.ToStringList();

    public AsyncRelayCommand SubmitCommand { get; }

    public GasVolumeCalculatorViewModel(
        IGasVolumeDatabase gasVolumeDatabase,
        IGasVolumeService gasVolumeService)
    {
        _gasVolumeDatabase = gasVolumeDatabase;
        _gasVolumeService = gasVolumeService;

        SelectedNominalPipeSize = NominalPipeSizeChoices.First();
        SelectedPressure = PressureChoices.First();

        SubmitCommand = new AsyncRelayCommand(OnSubmit, () => !HasErrors);
        ResetInputFields();
        ErrorsChanged += GasVolumeCalculatorViewModel_ErrorsChanged;
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

        // ValidateAllProperties();
        // if (HasErrors)
        // {
        //     var errors = GetErrors();
        //     Debug.WriteLine(string.Join("\n", errors.Select(e => e.ErrorMessage)));
        // }
        // else
        // {
        //     Debug.WriteLine("All OK");
        // }
        //
        // return Task.CompletedTask;
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
        var savedRecord = await _gasVolumeDatabase.SaveGasVolumeRecord(
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

    [RelayCommand]
    private void ShowNominalPipeSizeSelectionSheet()
    {
        NpsSheetState = BottomSheetState.FullExpanded;
    }

    [RelayCommand]
    private void SelectNominalPipeSize(string selection)
    {
        NpsSheetState = BottomSheetState.Hidden;
        SelectedNominalPipeSize = selection;
    }

    [RelayCommand]
    private void ShowPressureSelectionSheet()
    {
        PressureSheetState = BottomSheetState.HalfExpanded;
    }

    [RelayCommand]
    private void SelectPressure(string selection)
    {
        PressureSheetState = BottomSheetState.Hidden;
        SelectedPressure = selection;
    }
}