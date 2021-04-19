using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

// API internal classes
using HotChanApi.Data;
using HotChanApi.Endpoints;
using HotChanApi.Services;


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
			
			// Enable this later =>
			//services.AddSingleton<IUriService>(o =>
			//{
			//	var accessor = o.GetRequiredService<IHttpContextAccessor>();
			//	var request = accessor.HttpContext.Request;
			//	var uri = string.Concat(request.Scheme, "://", request.Host.ToUriComponent());
			//	return new UriService(uri);
			//});
			
			services.AddControllers();

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
			
			//services.AddRazorPages();

			services.AddDbContext<DataContext>(options =>
				options.UseSqlServer(Configuration.GetConnectionString("hotchandatabase")));
			
			//services.AddScoped<IThreadBox, ThreadBox>();
			services.AddTransient<Seed>();

		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env, Seed seeder)
		{
			app.UseResponseCompression();

			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			//seeder.SeedUsers();
			app.UseStaticFiles();

			app.UseRouting();

			app.UseGrpcWeb();

			app.UseHttpsRedirection();

			//seeder.SeedUsers();

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
