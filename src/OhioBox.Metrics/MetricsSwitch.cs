using System;

namespace OhioBox.Metrics
{
	public interface IMetricsSwitch
	{
		bool IsEnabled();
		void On();
		void Off();
		void Suspend(TimeSpan @for);
	}

	internal class MetricsSwitch : IMetricsSwitch
	{
		public bool IsEnabled()
		{
			return MetricsState.IsReportingEnabled();
		}

		public void On()
		{
			MetricsState.EnableReporting();
		}

		public void Off()
		{
			MetricsState.DisableReporting();
		}

		public void Suspend(TimeSpan @for)
		{
			MetricsState.SuspendReporting(@for);
		}
	}
}