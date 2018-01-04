using System;
using System.Collections;
using System.Collections.Generic;

namespace OhioBox.Metrics.Reporters.Testing
{
	public class InMemoryMetricsReporterFactory : IMetricsReporterFactory
	{
		private readonly ICollection<DataPoint> _buffer;

		public InMemoryMetricsReporterFactory(ICollection<DataPoint> buffer)
		{
			_buffer = buffer;
		}

		public Action<DataPoint> CreateReporter()
		{
			return dataPoint =>
			{
				lock (((ICollection) _buffer).SyncRoot)
				{
					_buffer.Add(dataPoint);
				}
			};
		}
	}
}