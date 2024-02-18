namespace GasMatic.Client.Core.Services.Domain;

public record GasVolumeRecord(
    int NominalPipeSize,
    double Length,
    double Pressure,
    double GasVolume,
    DateTime CalculatedAt,
    int Id = 0
);