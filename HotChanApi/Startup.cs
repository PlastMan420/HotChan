using HotChanApi.Data;
using HotChanApi.Models;
using HotChanApi.Services;
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
			services.AddAutoMapper(typeof(Startup));
			services.AddHttpContextAccessor();
			services.AddTransient<Seed>();

			// add gRPC service
			services.AddGrpc();
			services.AddResponseCompression(opts =>
			{
				opts.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(
					new[] { "application/octet-stream" });
			});
			services.AddCors(o => o.AddPolicy("AllowAll", builder =>
			{
				builder.AllowAnyOrigin()
					   .AllowAnyMethod()
					   .AllowAnyHeader()
					   .WithExposedHeaders("Grpc-Status", "Grpc-Message", "Grpc-Encoding", "Grpc-Accept-Encoding");
			}));

			// Connection String is in the secrets storage.
			string PostGresConnectionString = Configuration["ConnectionStrings:hotchandatabase-postgres-dev"];
			services.AddDbContext<DataContext>(options =>
				options.UseNpgsql(Configuration.GetConnectionString(PostGresConnectionString)));

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

			services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options => {
				options.TokenValidationParameters = new TokenValidationParameters
				{
						ValidateIssuerSigningKey = true,
						IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII
						.GetBytes(Configuration.GetSection("AppSettings:Token").Value)),
						ValidateIssuer = false,
						ValidateAudience = false
					};
				});          
			//


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

			app.UseRouting();

			app.UseGrpcWeb();

			app.UseHttpsRedirection();

			app.UseCors();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapGrpcService<PostViewService>().RequireCors("AllowAll").EnableGrpcWeb();
				//endpoints.MapRazorPages();
				//endpoints.MapControllers();
				endpoints.MapFallbackToFile("index.html");
			});
		}
	}
}