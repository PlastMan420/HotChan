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
using HotChan.DataAccess.Users;
using System;

namespace HotChanApi
{
	public class Startup
	{
		public const string Users = "users";
		public const string Posts = "posts";

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
			//services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
			//	options.TokenValidationParameters = new TokenValidationParameters
			//	{
			//		ValidateIssuerSigningKey = true,
			//		IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Configuration.GetSection("AppSettings:Token").Value)),
			//		// no validation rn since we will be just using localhost -->
			//		ValidateIssuer = false,
			//		ValidateAudience = false
			//	});

			// DbContext and Postgres connection string.
			// Connection String is in the secrets storage.
			//_PostGresConnectionString = Configuration["ConnectionStrings:hotchandatabase-postgres-dev"];
			//services.AddDbContext<HotChanContext>(options =>
			//	options.UseNpgsql(Configuration.GetConnectionString("hotchandatabase-postgres-dev")));
			var ConnectionString = Configuration.GetConnectionString("hotchandatabase-postgres-dev");
			var migrationAssembly = this.GetType().Assembly.FullName;

			services.AddPooledDbContextFactory<HotChanContext>(options => 
				options.UseNpgsql(ConnectionString, b => b.MigrationsAssembly(migrationAssembly))
			);

			services.AddHttpClient(Users, c => c.BaseAddress = new Uri("https://users.localhost:5001/graphql"));
			services.AddHttpClient(Posts, c => c.BaseAddress = new Uri("https://posts.localhost:5001/graphql"));

			services.AddGraphQLServer()
					.AddQueryType(d => d.Name("Query"))
						.AddTypeExtension<UserQuery>()
						.AddTypeExtension<PostQuery>()
					.AddMutationType<PostMutation>()
					.AddDataLoader<PostsDL>()
					.AddDataLoader<UsersDL>()
					.AddDataLoader<UserSubmissionsDL>();
					;

			// AddIdentity: for server-side razor pages.
			// AddIDentityCore: same as add Identity without server side razor pages.
			

			// We Want to Query the users and their roles at the same time
			//builder = new IdentityBuilder(builder.UserType, typeof(Role), builder.Services);
			//// Since we're using AddIdentityCore. We will have to add/configure this manually =>
			//builder.AddEntityFrameworkStores<HotChanContext>();
			//builder.AddRoleValidator<RoleValidator<Role>>();
			//builder.AddRoleManager<RoleManager<Role>>();
			//builder.AddSignInManager<SignInManager<User>>();
			//builder.AddUserManager<UserManager<User>>();

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

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapGraphQL();
			});

			//app.UseCors();
			//app.UseAuthentication();
			//app.UseAuthorization();
		}
	}
}