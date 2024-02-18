namespace GasMatic.Client.Core.Messages;

public record CalculationCompletedMessage(Services.Domain.GasVolumeRecord Record);

public record GasVolumeDataDeletedMessage();