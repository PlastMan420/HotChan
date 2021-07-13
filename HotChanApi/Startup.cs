using HotChanApi.Data;
//using HotChanApi.Models;
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
using GraphiQl;
using HotChan.DataBase.Models;

namespace HotChanApi
{
	public class Startup
	{
		private string _PostGresConnectionString;
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddAutoMapper(typeof(Startup));
			services.AddHttpContextAccessor();
			//services.AddTransient<Seed>();

			// init gRPC.
			//services.AddGrpc();

			services.AddResponseCompression(opts =>
			{
				opts.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(
					new[] { "application/octet-stream" });
			});

			services.AddMvcCore();

			//services.AddSingleton<IDocumentExecuter, DocumentExecuter>();
			//services.AddTransient<IPostRepo, PostRepo>();
			//services.AddSingleton<PostQuery>();
			//services.AddSingleton<PostViewType>();

			//// schema time
			////var sp = services.BuildServiceProvider();
			//services.AddSingleton<ISchema, PostViewSchema>();

			//services.AddSingleton<ISchema, PostViewSchema>(services => new PostViewSchema(new SelfActivatingServiceProvider(services)));

			services.AddGraphQLServer()
					.AddQueryType<PostRepo>();

			// Add JWT Tokens. Also authentication must be Added inorder to get services.AddIdentityCore<>() to work.
			services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
				options.TokenValidationParameters = new TokenValidationParameters
				{
					ValidateIssuerSigningKey = true,
					IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Configuration.GetSection("AppSettings:Token").Value)),
					// no validation rn since we will be just using localhost -->
					ValidateIssuer = false,
					ValidateAudience = false
				});

			// DbContext and Postgres connection string.
			// Connection String is in the secrets storage.
			_PostGresConnectionString = Configuration["ConnectionStrings:hotchandatabase-postgres-dev"];
			services.AddDbContext<DataContext>(options =>
				options.UseNpgsql(Configuration.GetConnectionString("hotchandatabase-postgres-dev")));

			// AddIdentity: for server-side razor pages.
			// AddIDentityCore: same as add Identity without server side razor pages.
			IdentityBuilder builder = services.AddIdentityCore<User>();

			// We Want to Query the users and their roles at the same time
			builder = new IdentityBuilder(builder.UserType, typeof(Role), builder.Services);
			// Since we're using AddIdentityCore. We will have to add/configure this manually =>
			builder.AddEntityFrameworkStores<DataContext>();
			builder.AddRoleValidator<RoleValidator<Role>>();
			builder.AddRoleManager<RoleManager<Role>>();
			builder.AddSignInManager<SignInManager<User>>();
			builder.AddUserManager<UserManager<User>>();


			//services.AddScoped<IThreadBox, ThreadBox>();
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env, Seed seeder)
		{
			app.UseResponseCompression();

			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();

			}

			app.UseStaticFiles();

			// Use with dotnet run =>
			//seeder.SeedUsers();

			app.UseRouting()
				.UseEndpoints(endpoints =>
				{
					endpoints.MapGraphQL();
				});

			app.UseHttpsRedirection();

			app.UseCors();

			app.UseGraphiQl();
		}
	}
}