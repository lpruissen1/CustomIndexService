using Database.Core;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using System;
using Users.Database;
using Users.Database.Config;
using Users.Database.Repositories;
using Users.Database.Repositories.Interfaces;

namespace Users.Tests
{
	public class DbTestBase
	{
		protected IMongoDBContext context;

		protected IUserPositionsRepository UserPositionsRepository;

		protected Guid userId = Guid.NewGuid();

		[OneTimeSetUp]
		public virtual void OneTimeSetUp()
		{
			var config = new ConfigurationBuilder().SetBasePath("C:\\sketch\\CustomIndexService\\API\\Users.Tests").AddJsonFile("appsettings.json").Build();
			var dbSettings = new UserDatabaseSettings() { ConnectionString = config["UserDatabaseSettings:ConnectionString"], DatabaseName = config["UserDatabaseSettings:DatabaseName"] };

			context = new MongoUserDbContext(dbSettings);
		}

		[SetUp]
		public virtual void SetUp()
		{
			context.ClearAll();

			UserPositionsRepository = new UserPositionsRepository(context);
		}
	}
}

