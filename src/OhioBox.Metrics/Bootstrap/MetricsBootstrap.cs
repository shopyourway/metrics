using OhioBox.Metrics.Reporters.Graphite;

namespace OhioBox.Metrics.Bootstrap
{
	public static class MetricsBootstrap
	{
		public static Registration[] GetRegistrationCandidates()
		{
			return new[]
			{
				Registration.Create<IMetricsSwitch, MetricsSwitch>(),
				Registration.Create<IGraphiteSwitch, GraphiteSwitch>()
			};
		}
	}
}