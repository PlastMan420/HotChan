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
using System.Data.SqlClient;
using System.Security.Cryptography;

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

			//services.AddResponseCompression(opts =>
			//{
			//	opts.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(
			//		new[] { "application/octet-stream" });
			//});

			// Add JWT Tokens. Also authentication must be Added inorder to get services.AddIdentityCore<>() to work.
			// TODO
			// Asymmetric JWT auth:
			// https://blog.devgenius.io/jwt-authentication-in-asp-net-core-e67dca9ae3e8
			services.AddSingleton<RsaSecurityKey>(provider =>
			{
				RSA rsa = RSA.Create();

				rsa.ImportRSAPublicKey(
					source: Convert.FromBase64String(Configuration["jwt:rs512-public"]),
					bytesRead: out int _
				);
				return new RsaSecurityKey(rsa);
			});

			services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
				.AddJwtBearer("Asymmetric", options =>
				{
					SecurityKey rsa = services.BuildServiceProvider().GetRequiredService<RsaSecurityKey>();
					options.IncludeErrorDetails = true; // <- great for debugging

					// Configure the actual Bearer validation
					options.TokenValidationParameters = new TokenValidationParameters
					{
						IssuerSigningKey = rsa,
						ValidAudience = "jwt-test",
						ValidIssuer = "jwt-test",
						RequireSignedTokens = true,
						RequireExpirationTime = true, // <- JWTs are required to have "exp" property set
						ValidateLifetime = true, // <- the "exp" will be validated
						ValidateAudience = true,
						ValidateIssuer = true,
					};
				});

			// DataBase
			var csBuilder = new SqlConnectionStringBuilder(
				Configuration.GetConnectionString("hotchandatabase-postgres-dev"));
			csBuilder.Password = Configuration["db:password"];
			_connection = csBuilder.ConnectionString;

			var migrationAssembly = this.GetType().Assembly.FullName;

			services.AddPooledDbContextFactory<HotChanContext>(options => 
				options.UseNpgsql(_connection, b => b.MigrationsAssembly(migrationAssembly))
			);

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
				.AddDataLoader<PostsDL>()
				.AddDataLoader<UsersDL>()
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