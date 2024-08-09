using AngleSharp.Html.Dom;
using FluentAssertions;
using Moq;
using SqliteWasmHelper;
using GasMatic.Blazor.Wasm.Components.Pages.GasVolume;
using GasMatic.Blazor.Wasm.Data;
using GasMatic.Blazor.Wasm.Services;
using GasMatic.Core.Interfaces;
using GasMatic.Core.Services;
using GasMatic.Core.ViewModels;

namespace GasMatic.Tests.Blazor.Wasm;

public class GasVolumeCalculatorTests : TestContext
{
    public GasVolumeCalculatorTests()
    {
        // Arrange
        Services.AddSqliteWasmDbContextFactory<GasMaticDbContext>();
        Services.AddScoped<IGasVolumeDataSource, GasVolumeLocalDataSource>();
        Services.AddScoped<IGasVolumeService, GasVolumeService>();
        Services.AddLocalization();
    }

    [Fact]
    public void SubmitButtonShouldInitiallyBeDisabled()
    {
        // Act
        var cut = RenderComponent<GasVolumeCalculator>(
            parameters => parameters
                .Add(p => p.GasVolumeInputViewModel, new GasVolumeInputViewModel())
        );
        var submitButton = cut.Find("button[type=submit]");

        // Assert
        submitButton.GetAttribute("disabled").Should().NotBeNull();
    }

    [Fact]
    public void SubmitButtonShouldBeEnabledAfterProvidingValidInput()
    {
        // Act
        var cut = RenderComponent<GasVolumeCalculator>(
            parameters => parameters
                .Add(p => p.GasVolumeInputViewModel, new GasVolumeInputViewModel())
        );
        cut.Find("#length").Input(2400);
        var submitButton = cut.Find("button[type=submit]");

        // Assert
        submitButton.GetAttribute("disabled").Should().BeNull();
    }

    [Fact]
    public void PressureRadioGroupShouldBeVisibleWhenCustomPressureInputIsFalse()
    {
        // Act
        var cut = RenderComponent<GasVolumeCalculator>(
            parameters => parameters
                .Add(p => p.GasVolumeInputViewModel, new GasVolumeInputViewModel())
        );
        var pressureThirtyInputRadio = cut.Find("#Thirty");

        // Assert
        pressureThirtyInputRadio.GetAttribute("type").Should().Be("radio");
    }

    [Fact]
    public void PressureInputShouldBeVisibleWhenCustomPressureCheckboxIsTrue()
    {
        // Act
        var cut = RenderComponent<GasVolumeCalculator>(
            parameters => parameters
                .Add(p => p.GasVolumeInputViewModel, new GasVolumeInputViewModel())
        );
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

        // Act
        var cut = RenderComponent<GasVolumeCalculator>(
            parameters => parameters
                .Add(p => p.GasVolumeInputViewModel, new GasVolumeInputViewModel())
        );
        var selectElement = (IHtmlSelectElement)cut.Find("#nominalPipeSize");
        selectElement.Change(expectedOption);

        // Assert
        selectElement.Value.Should().Be(expectedOption);
    }

    [Fact]
    public void CanChangeRadioGroupOption()
    {
        // Act
        var cut = RenderComponent<GasVolumeCalculator>(
            parameters => parameters
                .Add(p => p.GasVolumeInputViewModel, new GasVolumeInputViewModel())
        );
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
        var mockedDataSource = new Mock<IGasVolumeDataSource>();
        var viewModel = new GasVolumeViewModel();
        mockedDataSource
            .Setup(s => s.CreateAsync(viewModel))
            .ReturnsAsync(viewModel);
        Services.AddScoped<IGasVolumeDataSource>(_ => mockedDataSource.Object);

        // Act
        var cut = RenderComponent<GasVolumeCalculator>(
            parameters => parameters
                .Add(p => p.GasVolumeInputViewModel, new GasVolumeInputViewModel())
        );
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
        // Act
        var cut = RenderComponent<GasVolumeCalculator>(
            parameters => parameters
                .Add(p => p.GasVolumeInputViewModel, new GasVolumeInputViewModel())
        );
        cut.Find("#length").Input("a");
        var divValidationMessage = cut.Find(".text-danger");

        // Assert
        divValidationMessage.TextContent.Should().Be("That is not a valid number.");
    }

    [Fact]
    public void LeavingLengthControlEmptyAfterFirstTypingShouldRenderValidationMessage()
    {
        // Act
        var cut = RenderComponent<GasVolumeCalculator>(
            parameters => parameters
                .Add(p => p.GasVolumeInputViewModel, new GasVolumeInputViewModel())
        );
        cut.Find("#length").Input("a");
        cut.Find("#length").Input(string.Empty);
        var divValidationMessage = cut.Find(".text-danger");

        // Assert
        divValidationMessage.TextContent.Should().Be("The Length field is required.");
    }
}