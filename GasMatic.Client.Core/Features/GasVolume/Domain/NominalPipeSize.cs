namespace GasMatic.Client.Core.Features.GasVolume.Domain;

public enum NominalPipeSize
{
    Twenty = 20,
    TwentyFive = 25,
    ThirtyTwo = 32,
    Forty = 40,
    Fifty = 50,
    SixtyFive = 65,
    Eighty = 80,
    Ninety = 90,
    Hundred = 100,
    HundredFifteen = 115,
    HundredTwentyFive = 125,
    HundredFifty = 150,
    TwoHundred = 200,
    TwoHundredFifty = 250,
    ThreeHundred = 300,
    ThreeHundredFifty = 350,
    FourHundred = 400
}

public static class NominalPipeSizeExtensions
{
    private static readonly Dictionary<NominalPipeSize, string> PipeSizeLabels = new()
    {
        { NominalPipeSize.Twenty, $"\u00be - 20" }, // ¾
        { NominalPipeSize.TwentyFive, "1 - 25" },
        { NominalPipeSize.ThirtyTwo, "1 \u00bc - 32" }, // 1 ¼
        { NominalPipeSize.Forty, "1 \u00bd - 40" }, // ½
        { NominalPipeSize.Fifty, "2 - 50" },
        { NominalPipeSize.SixtyFive, "2 \u00bd - 65" }, // ½
        { NominalPipeSize.Eighty, "3 - 80" },
        { NominalPipeSize.Ninety, "3 \u00bd - 90" }, // ½
        { NominalPipeSize.Hundred, "4 - 100" },
        { NominalPipeSize.HundredFifteen, "4 \u00bd - 115" }, // ½
        { NominalPipeSize.HundredTwentyFive, "5 - 125" },
        { NominalPipeSize.HundredFifty, "6 - 150" },
        { NominalPipeSize.TwoHundred, "8 - 200" },
        { NominalPipeSize.TwoHundredFifty, "10 - 250" },
        { NominalPipeSize.ThreeHundred, "12 - 300" },
        { NominalPipeSize.ThreeHundredFifty, "14 - 350" },
        { NominalPipeSize.FourHundred, "16 - 400" },
    };


    private static readonly NominalPipeSize[] AllEnumValues =
    [
        ..(NominalPipeSize[])Enum.GetValues(typeof(NominalPipeSize))
    ];

    private static readonly string[] AllLabels = PipeSizeLabels.Values.ToArray();

    public static string[] ToStringArray() => AllLabels;

    public static NominalPipeSize FromString(string label)
    {
        var index = Array.IndexOf(AllLabels, label);
        if (index != -1)
            return AllEnumValues[index];

        throw new Exception("Label not found");
    }

    public static string Label(NominalPipeSize nominalPipeSize) => PipeSizeLabels.GetValueOrDefault(
        nominalPipeSize, "Unknown"
    );
}