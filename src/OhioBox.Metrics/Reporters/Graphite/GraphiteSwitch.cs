using System;

namespace OhioBox.Metrics.Reporters.Graphite
{
	public interface IGraphiteSwitch
	{
		bool IsEnabled();
		void On();
		void Off();
		void Suspend(TimeSpan @for);
	}

	internal class GraphiteSwitch : IGraphiteSwitch
	{
		public bool IsEnabled()
		{
			return GraphiteState.IsReportingEnabled();
		}

		public void On()
		{
			GraphiteState.EnableReporting();
		}

		public void Off()
		{
			GraphiteState.DisableReporting();
		}

		public void Suspend(TimeSpan @for)
		{
			GraphiteState.SuspendReporting(@for);
		}
	}
}