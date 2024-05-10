using Application.Common;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.BDDTest.Common
{
    public class CustomWebApplicationFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
    {

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder
                .ConfigureServices(services =>
                {
                    var serviceProvider = new ServiceCollection()
                                .AddEntityFrameworkInMemoryDatabase()
                                .BuildServiceProvider();

                    services.AddDbContext<PersistanceDBContext>(options =>
                    {
                        options.UseInMemoryDatabase("MyInMemoryDB");
                        options.UseInternalServiceProvider(serviceProvider);
                    });
                    services.AddScoped<IPersistanceDBContext>(provider => provider.GetService<PersistanceDBContext>());

                    services.AddOptions();

                    var sp = services.BuildServiceProvider();

                    using var scope = sp.CreateScope();
                    var scopedServices = scope.ServiceProvider;
                    var context = scopedServices.GetRequiredService<PersistanceDBContext>();
                    context.Database.EnsureCreated();

                })
                    .UseEnvironment("Test");
        }
    }
}
