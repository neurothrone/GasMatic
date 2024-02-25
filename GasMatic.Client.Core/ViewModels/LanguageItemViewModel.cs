namespace GasMatic.Client.Core.ViewModels;

public class LanguageItemViewModel(string language, bool isSelected)
{
    public string Language { get; init; } = language;
    public bool IsSelected { get; init; } = isSelected;
}