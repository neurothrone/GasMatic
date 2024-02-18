using GasMatic.Client.Core.Features.GasVolume.Domain;

namespace GasMatic.Client.Core.Messages;

public record CalculationCompletedMessage(GasVolumeRecord Record);

public record GasVolumeDataDeletedMessage();