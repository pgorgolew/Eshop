using System.Diagnostics.Metrics;

namespace Eshop.Application.Metrics;

public class MetricsService(IMeterFactory meterFactory)
{
    private readonly Dictionary<string, Counter<int>> _errorCounters = new();
    private readonly Meter _meter = meterFactory.Create("Eshop");

    public void RecordError(string errorTypeMetricName, int count = 1)
    {
        if (!_errorCounters.ContainsKey(errorTypeMetricName))
        {
            _errorCounters[errorTypeMetricName] = _meter.CreateCounter<int>($"errors.{errorTypeMetricName}");
        }

        _errorCounters[errorTypeMetricName].Add(count);
    }
}