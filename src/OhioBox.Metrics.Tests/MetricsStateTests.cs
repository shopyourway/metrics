using System;
using NUnit.Framework;
using OhioBox.Time;

namespace OhioBox.Metrics.Tests
{
	[TestFixture]
	public class MetricsStateTests
	{
		[SetUp]
		public void Setup()
		{
			ResetState();
		}

		[Test]
		public void IsReportingEnabled_NoSuspension_ReturnTrue()
		{
			var result = MetricsState.IsReportingEnabled();

			Assert.That(result, Is.True);
		}

		[Test]
		public void IsReportingEnabled_HasSuspension_ReturnFalse()
		{
			FreezeSystemTime(SystemTime.Now());
			MetricsState.SuspendReporting(TimeSpan.FromMinutes(5));

			var result = MetricsState.IsReportingEnabled();

			Assert.That(result, Is.False);
		}

		[Test]
		public void IsReportingEnabled_ZeroSuspension_ReturnTrue()
		{
			FreezeSystemTime(SystemTime.Now());
			MetricsState.SuspendReporting(TimeSpan.FromMinutes(0));

			var result = MetricsState.IsReportingEnabled();

			Assert.That(result, Is.True);
		}

		[Test]
		public void IsReportingEnabled_SuspensionEnded_ReturnTrue()
		{
			var now = SystemTime.Now();
			FreezeSystemTime(now);
			const int seconds = 10;
			var suspension = TimeSpan.FromSeconds(seconds);
			MetricsState.SuspendReporting(suspension);
			var futureNow = now.AddSeconds(seconds + 1);
			FreezeSystemTime(futureNow);

			var result = MetricsState.IsReportingEnabled();

			Assert.That(result, Is.True);
		}

		[Test]
		public void IsReportingEnabled_SuspensionNotOverButEnabledExplicitly_ReturnTrue()
		{
			var suspension = TimeSpan.FromSeconds(10);
			FreezeSystemTime(SystemTime.Now());
			MetricsState.SuspendReporting(suspension);
			MetricsState.EnableReporting();

			var result = MetricsState.IsReportingEnabled();

			Assert.That(result, Is.True);
		}

		[Test]
		public void IsReportingEnabled_SuspensionIsOverButDisabledExplicitly_ReturnFalse()
		{
			var now = SystemTime.Now();
			FreezeSystemTime(now);
			const int seconds = 10;
			var suspension = TimeSpan.FromSeconds(seconds);
			MetricsState.SuspendReporting(suspension);
			var futureNow = now.AddSeconds(seconds + 1);
			FreezeSystemTime(futureNow);
			MetricsState.DisableReporting();

			var result = MetricsState.IsReportingEnabled();

			Assert.That(result, Is.False);
		}

		private static void ResetState()
		{
			MetricsState.EnableReporting();
		}

		private static void FreezeSystemTime(DateTime time)
		{
			SystemTime.Now = () => time;
		}
	}
}
