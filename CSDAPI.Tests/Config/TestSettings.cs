using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Mime;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.WindowsRuntime;
using CSDAPI.Interfaces;
using CSDAPI.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.Adapter;
using Xunit;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace CSDAPI.Tests.Config
{
	public class TestSettings
	{
		private static IConfiguration _configuration;

		private static IConfiguration Configuration
		{
			get
			{
				if (_configuration != null) return _configuration;

				var config = ConfigPath();
				if (!File.Exists(config))
				{
					var example = config + ".example";
					if (File.Exists(example))
					{ 
						throw new XunitException("Need Configuration: Copy 'testsettings.json.example' to 'testsettings.json' and fill in the details.");
					}
					throw new XunitException("Need Configuration: Create 'testsettings.json' file.");
				}
			
				_configuration = new ConfigurationBuilder().AddJsonFile(config).Build();				
				
				return _configuration;
			}
		}
		
		
		private static string ConfigPath([CallerFilePath] string callerFilePath = "")
		{
			return Path.GetFullPath(Path.GetDirectoryName(callerFilePath).TrimEnd('\\', '/') + "/../testsettings.json");
		}

		
		private IDictionary<string, string> GetRequiredConfig(params string[] keys)
		{
			var errors = new List<string>();
			var ret = new Dictionary<string,string>();

			foreach (var key in keys)
			{
				ret[key] = Configuration[key];
				if (string.IsNullOrWhiteSpace(ret[key]))
				{
					errors.Add($"The '{key}' value cannot be blank in 'testsettings.json'.");
				}
			}

			if (errors.Count > 0)
			{
				throw new XunitException(string.Join("\r\n", errors));
			}
			
			return ret;
		}
		
		public IApiSettings GetApiSettings()
		{
			var cfg = GetRequiredConfig("host", "apikey");
			return new ApiSettings(cfg["apikey"], cfg["host"]);
		}
		
	}
}