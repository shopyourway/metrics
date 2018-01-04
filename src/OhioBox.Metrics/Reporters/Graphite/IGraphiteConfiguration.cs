namespace OhioBox.Metrics.Reporters.Graphite
{
	public class GraphiteConfiguration
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