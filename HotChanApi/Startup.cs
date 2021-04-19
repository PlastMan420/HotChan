using HotChanApi.Data;
using HotChanApi.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Linq;

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

			services.AddDbContext<DataContext>(options =>
				options.UseSqlServer(Configuration.GetConnectionString("hotchandatabase")));

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