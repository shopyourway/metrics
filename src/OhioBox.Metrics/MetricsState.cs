using System;
using OhioBox.Time;

namespace OhioBox.Metrics
{
	internal static class MetricsState
	{
		private static DateTime? _suspendTill;

		public static bool IsReportingEnabled()
		{
			if (!_suspendTill.HasValue)
				return true;
			
			if (SystemTime.Now() >= _suspendTill.Value)
			{
				_suspendTill = null;
				return true;
			}

			return false;
		}

		public static void EnableReporting()
		{
			_suspendTill = null;
		}

		public static void SuspendReporting(TimeSpan @for)
		{
			_suspendTill = SystemTime.Now().Add(@for);
		}

		public static void DisableReporting()
		{
			_suspendTill = DateTime.MaxValue;
		}
	}
}