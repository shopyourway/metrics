using System;

namespace OhioBox.Metrics.Reporters.Console
{
	internal class ConsoleReporter
	{
		private readonly int _ignoreValuesBelow;

		public ConsoleReporter(int ignoreValuesBelow)
		{
			_ignoreValuesBelow = ignoreValuesBelow;
		}

		public void Report(DataPoint dataPoint)
		{
			if (dataPoint.Units == "ticks")
			{
				dataPoint.Value /= TimeSpan.TicksPerMillisecond;
				dataPoint.Units = "ms";
			}

			if (dataPoint.Units != "ms")
				return;

			if (dataPoint.Value < _ignoreValuesBelow)
				return;
			
			System.Console.WriteLine(dataPoint.Key + "\t" + dataPoint.Value + "\t" + dataPoint.Units);
		}
	}
}