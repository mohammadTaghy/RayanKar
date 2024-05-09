using ClientBlazor.Service;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

namespace ClientBlazor
{
    public class Program
    {

        public Program(IConfiguration configuration)
        {
        }

        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");
            builder.RootComponents.Add<HeadOutlet>("head::after");
            builder.Services.AddScoped(sp => new HttpClient
            {
                BaseAddress = new Uri(builder.Configuration["BaseUrl"])
            });
           
            builder.Services.AddScoped<ICustomerService, CustomerService>();

            await builder.Build().RunAsync();
        }
    }
}
