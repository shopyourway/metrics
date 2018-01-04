using System;
using System.Linq;

namespace OhioBox.Metrics
{
	public static class Metrics
	{
		public static MetricsSpace NullSpace = MetricsSpace.NullSpace;

		public static Meter Meter(string key)
		{
			return new Meter(key);
		}

		public static MetricsSpace Space(string keyFormat, params object[] args)
		{
			if (args == null || !args.Any())
				return new MetricsSpace(keyFormat);

			return new MetricsSpace(string.Format(keyFormat, args));
		}

		public static void ReportException<T>(T exception) where T : Exception
		{
			Space("Exceptions").Meter(exception.GetType().Name).Increment(1);
		}
	}
}