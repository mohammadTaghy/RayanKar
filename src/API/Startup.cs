using API.Common;
using Application;
using Asp.Versioning;
using Common;
using Domain;
using Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Persistence;

namespace API
{
    public class Startup
    {
        private IConfiguration Configuration { get; }
        private IWebHostEnvironment Environment { get; }
        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            Configuration = configuration;
            Environment = environment;
        }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddApplication();
            services.AddPersistence(Configuration);
            services.AddInfrastructure();


            services.AddControllers()
                .AddNewtonsoftJson();

            
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });

            services.AddSession();
            services.AddApiVersioning(options =>
            {
                options.DefaultApiVersion = new ApiVersion(1, 0);
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.ReportApiVersions = true;
            })
             .AddApiExplorer(options =>
             {
                 options.GroupNameFormat = "'v'VVV";
                 options.SubstituteApiVersionInUrl = true;
             });
            services.AddMvcCore(options =>
            {
                options.EnableEndpointRouting = false;
            });
            services.AddMvc()
                .AddSessionStateTempDataProvider();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            services.AddMemoryCache();
            services.AddCors(c =>
            {
                c.AddPolicy("AllowOrigin", options =>
                {
                    options.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
                });
            });
            configuration(services);
        }
        private void configuration(IServiceCollection services)
        {

            services.Configure<MongoDatabaseOption>(
               Configuration.GetSection("MongoDatabaseSettings"));
            services.Configure<AppSettings>(
               Configuration.GetSection("AppSettings"));

        }
        public void Configure(IApplicationBuilder app)
        {
            app.UseCustomExceptionHandler();
            app.UseSession();
            app.UseCors();
            if (Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI();
            }


            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();

            });
        }
    }
}
