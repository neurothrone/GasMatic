namespace GasMatic.Core.Interfaces;

public interface IGasVolumeService
{
    double CalculateGasVolume(
        int nominalPipeSize,
        double length,
        double pressure
    );
}