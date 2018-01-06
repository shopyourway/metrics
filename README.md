# Metrics
Metrics is a framework for reporting data point information (measurements and time-series) for .NET applications, uncompromisingly simple.

Metrics can also be used as a .NET client for Graphite.

## Highlights
* We currently support the following targets out of the box:
	* Graphite
	* Console
	* In-momeory (for tests)
* .Net Core support - targeting .Net Standard 1.6

## Getting started
### Installation
[![NuGet](https://img.shields.io/nuget/v/OhioBox.Metrics.svg?style=flat-square)](https://www.nuget.org/packages/OhioBox.Metrics/)

### Start Reporting Metrics
To start reporting metrics on an application, just call `MetricsStarter.Initialize` with the required reporters, on the application startup:

```cs
MetricsStarter.Initialize(
    new GraphiteMetricsReporterFactory(_graphiteConfiguration),
    new ConsoleMetricsReporterFactory(0)
);
```

Next, to report metrics data, you need to use the static `Metrics` object, with the required operation. For example:

```cs
//first, declare a space:
private MetricsSpace _metrics = Metrics.Space("Custom").Space("SubSpace").Space("SubSubSpace");

public void SomeMethod()
{
	//reporting an increment:
	_metrics.Meter("MeterName").Increment(1);


	//reporting execution time:
	using (_meter("MeterName").TimeScope())
	{
		// operations to measure...
	}

	//reporting value distribution:
	_metrics.Meter("MeterName").ValueDistribution(value);
}
```

### Using Graphite
One of Metric's main targets is Graphite. Graphite is a monitoring tool that applications can report numeric data-points to, and afterwards visualize this data. (more info abour Graphite can be found on Graphite's webstite: https://graphiteapp.org/.

To use Graphite, you will need to implement `IGraphiteConfiguration` on your application, and pass it to the `GraphiteMetricsReporterFactory` as shown above.

```cs
public class GraphiteConfiguration : IGraphiteConfiguration
{
	public string IpAddress { get; }

	public GraphiteConfiguration(string ipAddress)
	{
		IpAddress = ipAddress;
	}
}
```

### Reporting Control
You can turn on/off/suspend the metrics reporting while the application is still running.

Just take dependency on `IMetricsSwitch` and use the actions it provides. Note that `IMetricsSwitch` controls *all* metrics reporters.

There's also a switch for controlling *graphite only*, with the suprising name - `IGraphiteSwitch`.


### Customize
You can customize metrics and take it to a whole new level by creating your own implementation of `IMetricsReporterFactory`.

Implement the `CreateReporter` Method, and register it to `MetricsStarter.Initialize`, along with all other out-of-the-box reporters.

For example, this is basically how our beloved PerfDbg works... it's based on metrics reporting:

```cs
public class PerfDbgReporterFactory : IMetricsReporterFactory
{
	public Action<DataPoint> CreateReporter()
	{
		return PerfDbgReporter.Report;
	}
}
```

## Development

### How to contribute
We encorage contribution via pull requests on any feature you see fit.

When submitting a pull request make sure to do the following:
* Check that new and updated code follows OhioBox existing code formatting and naming standard
* Run all tests to ensure no existing functionality has been affected
* Write tests to test your changes. All features and fixed bugs must have tests to verify they work. Read [GitHub Help](https://help.github.com/articles/about-pull-requests/) for more details about creating pull requests

### Running tests
You can simply run the tests in Visual Studio or with NUnit test runner.
