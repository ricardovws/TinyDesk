using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TinyDesk.Areas.Identity.Data;
using TinyDesk.Models;

[assembly: HostingStartup(typeof(TinyDesk.Areas.Identity.IdentityHostingStartup))]
namespace TinyDesk.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<AppIdentityContext>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("AppIdentityContextConnection")));

                services.AddDefaultIdentity<AppIdentityUser>()
                    .AddEntityFrameworkStores<AppIdentityContext>();
            });
        }
    }
}