using Application.Common;
using Application.IRepositoryRead;
using Common;
using Domain.Entities;
using Domain.ReadEntitis;
using Microsoft.Extensions.Options;
using Persistence.RepositoryRead.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.RepositoryRead
{
    public class CustomerReadRepository : RepositoryReadBase<CustomerRead>, ICustomerReadRepository
    {
        public CustomerReadRepository(IOptions<MongoDatabaseOption> databaseSettings, IRabbitMQUtility rabbitMQUtility) : 
            base(databaseSettings, rabbitMQUtility)
        {
        }

        protected override string QueueName => nameof(Customer);
    }
}
