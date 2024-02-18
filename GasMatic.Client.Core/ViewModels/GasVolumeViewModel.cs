using GasMatic.Client.Core.Services.Domain;

namespace GasMatic.Client.Core.ViewModels;

public class GasVolumeViewModel(GasVolumeRecord record)
{
    public int Id => record.Id;
    public string NominalPipeSize => NominalPipeSizeExtensions.Label((NominalPipeSize)record.NominalPipeSize);
    public double Length => record.Length;
    public double Pressure => record.Pressure;
    public double GasVolume => record.GasVolume;
    public DateTime CalculatedAt => record.CalculatedAt;


    // Format: Feb 10, 2024
    public string CalculatedAtFormatted => CalculatedAt.ToString("MMM d, yyyy");
}