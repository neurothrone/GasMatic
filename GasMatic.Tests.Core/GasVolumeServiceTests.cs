using FluentAssertions;
using GasMatic.Core.Interfaces;
using GasMatic.Core.Services;

namespace GasMatic.Tests.Core;

public class GasVolumeServiceTests
{
    [Fact]
    public void ServiceReturnsValidResultOnCalculate()
    {
        // Arrange
        const double expectedGasVolume = 839.52163327943151;
        IGasVolumeService sut = new GasVolumeService();

        // Act
        var gasVolume = sut.CalculateGasVolume(300, 2400.0, 4000.0);

        // Assert
        gasVolume.Should().Be(expectedGasVolume);
    }

    [Fact]
    public void ServiceReturnsZeroIfAllInputIsZero()
    {
        // Arrange
        IGasVolumeService sut = new GasVolumeService();

        // Act
        var gasVolume = sut.CalculateGasVolume(0, 0, 0);

        // Assert
        gasVolume.Should().Be(0);
    }

    [Fact]
    public void ServiceReturnsZeroIfLengthAndPressureInputIsZero()
    {
        // Arrange
        IGasVolumeService sut = new GasVolumeService();

        // Act
        var gasVolume = sut.CalculateGasVolume(300, 0, 0);

        // Assert
        gasVolume.Should().Be(0);
    }

    [Fact]
    public void ServiceReturnsNonZeroIfPressureInputIsZero()
    {
        // Arrange
        IGasVolumeService sut = new GasVolumeService();

        // Act
        var gasVolume = sut.CalculateGasVolume(300, 2400, 0);

        // Assert
        gasVolume.Should().NotBe(0);
    }

    [Fact]
    public void ServiceReturnsZeroIfLengthInputIsZero()
    {
        // Arrange
        IGasVolumeService sut = new GasVolumeService();

        // Act
        var gasVolume = sut.CalculateGasVolume(300, 0, 4000);

        // Assert
        gasVolume.Should().Be(0);
    }

    [Fact]
    public void ServiceReturnsZeroIfNominalPipeSizeInputIsZero()
    {
        // Arrange
        IGasVolumeService sut = new GasVolumeService();

        // Act
        var gasVolume = sut.CalculateGasVolume(0, 2400, 4000);

        // Assert
        gasVolume.Should().Be(0);
    }

    [Fact]
    public void ServiceShouldNotThrowExceptionIfUsingMaxValues()
    {
        // Arrange
        IGasVolumeService sut = new GasVolumeService();

        // Act
        Action act = () => sut.CalculateGasVolume(
            int.MaxValue,
            double.MaxValue,
            double.MaxValue
        );

        // Assert
        act.Should().NotThrow();
    }
}