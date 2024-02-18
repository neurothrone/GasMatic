using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using GasMatic.Client.Core.Features.GasVolume.Database;
using GasMatic.Client.Core.Features.GasVolume.Domain;
using GasMatic.Client.Core.Messages;

namespace GasMatic.Client.Core.ViewModels;

public partial class GasVolumeHistoryViewModel : ObservableObject
{
    private readonly IGasVolumeDatabase _gasVolumeDatabase = new GasVolumeDatabase();

    private ObservableCollection<GasVolumeViewModel> _items;

    public ObservableCollection<GasVolumeViewModel> Items
    {
        get => _items;
        set => SetProperty(ref _items, value);
    }

    [ObservableProperty] private bool _isLoading;

    public GasVolumeHistoryViewModel()
    {
        _items = [];
        LoadRecordsFromDatabase();
        SubscribeToMessenger();
    }

    private async void LoadRecordsFromDatabase()
    {
        IsLoading = true;

        var calculations = await _gasVolumeDatabase.FetchGasVolumeCalculationsAsync();
        var viewModels = calculations
            .Select(record => new GasVolumeViewModel(record))
            .OrderByDescending(record => record.CalculatedAt)
            .ToList();
        Items = new ObservableCollection<GasVolumeViewModel>(viewModels);

        IsLoading = false;
    }

    private void SubscribeToMessenger()
    {
        WeakReferenceMessenger.Default.Register<CalculationCompletedMessage>(
            this,
            (_, message) => InsertRecord(message.Record)
        );
        WeakReferenceMessenger.Default.Register<GasVolumeDataDeletedMessage>(
            this,
            (_, _) => Items.Clear()
        );
    }

    private void InsertRecord(GasVolumeRecord record)
    {
        Items.Insert(0, new GasVolumeViewModel(record));
    }

    [RelayCommand]
    private async Task Delete(GasVolumeViewModel vm)
    {
        await _gasVolumeDatabase.DeleteGasVolumeCalculationById(vm.Id);
        Items.Remove(vm);
    }
}