namespace FluentAssertionsAndNSubstitute;

public class CurrencyConverter
{
    private readonly IExchangeRateService _exchangeRateService;
    private readonly ILogger _logger;

    public CurrencyConverter(IExchangeRateService exchangeRateService, ILogger logger)
    {
        _exchangeRateService = exchangeRateService;
        _logger = logger;
    }

    public float ConvertEuroToFrancs(float amount)
    {
        var ratio = _exchangeRateService.GetEuroToFrancsRate();

        if (ratio is 0)
        {
            throw new Exception("Rate is wrong");
        }

        var converted = amount * ratio;

        _logger.LogInformation("Conversion {Amount}*{Rate}={Converted}", amount,
            ratio, converted);

        return converted;
    }
}