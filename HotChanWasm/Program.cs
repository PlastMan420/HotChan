using System;
using System.Net.Http;
using HotChanWasm;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");

builder.Services.AddScoped(sp => 
	new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services
.AddHotChanWasmClient()
.ConfigureHttpClient(client => client.BaseAddress = new Uri("https://localhost:5001/graphql"));


await builder.Build().RunAsync();
