using AngleSharp.Html.Dom;
using FluentAssertions;
using Moq;
using SqliteWasmHelper;
using GasMatic.Shared.Services;
using GasMatic.Blazor.Wasm.Components.Pages.GasVolume;
using GasMatic.Blazor.Wasm.Data;
using GasMatic.Blazor.Wasm.Services;
using GasMatic.Blazor.Wasm.ViewModels;

namespace GasMatic.Tests.Blazor.Wasm;

public class GasVolumeCalculatorTests
{
    [Fact]
    public void HeaderComponentRendersCorrectly()
    {
        // Arrange
        using var ctx = new TestContext();
        ctx.Services.AddSqliteWasmDbContextFactory<GasMaticDbContext>();
        ctx.Services.AddScoped<IGasVolumeDataSource, GasVolumeLocalDataSource>();
        ctx.Services.AddScoped<IGasVolumeService, GasVolumeService>();

        // Act
        var cut = ctx.RenderComponent<GasVolumeCalculator>();
        var h3Element = cut.Find("h3");

        // Assert
        h3Element.TextContent.Should().Be("Gas Volume Calculator");
    }

    [Fact]
    public void SubmitButtonShouldInitiallyBeDisabled()
    {
        // Arrange
        using var ctx = new TestContext();
        ctx.Services.AddSqliteWasmDbContextFactory<GasMaticDbContext>();
        ctx.Services.AddScoped<IGasVolumeDataSource, GasVolumeLocalDataSource>();
        ctx.Services.AddScoped<IGasVolumeService, GasVolumeService>();

        // Act
        var cut = ctx.RenderComponent<GasVolumeCalculator>();
        var submitButton = cut.Find("button[type=submit]");

        // Assert
        submitButton.GetAttribute("disabled").Should().NotBeNull();
    }

    [Fact]
    public void SubmitButtonShouldBeEnabledAfterProvidingValidInput()
    {
        // Arrange
        using var ctx = new TestContext();
        ctx.Services.AddSqliteWasmDbContextFactory<GasMaticDbContext>();
        ctx.Services.AddScoped<IGasVolumeDataSource, GasVolumeLocalDataSource>();
        ctx.Services.AddScoped<IGasVolumeService, GasVolumeService>();

        // Act
        var cut = ctx.RenderComponent<GasVolumeCalculator>();
        cut.Find("#length").Input(2400);
        var submitButton = cut.Find("button[type=submit]");

        // Assert
        submitButton.GetAttribute("disabled").Should().BeNull();
    }

    [Fact]
    public void PressureRadioGroupShouldBeVisibleWhenCustomPressureInputIsFalse()
    {
        // Arrange
        using var ctx = new TestContext();
        ctx.Services.AddSqliteWasmDbContextFactory<GasMaticDbContext>();
        ctx.Services.AddScoped<IGasVolumeDataSource, GasVolumeLocalDataSource>();
        ctx.Services.AddScoped<IGasVolumeService, GasVolumeService>();

        // Act
        var cut = ctx.RenderComponent<GasVolumeCalculator>();
        var pressureThirtyInputRadio = cut.Find("#Thirty");

        // Assert
        pressureThirtyInputRadio.GetAttribute("type").Should().Be("radio");
    }

    [Fact]
    public void PressureInputShouldBeVisibleWhenCustomPressureCheckboxIsTrue()
    {
        // Arrange
        using var ctx = new TestContext();
        ctx.Services.AddSqliteWasmDbContextFactory<GasMaticDbContext>();
        ctx.Services.AddScoped<IGasVolumeDataSource, GasVolumeLocalDataSource>();
        ctx.Services.AddScoped<IGasVolumeService, GasVolumeService>();

        // Act
        var cut = ctx.RenderComponent<GasVolumeCalculator>();
        cut.Find("#useCustomPressure").Change(true);
        var pressureInput = cut.Find("#pressure");

        // Assert
        pressureInput.GetAttribute("placeholder").Should().Be("Enter pressure in mbar");
    }

