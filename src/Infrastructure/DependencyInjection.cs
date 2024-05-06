using Application.Common;
using Infrastructure.RabbitMQ;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static void AddInfrastructure(this IServiceCollection services)
        {
            services.AddSingleton<IRabbitMQUtility, RabbitMQUtility>();
        }

    }
}
