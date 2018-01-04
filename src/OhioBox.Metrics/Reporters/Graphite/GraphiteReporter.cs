using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace OhioBox.Metrics.Reporters.Graphite
{
	internal class GraphiteReporter
	{
		private readonly IGraphiteSwitch _graphiteSwitch;
		private readonly IPEndPoint _iPEndPoint;
		private readonly bool _reportMachineName;
		private readonly UdpClient _udpClient;
		private readonly string _prefix;

		public GraphiteReporter(GraphiteConfiguration configuration, IGraphiteSwitch graphiteSwitch)
		{
			if (configuration.IpAddress == null)
				return;

			var values = configuration.IpAddress.Split(':');
			IPAddress ipAddress;
			ipAddress = IPAddress.TryParse(values[0], out ipAddress) ? ipAddress : null;

			if (ipAddress == null)
				return;

			_graphiteSwitch = graphiteSwitch;
			_iPEndPoint = new IPEndPoint(ipAddress, 8125);
			_reportMachineName = configuration.ReportMachineName;
			_prefix = configuration.Prefix;
			_udpClient = new UdpClient();
#if (!NETSTANDARD1_6)
			_udpClient.Connect(_iPEndPoint);
#endif
		}

		public void Report(DataPoint dataPoint)
		{
			if (_iPEndPoint == null || !IsEnabled())
				return;

			var key = BuildKey(dataPoint);

			var value = dataPoint.Value;
			var units = dataPoint.Units;
			if (units == "ticks")
			{
				value = value / TimeSpan.TicksPerMillisecond;
				units = "ms";
			}

			if (units == "v") // statsd (part of graphite) statisticly distributes only things marked with "ms"
				units = "ms";

			if (value == 0)
				return;

			var sendBytes = Encoding.ASCII.GetBytes($"{key}:{value}|{units}");
#if (!NETSTANDARD1_6)
			_udpClient.BeginSend(sendBytes, sendBytes.Length, null, null);
#else
			_udpClient.SendAsync(sendBytes, sendBytes.Length, _iPEndPoint);
#endif
		}

		private bool IsEnabled()
		{
			return _graphiteSwitch == null || _graphiteSwitch.IsEnabled();
		}

		private string BuildKey(DataPoint dataPoint)
		{
			var key = dataPoint.Key;

			if (_prefix != null)
				key = AddPrefix(key);
			else if (_reportMachineName)
				key = $"Machines.{Environment.MachineName}.{key}";

			return key;
		}

		private string AddPrefix(string key)
		{
			var shouldAddDot = !_prefix.EndsWith(".");

			return shouldAddDot ? $"{_prefix}.{key}" : $"{_prefix}{key}";
		}
	}
}