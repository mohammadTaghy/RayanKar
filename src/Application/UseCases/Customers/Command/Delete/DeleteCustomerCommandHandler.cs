using Application.IRepositoryWrite;
using Application.UseCases.Customers.Command.Uodate;
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

namespace Application.UseCases.Customers.Command.Delete
{
    public class DeleteCustomerCommandHandler : BaseCommandHandler<
        DeleteCustomerCommand, CommandResponse<bool>, ICustomerWriteRepository>
    {
        public DeleteCustomerCommandHandler(ICustomerWriteRepository repo, IMapper mapper) : 
            base(repo, mapper)
        {
        }

        public override async Task<CommandResponse<bool>> Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
        {
            CheckRequestIsNull(request);

            CustomerWrite customer = LoadCustomer(request);

            await _writeRepo.DeleteItem(customer);

            return new CommandResponse<bool>(
               HttpStatusCode.OK,
               string.Format(CommonMessage.DeletedSucceed, nameof(Customer)),
                true);
        }
        private void CheckRequestIsNull(DeleteCustomerCommand request)
        {
            if (request == null) throw new ArgumentNullException(typeof(Customer).Name, string.Format(CommonMessage.NullException, nameof(Customer)));
        }
        private CustomerWrite LoadCustomer(DeleteCustomerCommand request)
        {
            CustomerWrite? customer= _writeRepo.Find(p => p.Id == request.Id || p.Email == request.Email);

            if (customer == null) throw new ValidationException(String.Format(CommonMessage.NotFound, nameof(Customer), $"{nameof(Customer.Id)}={request.Id}"));

            return customer;
        }
    }
}
