using GasMatic.Shared.Domain;

namespace GasMatic.Client.Core.Features.GasVolume;

public class GasVolumeService : IGasVolumeService
{
    private const double GasPressure = 1013.0;

    private static double CalculatePipeInnerRadius(NominalPipeSize nominalPipeSize) =>
        ((int)nominalPipeSize / 1000.0) / 2.0;

    public double CalculateGasVolume(
        NominalPipeSize nominalPipeSize,
        double length,
        double pressure)
    {
        var radius = CalculatePipeInnerRadius(nominalPipeSize);
        return (Math.PI * Math.Pow(radius, 2)) * length * ((pressure + GasPressure) / GasPressure);
    }
}