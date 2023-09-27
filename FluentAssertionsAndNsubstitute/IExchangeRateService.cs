namespace FluentAssertionsAndNSubstitute;

public interface IExchangeRateService
{
    float GetEuroToFrancsRate();
}

public interface ILogger
{
    void LogInformation(string message, params object[] args);
}