    [Fact]
    public void CanChangeNominalPipeSizeSelectInputOption()
    {
        // Arrange
        const string expectedOption = "ThreeHundred";
        using var ctx = new TestContext();
        ctx.Services.AddSqliteWasmDbContextFactory<GasMaticDbContext>();
        ctx.Services.AddScoped<IGasVolumeDataSource, GasVolumeLocalDataSource>();
        ctx.Services.AddScoped<IGasVolumeService, GasVolumeService>();

        // Act
        var cut = ctx.RenderComponent<GasVolumeCalculator>();
        var selectElement = (IHtmlSelectElement)cut.Find("#nominalPipeSize");
        selectElement.Change(expectedOption);

        // Assert
        selectElement.Value.Should().Be(expectedOption);
    }

    [Fact]
    public void CanChangeRadioGroupOption()
    {
        // Arrange
        using var ctx = new TestContext();
        ctx.Services.AddSqliteWasmDbContextFactory<GasMaticDbContext>();
        ctx.Services.AddScoped<IGasVolumeDataSource, GasVolumeLocalDataSource>();
        ctx.Services.AddScoped<IGasVolumeService, GasVolumeService>();

        // Act
        var cut = ctx.RenderComponent<GasVolumeCalculator>();
        var thirtyOption = (IHtmlInputElement)cut.Find("input[name=pressureOptions][value=Thirty]");
        var fourThousandOption = (IHtmlInputElement)cut.Find("input[name=pressureOptions][value=FourThousand]");
        thirtyOption.IsChecked = false;
        fourThousandOption.IsChecked = true;

        // Assert
        thirtyOption.IsChecked.Should().BeFalse();
        fourThousandOption.IsChecked.Should().BeTrue();
    }

    [Fact]
    public void GasVolumeResultShouldBeRenderedCorrectlyAfterSubmittingValidInput()
    {
        // Arrange
        using var ctx = new TestContext();
        ctx.Services.AddSqliteWasmDbContextFactory<GasMaticDbContext>();
        ctx.Services.AddScoped<IGasVolumeService, GasVolumeService>();

        var mockedDataSource = new Mock<IGasVolumeDataSource>();
        var viewModel = new GasVolumeViewModel();
        mockedDataSource
            .Setup(s => s.CreateAsync(viewModel))
            .ReturnsAsync(viewModel);
        ctx.Services.AddScoped<IGasVolumeDataSource>(_ => mockedDataSource.Object);

        // Act
        var cut = ctx.RenderComponent<GasVolumeCalculator>();
        cut.Find("#nominalPipeSize").Change("ThreeHundred");
        cut.Find("#length").Input(2400);
        cut.Find("#useCustomPressure").Change(true);
        cut.Find("#pressure").Input(4000);
        cut.Find("button[type=submit]").Click();
        var h4Element = cut.Find("h4");

        // Assert
        h4Element.TextContent.Should().Contain("Gas Volume: 839.522");
    }

    [Fact]
    public void TypingInvalidInputInLengthControlShouldRenderValidationMessage()
    {
        // Arrange
        using var ctx = new TestContext();
        ctx.Services.AddSqliteWasmDbContextFactory<GasMaticDbContext>();
        ctx.Services.AddScoped<IGasVolumeDataSource, GasVolumeLocalDataSource>();
        ctx.Services.AddScoped<IGasVolumeService, GasVolumeService>();

        // Act
        var cut = ctx.RenderComponent<GasVolumeCalculator>();
        cut.Find("#length").Input("a");
        var divValidationMessage = cut.Find(".text-danger");

        // Assert
        divValidationMessage.TextContent.Should().Be("That is not a valid number.");
    }

    [Fact]
    public void LeavingLengthControlEmptyAfterFirstTypingShouldRenderValidationMessage()
    {
        // Arrange
        using var ctx = new TestContext();
        ctx.Services.AddSqliteWasmDbContextFactory<GasMaticDbContext>();
        ctx.Services.AddScoped<IGasVolumeDataSource, GasVolumeLocalDataSource>();
        ctx.Services.AddScoped<IGasVolumeService, GasVolumeService>();

        // Act
        var cut = ctx.RenderComponent<GasVolumeCalculator>();
        cut.Find("#length").Input("a");
        cut.Find("#length").Input(string.Empty);
        var divValidationMessage = cut.Find(".text-danger");

        // Assert
        divValidationMessage.TextContent.Should().Be("The Length field is required.");
    }
}