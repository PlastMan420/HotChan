using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using HotChan.DataAccess.Data;
using HotChan.DataBase;
using HotChan.DataAccess.DataLoader;
using HotChan.DataAccess.Repository;
using HotChan.DataAccess.Users;
using System;
using System.Security.Cryptography;
using HotChan.DataBase.Models.Entities;
using HotChanApi.Services;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using HotChocolate.Language;
using HotChocolate.Types;
using HotChanApi.Authentication;

namespace HotChanApi
{
    public class Startup
	{
		private string _connection = null;

		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddControllers();

			// DataBase
			var csBuilder = new SqlConnectionStringBuilder(
				Configuration.GetConnectionString("hotchandatabase-postgres-dev"));
			csBuilder.Password = Configuration["db:password"];
			_connection = csBuilder.ConnectionString;

			//services.AddPooledDbContextFactory<HotChanContext>(options => 
			//	options.UseNpgsql(_connection, b => b.MigrationsAssembly(migrationAssembly))
			//);

			services.AddDbContext<HotChanContext>(
				options => options.UseNpgsql(_connection));

			// ASP.NET Identity
			services.AddIdentity<User, Role>(
				options => {
					options.SignIn.RequireConfirmedAccount = false;
					options.Password.RequiredLength = 12;
					options.Password.RequiredUniqueChars = 1;
					options.Password.RequireDigit = true;
					options.User.RequireUniqueEmail = true;
								//Other options go here
				}
			).AddEntityFrameworkStores<HotChanContext>()
			.AddDefaultTokenProviders();

			// Services
			services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
			services.AddScoped<IUserRepository, UserRepository>();

			services.AddAuthorization(options => {
				options.AddPolicy("AdminAccess", policy => 
					policy.RequireAssertion(context => 
						context.User.IsInRole("Admin")));

				options.AddPolicy("UserAccess", policy => 
					policy.RequireAssertion(context =>
						context.User.IsInRole("Member")));

				options.AddPolicy("RestrictedAccess", policy =>
					policy.RequireAssertion(context =>
						context.User.IsInRole("Banned")));
			});

			var adminRole = new Role("Admin");

			// GraphQL
			services
				.AddSingleton<IUserRepository, UserRepository>()
				.AddMemoryCache()
				.AddSha256DocumentHashProvider(HashFormat.Hex)
				.AddGraphQLServer()
				.AddAuthorization()
				.AddQueryType(d => d.Name("Query"))
					.AddTypeExtension<UserQuery>()
					.AddTypeExtension<PostQuery>()
					.UseAutomaticPersistedQueryPipeline()
					.AddInMemoryQueryStorage()
				.AddMutationType<PostMutation>()
				.AddDataLoader<PostsBatchDL>()
				.AddDataLoader<UsersBatchDL>()
				.AddDataLoader<UserSubmissionsDL>()
				.AddType<UploadType>();

		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();

			}
			else
			{
				app.UseExceptionHandler("/Error");
				app.UseHsts();
			}

			app.UseHttpsRedirection();
			app.UseStaticFiles();

			app.UseWebSockets();
			app.UseRouting();

			app.UseAuthentication();
			//app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapGraphQL();
				endpoints.MapControllers();

			});

			//app.UseCors();

		}
	}
}