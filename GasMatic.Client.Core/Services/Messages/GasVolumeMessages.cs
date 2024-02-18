namespace GasMatic.Client.Core.Services.Messages;

public record CalculationCompletedMessage(Domain.GasVolumeRecord Record);

public record GasVolumeDataDeletedMessage();