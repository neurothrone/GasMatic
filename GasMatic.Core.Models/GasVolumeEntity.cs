using SQLite;

namespace GasMatic.Core.Models;

public class GasVolumeEntity
{
    // This is required for IndexedDB (Wasm)
    [System.ComponentModel.DataAnnotations.Key]
    // This is required for SQLite (Maui)
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }

    public int NominalPipeSize { get; set; }
    public double Length { get; set; }
    public double Pressure { get; set; }
    public double GasVolume { get; set; }

    [Indexed]
    public DateTime CalculatedDate { get; set; }
}