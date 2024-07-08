namespace GasMatic.Blazor.Wasm.ViewModels;

public class GasVolumeViewModel
{
    public int Id { get; set; }
    public int NominalPipeSize { get; set; }
    public double Length { get; set; }
    public double Pressure { get; set; }
    public double GasVolume { get; set; }
    public DateTime CalculatedAt { get; set; }
}