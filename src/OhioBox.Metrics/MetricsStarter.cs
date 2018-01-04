using OhioBox.Metrics.Reporters;

namespace OhioBox.Metrics
{
	public static class MetricsStarter
	{
		public static void Initialize(params IMetricsReporterFactory[] reporterFactories)
		{
			foreach (var reporterFactory in reporterFactories)
			{
				Meter.Report += reporterFactory.CreateReporter();
			}
		}
	}
}
