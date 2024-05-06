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
using System.Xml.Linq;

namespace Application.UseCases.Customers.Command.Uodate
{
    public class UpdateCustomerCommandHandler : BaseCommandHandler<UpdateCustomerCommand, CommandResponse<CustomerRead>, ICustomerWriteRepository>
    {

        public UpdateCustomerCommandHandler(ICustomerWriteRepository repo, IMapper mapper) : base(repo, mapper)
        {
        }

        /// <exception cref="FormatException"></exception>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ValidationException"></exception>
        /// <exception cref="NotFoundException"></exception>
        public override async Task<CommandResponse<CustomerRead>> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
        {
            CheckRequestIsNull(request);

            CustomerWrite? customer = _writeRepo.Find(request.Id);

            CheckCustomerExist(request, customer);

            customer = _mapper.Map<CustomerWrite>(request);

            CheckCustomerIsDuplicate(customer);

            CustomerRead CustomerRead = await UpdateCustomer(customer);

            return new CommandResponse<CustomerRead>(
                HttpStatusCode.OK,
                string.Format(CommonMessage.InsertedSucceed, nameof(Customer)),
                 CustomerRead);

        }

        private static void CheckRequestIsNull(UpdateCustomerCommand request)
        {
            if (request == null) throw new ArgumentNullException(typeof(Customer).Name, string.Format(CommonMessage.NullException, nameof(Customer)));
        }

        private static void CheckCustomerExist(UpdateCustomerCommand request, CustomerWrite? customer)
        {
            if (customer == null) throw new ValidationException(String.Format(CommonMessage.NotFound, nameof(Customer), $"{nameof(Customer.Id)}={request.Id}"));
        }
        private void CheckCustomerIsDuplicate(CustomerWrite customer)
        {
            Tuple<bool, string> existUser = _writeRepo.IsExsists(customer);
            if (existUser.Item1)
                throw new ValidationException(existUser.Item2);
        }
        private async Task<CustomerRead> UpdateCustomer(CustomerWrite customer)
        {
            await _writeRepo.Update(customer);
            CustomerRead CustomerRead = _mapper.Map<CustomerRead>(customer);
            return CustomerRead;
        }
    }
}
