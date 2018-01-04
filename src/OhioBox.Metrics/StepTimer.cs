using System;

namespace OhioBox.Metrics
{
	public class StepTimer : IDisposable
	{
		public StepTimer(MetricsSpace space)
		{
			_space = space;
		}

		private MetricsSpace _space;
		private IDisposable _timingScope;

		public void MarkStep(string key)
		{
			_timingScope?.Dispose();
			_timingScope = _space.Meter(key).TimeScope();
		}

		public void Dispose()
		{
			_timingScope?.Dispose();
		}
	}
}