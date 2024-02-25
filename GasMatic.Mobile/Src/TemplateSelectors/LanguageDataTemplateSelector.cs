using GasMatic.Client.Core.ViewModels;

namespace GasMatic.Mobile.TemplateSelectors;

public class LanguageDataTemplateSelector : DataTemplateSelector
{
    public DataTemplate SelectedLanguageTemplate { get; set; }
    public DataTemplate UnselectedLanguageTemplate { get; set; }

    protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
    {
        return item is LanguageItemViewModel { IsSelected: true }
            ? SelectedLanguageTemplate
            : UnselectedLanguageTemplate;
    }
}