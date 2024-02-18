namespace GasMatic.Client.Core.Services.Domain;

public enum Pressure
{
    Thirty = 30,
    Hundred = 100,
    FourThousand = 4000
}

public static class PressureExtensions
{
    private static readonly Pressure[] AllEnumValues =
    [
        ..(Pressure[])Enum.GetValues(typeof(Pressure))
    ];

    public static string[] ToStringList() => AllEnumValues
        .Select(p => ((int)p).ToString())
        .ToArray();
}