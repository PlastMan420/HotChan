using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
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

// tus file upload stream
using tusdotnet;
using tusdotnet.Interfaces;
using tusdotnet.Models;
using tusdotnet.Models.Configuration;
using tusdotnet.Models.Expiration;
using tusdotnet.Stores;

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
			
			services.AddTransient<Seed>();

		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			//seeder.SeedUsers();
			app.UseHttpsRedirection();

			app.UseStaticFiles();

			app.UseAuthorization();

			app.UseCors("CorsPolicy");

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});

			app.UseRouting();

			// tus file upload streaming
			app.UseTus(httpContext => Task.FromResult(httpContext.RequestServices.GetService<DefaultTusConfiguration>()));

			// All GET requests to tusdotnet are forwarded so that you can handle file downloads.
			// This is done because the file's metadata is domain specific and thus cannot be handled 
			// in a generic way by tusdotnet.
			app.UseEndpoints(endpoints => endpoints.MapGet("/files/{fileId}", DownloadFileEndpoint.HandleRoute));
		}

		private DefaultTusConfiguration CreateTusConfiguration(IServiceProvider serviceProvider)
		{
			var env = (IWebHostEnvironment)serviceProvider.GetRequiredService(typeof(IWebHostEnvironment));

			//File upload path
			var tusFiles = @"C:\tusfiles\";

			return new DefaultTusConfiguration
			{
				UrlPath = "/files",
				//File storage path
				Store = new TusDiskStore(tusFilesو  deletePartialFilesOnConcat: true),
				//Does metadata allow null values
				MetadataParsingStrategy = MetadataParsingStrategy.AllowEmptyValues,
				//The file will not be updated after expiration
				Expiration = new AbsoluteExpiration(TimeSpan.FromMinutes(5)),
				//Event handling (various events, meet your needs)
				Events = new Events
				{
					//Upload completion event callback
					OnFileCompleteAsync = async ctx =>
					{
						// The OnBeforeCreate event is fired just before a file is created.
						OnBeforeCreateAsync = ctx =>
								{
									if (!ctx.Metadata.ContainsKey("name"))
									{
										ctx.FailRequest("name metadata must be specified. ");
									}

									if (!ctx.Metadata.ContainsKey("contentType"))
									{
										ctx.FailRequest("contentType metadata must be specified. ");
									}

									return Task.CompletedTask;
								}

						//Get upload file
						ITusFile file = await ctx.GetFileAsync();

						//Get upload file元数据
						var metadatas = await file.GetMetadataAsync(ctx.CancellationToken);

						//Get the target file name in the above file metadata
						var fileNameMetadata = metadatas["name"];

						//The target file name is encoded in Base64, so it needs to be decoded here
						var fileName = fileNameMetadata.GetString(Encoding.UTF8);

						var extensionName = Path.GetExtension(fileName);

						//Convert the uploaded file to the actual target file
						File.Move(Path.Combine(tusFiles, ctx.FileId), Path.Combine(tusFiles, $"{ctx.FileId}{extensionName}"));
					}
				}
			};
		}
	}
}
