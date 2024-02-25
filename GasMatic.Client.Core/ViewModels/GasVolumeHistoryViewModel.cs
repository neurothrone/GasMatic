using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using GasMatic.Client.Core.Features.GasVolume.Database;
using GasMatic.Client.Core.Messages;
using GasMatic.Shared.Dto;

namespace GasMatic.Client.Core.ViewModels;

public partial class GasVolumeHistoryViewModel : ObservableObject,
    IRecipient<CalculationCompletedMessage>,
    IRecipient<GasVolumeDataDeletedMessage>
{
    private readonly IGasVolumeDatabase _gasVolumeDatabase;

    private ObservableCollection<GasVolumeItemViewModel> _items;

    public ObservableCollection<GasVolumeItemViewModel> Items
    {
        get => _items;
        set => SetProperty(ref _items, value);
    }

    [ObservableProperty] private bool _isLoading;

    public GasVolumeHistoryViewModel(IGasVolumeDatabase gasVolumeDatabase)
    {
        _gasVolumeDatabase = gasVolumeDatabase;

        _items = [];
        SubscribeToMessages();
        LoadRecordsFromDatabase();
    }

    private void SubscribeToMessages()
    {
        WeakReferenceMessenger.Default.Register<GasVolumeDataDeletedMessage>(this);
        WeakReferenceMessenger.Default.Register<CalculationCompletedMessage>(this);
    }

    void IRecipient<CalculationCompletedMessage>.Receive(CalculationCompletedMessage message)
    {
        InsertRecord(message.Record);
    }

    void IRecipient<GasVolumeDataDeletedMessage>.Receive(GasVolumeDataDeletedMessage _) => Items.Clear();

    private void InsertRecord(GasVolumeRecord record)
    {
        Items.Insert(0, new GasVolumeItemViewModel(record));
    }

    private async void LoadRecordsFromDatabase()
    {
        IsLoading = true;

        var calculations = await _gasVolumeDatabase.FetchGasVolumeCalculationsAsync();
        var viewModels = calculations
            .Select(record => new GasVolumeItemViewModel(record))
            .OrderByDescending(record => record.CalculatedDate)
            .ToList();
        Items = new ObservableCollection<GasVolumeItemViewModel>(viewModels);

        IsLoading = false;
    }

    [RelayCommand]
    private async Task Delete(GasVolumeItemViewModel vm)
    {
        await _gasVolumeDatabase.DeleteGasVolumeCalculationById(vm.Id);
        Items.Remove(vm);
    }
}