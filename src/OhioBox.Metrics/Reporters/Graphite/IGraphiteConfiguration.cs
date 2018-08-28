namespace OhioBox.Metrics.Reporters.Graphite
{
	public interface IGraphiteConfiguration
	{
		string IpAddress { get; }
		bool ReportMachineName { get; }
		string Prefix { get; }
	}

	public class GraphiteConfiguration : IGraphiteConfiguration
	{
		public string IpAddress { get; }
		public bool ReportMachineName { get; }
		public string Prefix { get; }

		public GraphiteConfiguration(string ipAddress, bool reportMachineName, string prefix)
		{
			IpAddress = ipAddress;
			ReportMachineName = reportMachineName;
			Prefix = prefix;
		}
	}
}