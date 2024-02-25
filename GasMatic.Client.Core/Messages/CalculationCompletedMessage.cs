using GasMatic.Shared.Dto;

namespace GasMatic.Client.Core.Messages;

public record CalculationCompletedMessage(GasVolumeRecord Record);