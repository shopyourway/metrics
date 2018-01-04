namespace OhioBox.Metrics
{
	public class DataPoint
	{
		public string Key { get; set; }
		public long Value { get; set; }
		public string Units { get; set; }

		public string StackTrace { get; set; }

		public DataPoint(string key, long value, string units)
		{
			Key = key;
			Value = value;
			Units = units;
		}
	}
}