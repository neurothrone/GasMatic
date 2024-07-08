namespace GasMatic.Shared.Services;

public interface IGasVolumeService
{
    double CalculateGasVolume(
        int nominalPipeSize,
        double length,
        double pressure
    );
}