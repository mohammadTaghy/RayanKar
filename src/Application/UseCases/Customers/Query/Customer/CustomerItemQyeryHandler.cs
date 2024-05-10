using Application.IRepositoryRead;
using Application.UseCases.Customers.Command.Create;
using Application.UseCases.Customers.Command.Delete;
using AutoMapper;
using Common;
using Domain.Entities;
using Domain.ReadEntitis;
using Domain.WriteEntities;
using MediatR;
using SharedProject;
using SharedProject.Customer;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.Customers.Query
{
    public class CustomerItemQyeryHandler : IRequestHandler<CustomerItemQyery, CustomerDto> 
    {
        private readonly ICustomerReadRepository customerReadRepo;
        private readonly IMapper mapper;

        public CustomerItemQyeryHandler(ICustomerReadRepository customerReadRepo, IMapper mapper)
        {
            this.customerReadRepo = customerReadRepo;
            this.mapper = mapper;
        }

        public async Task<CustomerDto> Handle(CustomerItemQyery request, CancellationToken cancellationToken)
        {
            CheckIfNull(request);

            CustomerRead customer = await LoadCustomer(request);

            return mapper.Map<CustomerDto>(customer);
        }
        private void CheckIfNull(CustomerItemQyery request)
        {
            if (request is null)
            {
                throw new ArgumentNullException(typeof(Customer).Name, string.Format(CommonMessage.NullException, nameof(Customer)));
            }
        }
        private async Task<CustomerRead> LoadCustomer(CustomerItemQyery request)
        {
            CustomerRead? customer = await customerReadRepo.FindOne(request.Id);

            if (customer == null) throw new ValidationException(String.Format(CommonMessage.NotFound, nameof(Customer), $"{nameof(Customer.Id)}={request.Id}"));

            return customer;
        }
    }
}
