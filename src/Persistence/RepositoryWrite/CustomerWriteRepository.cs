using Application.IRepositoryWrite;
using Common;
using Domain.Entities;
using Domain.WriteEntities;
using Persistence.RepositoryWrite.Base;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.RepositoryWrite
{
    public class CustomerWriteRepository : RepositoryWriteBase<CustomerWrite>, ICustomerWriteRepository
    {
        public CustomerWriteRepository(PersistanceDBContext context) : base(context)
        {
        }

        protected override string QueueName => nameof(Customer);

        public async Task<Tuple<bool, string>> IsExsists(CustomerWrite customer)
        {
            string message = string.Empty;
            if (await GetAllAsQueryable().AnyAsync(p => p.Id != customer.Id && p.Email == customer.Email))
                message = String.Format(CommonMessage.IsDuplicateCustomer, nameof(Customer), $"{customer.Email}");
            else if(await GetAllAsQueryable().AnyAsync(p => p.Id != customer.Id && p.Firstname == customer.Firstname && p.LastName == customer.LastName && p.DateOfBirth == customer.DateOfBirth))
                message = String.Format(CommonMessage.IsDuplicateCustomer, nameof(Customer), $"{customer.Firstname},{customer.LastName},{customer.DateOfBirth}");
            return new Tuple<bool, string>(!string.IsNullOrEmpty(message),message);
        }
    }
}
