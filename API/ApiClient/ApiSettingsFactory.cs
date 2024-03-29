﻿using Microsoft.Extensions.Configuration;

namespace ApiClient
{
	public class ApiSettingsFactory : IApiSettingsFactory
	{
		private IConfigurationRoot config;

		public ApiSettingsFactory()
		{
			config = new ConfigurationBuilder().SetBasePath("C:\\sketch\\CustomIndexService\\API\\StockAggregation.Service").AddJsonFile("appsettings.json").Build();
		}

		public ApiSettings GetPolygonSettings()
		{
			return new ApiSettings() { Key = config["ApiSettings:PolygonKey"], Backup = config["ApiSettings:BackupPolygonKey"] };
		}
	}
}
