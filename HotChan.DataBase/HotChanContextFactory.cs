using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace HotChan.DataBase
{
	// this is used to get conenction strings
	public class HotChanContextFactory : IDesignTimeDbContextFactory<HotChanContext>
	{
		public HotChanContext CreateDbContext(string[] args)
		{
			var configuration = new ConfigurationBuilder()
				.SetBasePath(Directory.GetCurrentDirectory())
				.AddJsonFile("appsettings.json")
				.Build();

			var builder = new DbContextOptionsBuilder<HotChanContext>();
			var connectionString = configuration.GetConnectionString("hotchandatabase-postgres-dev");
			builder.UseNpgsql(connectionString);

			return new HotChanContext(builder.Options);
		}
	}
}
