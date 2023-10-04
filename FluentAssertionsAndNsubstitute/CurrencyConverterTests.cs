using FluentAssertions;
using NSubstitute;

namespace FluentAssertionsAndNSubstitute;

public class CurrencyConverterTests
{
    private readonly IExchangeRateService _exchangeRateServiceMock;
    private readonly CurrencyConverter _sut;
    private readonly ILogger _logger;

    public CurrencyConverterTests()
    {
        _exchangeRateServiceMock = Substitute.For<IExchangeRateService>();
        _logger = Substitute.For<ILogger>();

        _sut = new CurrencyConverter(_exchangeRateServiceMock, _logger);
    }

    [Theory]
    [InlineData(100f, 650f)]
    [InlineData(0, 0)]
    [InlineData(-50f, -325f)]
    public void should_convert_amount_correctly(float amount, float expected)
    {
        var rate = 6.5f;

        _exchangeRateServiceMock
            .GetEuroToFrancsRate()
            .Returns(rate);

        // Exercise
        var result = _sut.ConvertEuroToFrancs(amount);

        // Verify
        Assert.Equal(expected, result);

        result
            .Should()
            .Be(expected);

        _logger
            .Received(1)
            .LogInformation("Conversion {Amount}*{Rate}={Converted}", amount, rate, expected);
    }

    [Fact]
    public void wrong_rate_should_throw_exception()
    {
        // Setup
        _exchangeRateServiceMock
            .GetEuroToFrancsRate()
            .Returns(0);

        // Exercise
        var result = () => _sut.ConvertEuroToFrancs(0);

        // Verify
        result
            .Should()
            .ThrowExactly<Exception>("0 is an invalid rate")
            .WithMessage("Rate is wrong");

        _logger
            .Received(0)
            .LogInformation(Arg.Any<string>(), Arg.Any<decimal>(), Arg.Any<decimal>(), Arg.Any<decimal>());
    }
}