using SQLite;

namespace GasMatic.Client.Core.Services.Database.Entities;

[Table(nameof(GasVolumeEntity))]
public class GasVolumeEntity
{
    [PrimaryKey, AutoIncrement]
    [Column(nameof(Id))]
    public int Id { get; init; }

    [Column(nameof(NominalPipeSize))] public int NominalPipeSize { get; init; }

    [Column(nameof(Length))] public double Length { get; init; }
    [Column(nameof(Pressure))] public double Pressure { get; init; }
    [Column(nameof(GasVolume))] public double GasVolume { get; init; }

    [Indexed]
    [Column(nameof(CalculatedDate))]
    public DateTime CalculatedDate { get; init; }
}