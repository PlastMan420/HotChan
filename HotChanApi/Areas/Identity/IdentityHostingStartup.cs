using System;
using HotChanApi.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: HostingStartup(typeof(HotChanApi.Areas.Identity.IdentityHostingStartup))]
namespace HotChanApi.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<HotChanApiContext>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("HotChanApiUserConnection")));

                services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                    .AddEntityFrameworkStores<HotChanApiContext>()
                    .AddDefaultTokenProviders();
                ;
            });
        }
    }
}