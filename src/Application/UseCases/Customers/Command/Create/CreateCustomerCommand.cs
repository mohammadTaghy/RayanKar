using Application.Mapping;
using Domain.WriteEntities;
using MediatR;
using PhoneNumbers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.Customers.Command.Create
{
    public class CreateCustomerCommand : BaseCustomerCommand, IRequest<CommandResponse<int>>, IMapFrom<CustomerWrite>
    {
        public CreateCustomerCommand() : this("", "", DateTime.Now, "", "", "")
        {

        }
        public CreateCustomerCommand(
            string firstname, 
            string lastName, 
            DateTime dateOfBirth, 
            string phoneNumber, 
            string email, 
            string bankAccountNumber):
            base(firstname, lastName, dateOfBirth, phoneNumber, email, bankAccountNumber)
        {
        }
        public void Mapping(MappingProfile profile)
        {
            profile.CreateMap<CreateCustomerCommand, CustomerWrite>()
                .ForMember(p => p.Id, q => q.MapFrom(s => int.MinValue));
        }

    }
}
