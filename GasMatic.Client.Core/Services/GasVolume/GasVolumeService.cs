using GasMatic.Client.Core.Services.Domain;

namespace GasMatic.Client.Core.Services.GasVolume;

public class GasVolumeService : IGasVolumeService
{
    private readonly double _gasPressure = 1013.0;

    private static double CalculatePipeInnerRadius(NominalPipeSize nominalPipeSize)
    {
        return ((int)nominalPipeSize / 1000.0) / 2.0;
    }

    public double CalculateGasVolume(
        NominalPipeSize nominalPipeSize,
        double length,
        double pressure
    )
    {
        double radius = CalculatePipeInnerRadius(nominalPipeSize);
        return (Math.PI * Math.Pow(radius, 2)) * length * ((pressure + _gasPressure) / _gasPressure);
    }
}