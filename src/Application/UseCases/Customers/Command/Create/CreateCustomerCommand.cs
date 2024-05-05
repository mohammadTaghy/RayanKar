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
    public class CreateCustomerCommand: IRequest<CommandResponse<int>>, IMapFrom<CustomerWrite>
    {
        public string Firstname { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string PhoneNumber { get; set; }
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        [RegularExpression(@"^[^@\s]+@[^@\s]+\.(com|net|org|gov)$", ErrorMessage = "Invalid pattern.")]
        public string Email { get; set; }
        public string BankAccountNumber { get; set; }

        public CreateCustomerCommand():this("","",DateTime.Now,"","","")
        {
            
        }
        public CreateCustomerCommand(string firstname, string lastName, DateTime dateOfBirth, string phoneNumber, string email, string bankAccountNumber)
        {
            Firstname = firstname;
            LastName = lastName;
            DateOfBirth = dateOfBirth;
            PhoneNumber = phoneNumber;
            Email = email;
            BankAccountNumber = bankAccountNumber;
        }
        public void Mapping(MappingProfile profile)
        {
            profile.CreateMap<CreateCustomerCommand, CustomerWrite>()
                .ForMember(p => p.Id, q => q.MapFrom(s => int.MinValue))
                .ForMember(p=>p.PhoneNumber,q=>q.MapFrom(s=>new Domain.ValueObject.PhoneNumber(s.PhoneNumber)))
                .ForMember(p=>p.BankAccountNumber,q=>q.MapFrom(s=>new Domain.ValueObject.BankAccountNumber(s.BankAccountNumber)));
        }

    }
}
