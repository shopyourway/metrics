#if (!NETSTANDARD1_6)
using System.Diagnostics.Contracts;
#endif

namespace OhioBox.Metrics
{
	public struct MetricsSpace
	{
		private readonly string _key;
		internal static readonly MetricsSpace NullSpace = new MetricsSpace(null);

		internal MetricsSpace(string key)
		{
			_key = key;
		}

#if (!NETSTANDARD1_6)
		[Pure]
#endif
		public Meter Meter(string key)
		{
			if (_key == null) return OhioBox.Metrics.Meter.NullMeter;
			return Metrics.Meter(_key + "." + key);
		}

#if (!NETSTANDARD1_6)
		[Pure]
#endif
		public MetricsSpace Space(string key)
		{
			if (_key == null) return NullSpace;
			return Metrics.Space(_key + "." + key);
		}

#if (!NETSTANDARD1_6)
		[Pure]
#endif
		public StepTimer StepTimer()
		{
			return new StepTimer(this);
		}
	}
}