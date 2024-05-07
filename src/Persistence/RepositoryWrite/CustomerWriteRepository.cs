using Application.Common;
using Application.IRepositoryWrite;
using Application.Model;
using Common;
using Domain.Entities;
using Domain.ReadEntitis;
using Domain.WriteEntities;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Persistence.RepositoryWrite.Base;
using System.Linq;

namespace Persistence.RepositoryWrite
{
    public class CustomerWriteRepository : RepositoryWriteBase<CustomerWrite>, ICustomerWriteRepository
    {
        public CustomerWriteRepository(PersistanceDBContext context, IRabbitMQUtility rabbitMQUtility) : base(context, rabbitMQUtility)
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
        protected override void SendMessage(CustomerWrite? aggregateEntity, byte changedtype)
        {
            rabbitMQUtility.SendMessage(
                new Application.Model.RabbitMQSendRequest(
                    QueueName,
                    QueueName,
                    JsonConvert.SerializeObject(
                        new RabbitMQMessageModel("",
                        JsonConvert.SerializeObject(
                                new CustomerRead(
                                aggregateEntity.Firstname,
                                aggregateEntity.Email,
                                aggregateEntity.LastName,
                                aggregateEntity.DateOfBirth,
                                aggregateEntity.Id,
                                aggregateEntity.PhoneNumber,
                                aggregateEntity.BankAccountNumber)),
                        aggregateEntity.Id,
                        changedtype)
                        )
                        )
                    );
        }
    }
}
