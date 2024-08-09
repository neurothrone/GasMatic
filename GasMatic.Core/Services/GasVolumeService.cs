using GasMatic.Core.Interfaces;

namespace GasMatic.Core.Services;

public class GasVolumeService : IGasVolumeService
{
    private const double GasPressure = 1013.0;

    private static double CalculatePipeInnerRadius(int nominalPipeSize) => (nominalPipeSize / 1000.0) / 2.0;

    public double CalculateGasVolume(int nominalPipeSize, double length, double pressure)
    {
        var radius = CalculatePipeInnerRadius(nominalPipeSize);
        return (Math.PI * Math.Pow(radius, 2)) * length * ((pressure + GasPressure) / GasPressure);
    }
}