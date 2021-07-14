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
using HotChocolate.AspNetCore.Playground;
using HotChan.DataBase;
using Microsoft.AspNetCore.Authentication;
using HotChan.DataAccess.DataLoader;

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

			services.AddMvcCore();
			services.AddHttpContextAccessor();

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

			services.AddPooledDbContextFactory<HotChanContext>(options =>
				options.UseNpgsql(Configuration.GetConnectionString("hotchandatabase-postgres-dev")));

			services.AddGraphQLServer()
					.AddQueryType<PostQuery>()
					.AddMutationType<PostMutation>()
					.AddDataLoader<PostIdDL>()
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
				app.UsePlayground(new PlaygroundOptions { QueryPath = "/graphql", Path = "/playground" });

			}
			else
			{
				app.UseExceptionHandler("/Error");
				app.UseHsts();
			}

			app.UseHttpsRedirection();
			app.UseStaticFiles();

			app.UseRouting()
				.UseEndpoints(endpoints =>
				{
					endpoints.MapGraphQL();
				});

			//app.UseCors();
			//app.UseAuthentication();
			//app.UseAuthorization();
		}
	}
}