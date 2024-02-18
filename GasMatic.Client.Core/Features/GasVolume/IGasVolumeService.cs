using GasMatic.Client.Core.Features.GasVolume.Domain;

namespace GasMatic.Client.Core.Features.GasVolume;

public interface IGasVolumeService
{
    double CalculateGasVolume(
        NominalPipeSize nominalPipeSize,
        double length,
        double pressure
    );
}