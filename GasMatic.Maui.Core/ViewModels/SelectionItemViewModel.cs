using CommunityToolkit.Mvvm.ComponentModel;

namespace GasMatic.Maui.Core.ViewModels;

public partial class SelectionItemViewModel(string item, bool isSelected) : ObservableObject
{
    public string Item { get; init; } = item;

    [ObservableProperty] private bool _isSelected = isSelected;
}