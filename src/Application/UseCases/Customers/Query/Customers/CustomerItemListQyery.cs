using Application.Mapping;
using Domain.ReadEntitis;
using MediatR;
using Microsoft.AspNet.OData.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.Customers.Query.Customers
{
    public class CustomerItemListQyery : IRequest<QueryResponse<List<CustomerDto>>>
    {
        public ODataQueryOptions<CustomerRead> ODataQuery { get; set; }

        public CustomerItemListQyery(ODataQueryOptions<CustomerRead> oDataQuery)
        {
            ODataQuery = oDataQuery;
        }
        public void Mapping(MappingProfile profile)
        {
            profile.CreateMap<CustomerRead, CustomerDto>()
                .ForMember(p => p.PhoneNumber, s => s.MapFrom(c => c.PhoneNumber.ToString()))
                .ForMember(p => p.BankAccountNumber, s => s.MapFrom(c => c.BankAccountNumber.ToString()));
        }
    }
}
