using System;

namespace OhioBox.Metrics
{
	public class Meter
	{
		internal static readonly Meter NullMeter = new Meter(null);

		internal static event Action<DataPoint> Report = d => { };

		internal string Key { get; }

		internal Meter(string key)
		{
			if (key == null) return;

			Key = key
				.Replace("<", ".")
				.Replace(">", "")
				.Replace(":", "")
				.Replace("|", "");
		}

		public void TimeInTicks(long value)
		{
			if (Key == null) return;
			Send(new DataPoint(Key, value, "ticks"));
		}

		public void Increment(int value)
		{
			if (Key == null) return;
			Send(new DataPoint(Key, value, "c"));
		}

		public void ValueDistribution(int value)
		{
			Send(new DataPoint(Key, value, "v"));
		}

		private static void Send(DataPoint dataPoint)
		{
			if (MetricsState.IsReportingEnabled())
				Report(dataPoint);
		}
	}
}