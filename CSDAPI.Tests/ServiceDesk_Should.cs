using System;
using System.Linq;
using CSDAPI.Tests.Config;
using Xunit;
using Xunit.Abstractions;

namespace CSDAPI.Tests
{
	public class ServiceDesk_Should : IClassFixture<TestSettings>
	{
		private readonly ITestOutputHelper _output;
		private readonly TestSettings _settings;
		
		public ServiceDesk_Should(TestSettings settings, ITestOutputHelper output)
		{
			_settings = settings ?? throw new ArgumentNullException(nameof(settings));
			_output = output ?? throw new ArgumentNullException(nameof(output));
		}

		[Fact]
		public void QueryAssetTypes()
		{
			var sd = new ServiceDesk(_settings.GetApiSettings());
			var result = sd.GetAssetTypes();
			Assert.NotNull(result);
			_output.WriteLine($"Retrieved {result.Count()} asset types.");
		}

		[Fact]
		public void QueryCategories()
		{
			var sd = new ServiceDesk(_settings.GetApiSettings());
			var result = sd.GetCategories();
			Assert.NotNull(result);
			_output.WriteLine($"Retrieved {result.Count()} categories.");
		}

		[Fact]
		public void QueryTicketCategories()
		{
			var sd = new ServiceDesk(_settings.GetApiSettings());
			var result = sd.GetTicketCategories();
			Assert.NotNull(result);
			_output.WriteLine($"Retrieved {result.Count()} ticket categories.");
		}
	}
}