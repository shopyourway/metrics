using System;

namespace OhioBox.Metrics.Bootstrap
{
	public class Registration
	{
		public Type Interface { get; private set; }
		public Type Implementor { get; private set; }

		private Registration(Type interfaceType, Type implementor)
		{
			Interface = interfaceType;
			Implementor = implementor;
		}

		public static Registration Create<TService, TImplementation>()
			where TService : class
			where TImplementation : class, TService
		{
			return new Registration(typeof(TService), typeof(TImplementation));
		}
	}
}