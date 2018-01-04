using System;

namespace OhioBox.Metrics.Reporters.Console
{
	public class ConsoleMetricsReporterFactory : IMetricsReporterFactory
	{
		private readonly int _ignoreValuesBelow;

		public ConsoleMetricsReporterFactory(int ignoreValuesBelow)
		{
			_ignoreValuesBelow = ignoreValuesBelow;
		}

		public Action<DataPoint> CreateReporter()
		{
			return new ConsoleReporter(_ignoreValuesBelow).Report;
		}
	}
}