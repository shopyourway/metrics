using System;

namespace OhioBox.Metrics.Reporters
{
	public interface IMetricsReporterFactory
	{
		Action<DataPoint> CreateReporter();
	}
}