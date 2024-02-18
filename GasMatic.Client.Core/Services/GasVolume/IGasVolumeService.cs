using GasMatic.Client.Core.Services.Domain;

namespace GasMatic.Client.Core.Services.GasVolume;

public interface IGasVolumeService
{
    double CalculateGasVolume(
        NominalPipeSize nominalPipeSize,
        double length,
        double pressure
    );
}