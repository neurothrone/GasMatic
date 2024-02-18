using SQLite;

namespace GasMatic.Client.Core.Services.Database;

[Table("GasVolumeCalculations")]
public class GasVolumeCalculation
{
    [PrimaryKey, AutoIncrement]
    [Column("id")]
    public int Id { get; init; }

    [Column("nominal_pipe_size")] public int NominalPipeSize { get; init; }

    [Column("length")] public double Length { get; init; }
    [Column("pressure")] public double Pressure { get; init; }
    [Column("gas_volume")] public double GasVolume { get; init; }
    [Indexed] [Column("calculated_at")] public DateTime CalculatedAt { get; init; }
}