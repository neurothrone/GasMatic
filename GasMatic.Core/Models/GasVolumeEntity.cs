namespace GasMatic.Core.Models;

public class GasVolumeEntity
{
    // This is required for IndexedDB (Wasm)
    [System.ComponentModel.DataAnnotations.Key]
    public int Id { get; set; }

    public int NominalPipeSize { get; set; }
    public double Length { get; set; }
    public double Pressure { get; set; }
    public double GasVolume { get; set; }
    public DateTime CalculatedAt { get; set; }
}