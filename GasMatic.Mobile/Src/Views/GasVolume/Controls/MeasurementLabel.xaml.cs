namespace GasMatic.Mobile.Views.GasVolume.Controls;

public partial class MeasurementLabel
{
    private static readonly BindableProperty FirstTextProperty = BindableProperty.Create(
        nameof(MeasurementType),
        typeof(string),
        typeof(MeasurementLabel),
        string.Empty
    );

    private static readonly BindableProperty SecondTextProperty = BindableProperty.Create(
        nameof(Unit),
        typeof(string),
        typeof(MeasurementLabel),
        string.Empty
    );

    public string MeasurementType
    {
        get => (string)GetValue(FirstTextProperty);
        set => SetValue(FirstTextProperty, value);
    }

    public string Unit
    {
        get => (string)GetValue(SecondTextProperty);
        set => SetValue(SecondTextProperty, value);
    }

    public MeasurementLabel()
    {
        InitializeComponent();
    }
}