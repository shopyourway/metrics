﻿using System;

namespace OhioBox.Metrics.Reporters.Graphite
{
	public class GraphiteMetricsReporterFactory : IMetricsReporterFactory
	{
		private readonly IGraphiteConfiguration _configuration;
		private readonly IGraphiteSwitch _graphiteSwitch;

		public GraphiteMetricsReporterFactory(IGraphiteConfiguration configuration, IGraphiteSwitch graphiteSwitch = null)
		{
			_configuration = configuration;
			_graphiteSwitch = graphiteSwitch;
		}

		public Action<DataPoint> CreateReporter()
		{
			return new GraphiteReporter(_configuration, _graphiteSwitch).Report;
		}
	}
}