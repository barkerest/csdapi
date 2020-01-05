using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using CSDAPI.Models;
using Xunit;

namespace CSDAPI.Tests.Models
{
	public class ApiSettings_Should
	{
		[Fact]
		public void RequireApiKey()
		{
			ArgumentException ex = Assert.Throws<ArgumentNullException>(() => new ApiSettings(null, "test.example.com"));
			Assert.Equal("apiKey", ex.ParamName);

			ex = Assert.Throws<ArgumentException>(() => new ApiSettings("", "test.example.com"));
			Assert.Equal("apiKey", ex.ParamName);
			Assert.Contains("blank", ex.Message);

			ex = Assert.Throws<ArgumentException>(() => new ApiSettings("    ", "test.example.com"));
			Assert.Equal("apiKey", ex.ParamName);
			Assert.Contains("blank", ex.Message);
		}

		[Fact]
		public void RequireHostName()
		{
			ArgumentException ex = Assert.Throws<ArgumentNullException>(() => new ApiSettings("xxxx", null));
			Assert.Equal("hostName", ex.ParamName);

			ex = Assert.Throws<ArgumentException>(() => new ApiSettings("xxxx", ""));
			Assert.Equal("hostName", ex.ParamName);
			Assert.Contains("blank", ex.Message);

			ex = Assert.Throws<ArgumentException>(() => new ApiSettings("xxxx", "    "));
			Assert.Equal("hostName", ex.ParamName);
			Assert.Contains("blank", ex.Message);
		}
		
		[Fact]
		public void RequireBasePath()
		{
			ArgumentException ex = Assert.Throws<ArgumentNullException>(() => new ApiSettings("xxxx", "test.example.com", basePath: null));
			Assert.Equal("basePath", ex.ParamName);

			ex = Assert.Throws<ArgumentException>(() => new ApiSettings("xxxx", "test.example.com", basePath: ""));
			Assert.Equal("basePath", ex.ParamName);
			Assert.Contains("blank", ex.Message);

			ex = Assert.Throws<ArgumentException>(() => new ApiSettings("xxxx", "test.example.com", basePath: "    "));
			Assert.Equal("basePath", ex.ParamName);
			Assert.Contains("blank", ex.Message);
		}

		[Theory]
		[InlineData("xyz", true, "xyz.servicedesk-us.comodo.com")]
		[InlineData("xyz", false, "xyz.servicedesk.comodo.com")]
		public void CreateAppropriateHostNames(string companySub, bool usDomain, string expected)
		{
			var settings = ApiSettings.Create("xxxx", companySub, usDomain);
			Assert.Equal(expected, settings.HostName);
		}

		public static IEnumerable<object[]> GetUriForData()
			=> new (ApiSettings, string, string)[]
			{
				(new ApiSettings("xxxx", "localhost"), "help", "https://localhost/clientapi/index.php?serviceName=help" ),
				(new ApiSettings("xxxx", "localhost", port: 8443),"help", "https://localhost:8443/clientapi/index.php?serviceName=help" ),
				(new ApiSettings("xxxx", "example.com", basePath: "/api"), "help", "https://example.com/api?serviceName=help" ),
			}.Select(x => new object[] {x.Item1, x.Item2, x.Item3});

		[Theory]
		[MemberData(nameof(GetUriForData))]
		public void ConstructUriAppropriately(ApiSettings settings, string serviceName, string expectedUri)
		{
			var result = settings.GetUriFor(serviceName);
			Assert.Equal(expectedUri, result.ToString());
		}
	}
}