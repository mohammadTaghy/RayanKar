using Application.Mapping;
using Domain.ReadEntitis;
using MediatR;
using SharedProject.Customer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.Customers.Query
{
    public class CustomerItemQyery : IRequest<CustomerDto>, IMapFrom<CustomerDto>
    {
        public int Id { get; set; }
        public void Mapping(MappingProfile profile)
        {
            profile.CreateMap<CustomerRead, CustomerDto>()
                .ForMember(p=>p.PhoneNumber, s=>s.MapFrom(c=>c.PhoneNumber.ToString()))
                .ForMember(p=>p.BankAccountNumber, s=>s.MapFrom(c=>c.BankAccountNumber.ToString()));
        }
    }
}