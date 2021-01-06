using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;

using HotChanApi.Data;
using HotChanApi.Services;

using AutoMapper;

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
			services.AddCors(OptionsBuilderConfigurationExtensions =>
			{
				OptionsBuilderConfigurationExtensions.AddPolicy("CorsPolicy", builder =>
				builder.AllowAnyOrigin()
				.AllowAnyMethod()
				.AllowAnyHeader());
			});
			services.AddAutoMapper(typeof(Startup));
			services.AddHttpContextAccessor();
			services.AddSingleton<IUriService>(o =>
			{
				var accessor = o.GetRequiredService<IHttpContextAccessor>();
				var request = accessor.HttpContext.Request;
				var uri = string.Concat(request.Scheme, "://", request.Host.ToUriComponent());
				return new UriService(uri);
			});
			services.AddControllers();
			services.AddDbContext<DataContext>(options =>
				options.UseSqlServer(Configuration.GetConnectionString("hotchandatabase")));

			services.AddScoped<IThreadBox, ThreadBox>();
			

		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			app.UseHttpsRedirection();

			app.UseRouting();

			app.UseStaticFiles();

			app.UseAuthorization();

			app.UseCors("CorsPolicy");

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});
		}
	}
}
