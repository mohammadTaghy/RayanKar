using Application.IRepositoryWrite;
using AutoMapper;
using Common;
using Domain.Entities;
using Domain.ReadEntitis;
using Domain.WriteEntities;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.Customers.Command.Create
{
    public class CreateCustomerCommandHandler : BaseCommandHandler<CreateCustomerCommand, CommandResponse<int>, ICustomerWriteRepository>
    {
        public CreateCustomerCommandHandler(ICustomerWriteRepository repo, IMapper mapper) : base(repo, mapper)
        {
        }
        public override async Task<CommandResponse<int>> Handle(
            CreateCustomerCommand request,
            CancellationToken cancellationToken)
        {
            CheckIfNull(request);
            CustomerWrite customer = await InsertCustomer(request);

            return new CommandResponse<int>(
                HttpStatusCode.OK,
                string.Format(CommonMessage.InsertedSucceed, nameof(CustomerWrite)),
                 customer.Id);
        }
        private void CheckIfNull(CreateCustomerCommand request)
        {
            if (request is null)
            {
                throw new ArgumentNullException(typeof(Customer).Name, string.Format(CommonMessage.NullException, nameof(Customer)));
            }
        }
        private async Task<CustomerWrite> InsertCustomer(CreateCustomerCommand request)
        {
            CustomerWrite customer = _mapper.Map<CustomerWrite>(request);
            CheckCustomerIsDuplicate(customer);
            await _writeRepo.Insert(customer);
            return customer;
        }

        private void CheckCustomerIsDuplicate(CustomerWrite customer)
        {
            Tuple<bool, string> existUser = _writeRepo.IsExsists(customer);
            if (existUser.Item1)
                throw new ValidationException(existUser.Item2);
        }

    }
}
