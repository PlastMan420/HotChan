using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using Grpc.Net.Client;
using Grpc.Net.Client.Web;

using HotChanShared.Models;

namespace HotChanWasm
{
	public class Program
	{
		public static async Task Main(string[] args)
		{
			var builder = WebAssemblyHostBuilder.CreateDefault(args);
			builder.RootComponents.Add<App>("#app");
			
			// gRPC client =>
			builder.Services.AddSingleton(services =>
			{
				#if DEBUG
					var backendurl = "https://localhost:5001"; // local debug url
				#else
					var backendurl = "https://some.external.url:12345"; // production url
				#endif

				// Create a channel with a GrpcWebHandler that is addressed to the backend server.
				//
				// GrpcWebText is used because server streaming requires it. If server streaming is not used in your app
				// then GrpcWeb is recommended because it produces smaller messages.
				var httpHandler = new GrpcWebHandler(GrpcWebMode.GrpcWebText, new HttpClientHandler());


				return GrpcChannel.ForAddress(backendurl, new GrpcChannelOptions { HttpHandler = httpHandler });
				//return new chanpostview.chanpostviewclient(channel);
			});

			await builder.Build().RunAsync();
		}
	}
}
