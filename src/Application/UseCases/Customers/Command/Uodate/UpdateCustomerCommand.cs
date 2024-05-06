using Application.Mapping;
using Application.UseCases.Customers.Command.Create;
using Domain.ReadEntitis;
using Domain.WriteEntities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.Customers.Command.Uodate
{
    public class UpdateCustomerCommand : BaseCustomerCommand, IRequest<CommandResponse<CustomerRead>>, IMapFrom<CustomerWrite>
    {
        public int Id { get; set; }
        public UpdateCustomerCommand() : this("", "", DateTime.Now, "", "", "",0)
        {

        }
        public UpdateCustomerCommand(
            string firstname,
            string lastName,
            DateTime dateOfBirth,
            string phoneNumber,
            string email,
            string bankAccountNumber,
            int id) :
            base(firstname, lastName, dateOfBirth, phoneNumber, email, bankAccountNumber)
        {
            Id = id;
        }
        public void Mapping(MappingProfile profile)
        {
            profile.CreateMap<UpdateCustomerCommand, CustomerWrite>()
                .ForMember(p => p.PhoneNumber, q => q.MapFrom(s => new Domain.ValueObject.PhoneNumber(s.PhoneNumber)))
                .ForMember(p => p.BankAccountNumber, q => q.MapFrom(s => new Domain.ValueObject.BankAccountNumber(s.BankAccountNumber)));
            profile.CreateMap<CustomerWrite, CustomerRead>()
                .ForMember(p => p.PhoneNumber, q => q.MapFrom(s => s.PhoneNumber.ToString()))
                .ForMember(p => p.BankAccountNumber, q => q.MapFrom(s => s.BankAccountNumber.ToString()));
        }
    }
}
