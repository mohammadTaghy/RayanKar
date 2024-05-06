using Application.Common;
using Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NetCore.AutoRegisterDi;

namespace Persistence
{
    public static class DependencyInjection
    {
        public static void AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddDbContext<PersistanceDBContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("MainConnectionString")));

            services.AddScoped<IPersistanceDBContext>(provider => provider.GetService<PersistanceDBContext>());

            services.RegisterAssemblyPublicNonGenericClasses()
              .Where(c => c.Name.EndsWith("Repository"))  //optional
              .AsPublicImplementedInterfaces(ServiceLifetime.Scoped);



            services.Configure<DatabaseSettings>(option =>
            {
                option.ConnectionString = configuration.GetSection("DatabaseSettings:ConnectionString").Value;
                option.DatabaseName = configuration.GetSection("DatabaseSettings:DatabaseName").Value;
            });


        }
    }
}
