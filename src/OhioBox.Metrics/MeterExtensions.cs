using System;
using System.Diagnostics;
using OhioBox.Metrics.Common;

namespace OhioBox.Metrics
{
	public static class MeterExtensions
	{
		public static void Time(this Meter target, TimeSpan value)
		{
			target.TimeInTicks(value.Ticks);
		}

		public static T Time<T>(this Meter target, Func<T> action)
		{
			using (target.TimeScope())
			{
				return action();
			}
		}

		public static void Time(this Meter target, Action action)
		{
			using (target.TimeScope())
				action();
		}

		public static IDisposable TimeScope(this Meter target)
		{
			return TimeScope(target, 0);
		}

		public static IDisposable TimeScope(this Meter target, long minDuration)
		{
			var sw = new Stopwatch();
			return new Scope(sw.Start, () => {
				sw.Stop();
				if (sw.Elapsed.Ticks >= minDuration)
					target.Time(sw.Elapsed);
			});
		}

		public static IDisposable TimeScopeWithTreshold(this Meter target, long maxDuration)
		{
			var sw = new Stopwatch();
			return new Scope(sw.Start, () => {
				sw.Stop();
				target.Time(sw.Elapsed);
				if (sw.Elapsed.Milliseconds >= maxDuration)
					new Meter(target.Key + ".ExceededThreshold").Increment(1);
			});
		}
	}
}