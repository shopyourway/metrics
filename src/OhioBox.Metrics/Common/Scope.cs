using System;

namespace OhioBox.Metrics.Common
{
	internal class Scope : IDisposable
	{
		private readonly Action _endAction;

		public Scope(Action startAction, Action endAction)
		{
			_endAction = endAction;

			startAction?.Invoke();
		}

		public void Dispose()
		{
			_endAction?.Invoke();
		}
	}
}