namespace GasMatic.Shared.Dto;

public record GasVolumeRecord(
    int NominalPipeSize,
    double Length,
    double Pressure,
    double GasVolume,
    DateTime CalculatedDate,
    int Id = -1
);