using GasMatic.Maui.Core.Extensions;
using GasMatic.Maui.Shared.Domain;
using GasMatic.Maui.Shared.Dto;

namespace GasMatic.Maui.Core.ViewModels;

public class GasVolumeItemViewModel(GasVolumeRecord record)
{
    public int Id => record.Id;
    public string NominalPipeSize => NominalPipeSizeExtensions.Label((NominalPipeSize)record.NominalPipeSize);
    public double Length => record.Length;
    public double Pressure => record.Pressure;
    public double GasVolume => record.GasVolume;
    public DateTime CalculatedDate => record.CalculatedDate;


    // Format: Feb 10, 2024
    public string CalculatedAtFormatted => CalculatedDate.ToString("MMM d, yyyy");
}