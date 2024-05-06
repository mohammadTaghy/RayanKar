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

            CustomerWrite customer = await FillCustomerFromRequest(request);

           await  CheckCustomerIsDuplicate(customer);

            CustomerRead CustomerRead = await UpdateCustomer(customer);

            return new CommandResponse<CustomerRead>(
                HttpStatusCode.OK,
                string.Format(CommonMessage.InsertedSucceed, nameof(Customer)),
                 CustomerRead);

        }

        private void CheckRequestIsNull(UpdateCustomerCommand request)
        {
            if (request == null) throw new ArgumentNullException(typeof(Customer).Name, string.Format(CommonMessage.NullException, nameof(Customer)));
        }

        private async Task<CustomerWrite> FillCustomerFromRequest(UpdateCustomerCommand request)
        {
            CustomerWrite? customer = await _writeRepo.Find(request.Id);

            if (customer == null) throw new ValidationException(String.Format(CommonMessage.NotFound, nameof(Customer), $"{nameof(Customer.Id)}={request.Id}"));

            customer.DateOfBirth = request.DateOfBirth;
            customer.Firstname = request.Firstname;
            customer.LastName = request.LastName;
            customer.PhoneNumber = new Domain.ValueObject.PhoneNumber(request.PhoneNumber);
            customer.BankAccountNumber = new Domain.ValueObject.BankAccountNumber(request.BankAccountNumber);
            customer.Email = request.Email;

            return customer;
        }
        private async Task CheckCustomerIsDuplicate(CustomerWrite customer)
        {
            Tuple<bool, string> existUser = await _writeRepo.IsExsists(customer);
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
