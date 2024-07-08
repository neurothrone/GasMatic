using GasMatic.Maui.Shared.Domain;

namespace GasMatic.Maui.Core.Extensions;

public static class PressureExtensions
{
    private static readonly Pressure[] AllEnumValues =
    [
        ..(Pressure[])Enum.GetValues(typeof(Pressure))
    ];

    public static string[] ToStringArray() => AllEnumValues
        .Select(p => ((int)p).ToString())
        .ToArray();
}