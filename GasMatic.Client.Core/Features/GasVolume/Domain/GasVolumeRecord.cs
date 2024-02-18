namespace GasMatic.Client.Core.Features.GasVolume.Domain;

public record GasVolumeRecord(
    int NominalPipeSize,
    double Length,
    double Pressure,
    double GasVolume,
    DateTime CalculatedDate,
    int Id = 0
);