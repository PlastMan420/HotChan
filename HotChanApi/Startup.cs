using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Linq;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using HotChan.DataAccess.Data;
using HotChan.DataBase.Models;
using HotChocolate.AspNetCore;
using HotChan.DataBase;
using Microsoft.AspNetCore.Authentication;
using HotChan.DataAccess.DataLoader;
using HotChan.DataAccess.Repository;
using HotChan.DataAccess.Users;
using System;
using HotChocolate.Types;
using HotChocolate.Language;

namespace HotChanApi
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			//services.AddResponseCompression(opts =>
			//{
			//	opts.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(
			//		new[] { "application/octet-stream" });
			//});

			// Add JWT Tokens. Also authentication must be Added inorder to get services.AddIdentityCore<>() to work.
			// TODO

			services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
				.AddJwtBearer(options =>
				options.TokenValidationParameters = new TokenValidationParameters
				{
						// no validation rn since we will be just using localhost -->
						ValidateIssuerSigningKey = true,
						ValidateAudience = false,
						IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("jwt:SymmetricKey"))

				});

			// DataBase
			var ConnectionString = Configuration.GetConnectionString("hotchandatabase-postgres-dev");
			var migrationAssembly = this.GetType().Assembly.FullName;

			services.AddPooledDbContextFactory<HotChanContext>(options => 
				options.UseNpgsql(ConnectionString, b => b.MigrationsAssembly(migrationAssembly))
			);

			// ASP.NET Identity
			services.AddIdentity<User, Role>(
				options => {
					options.SignIn.RequireConfirmedAccount = false;
					options.Password.RequiredLength = 10;
					options.Password.RequiredUniqueChars = 1;
					options.User.RequireUniqueEmail = true;
								//Other options go here
				}
			).AddEntityFrameworkStores<HotChanContext>()
			.AddDefaultTokenProviders();

			// Services
			services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
			services.AddScoped<IUserRepository, UserRepository>();

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
				.AddDataLoader<PostsDL>()
				.AddDataLoader<UsersDL>()
				.AddDataLoader<UserSubmissionsDL>()
				.AddType<UploadType>();

			;

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