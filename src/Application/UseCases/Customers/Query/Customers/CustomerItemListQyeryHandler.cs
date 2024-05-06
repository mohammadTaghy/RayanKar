using Application.IRepositoryRead;
using Application.IRepositoryWrite;
using AutoMapper;
using Common;
using Domain.Entities;
using Domain.ReadEntitis;
using Domain.WriteEntities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.Customers.Query.Customers
{
    public class CustomerItemListQyeryHandler : IRequestHandler<CustomerItemListQyery, QueryResponse<List<CustomerDto>>>
    {
        private readonly ICustomerReadRepository customerReadRepo;
        private readonly IMapper mapper;
        public CustomerItemListQyeryHandler(ICustomerReadRepository customerReadRepo, IMapper mapper) 
        {
            this.customerReadRepo = customerReadRepo;
            this.mapper = mapper;
        }
        public async Task<QueryResponse<List<CustomerDto>>> Handle(CustomerItemListQyery request, CancellationToken cancellationToken)
        {
            CheckRequestIsNull(request);

            Tuple<List<CustomerRead>, int> result = await customerReadRepo.ItemList(request.ODataQuery);

            List<CustomerDto> customerDtos = mapper.Map<List<CustomerDto>>(result.Item1);

            return new QueryResponse<List<CustomerDto>>(customerDtos, result.Item2, true, "");
        }

        private static void CheckRequestIsNull(CustomerItemListQyery request)
        {
            if (request == null) throw new ArgumentNullException(typeof(Customer).Name, string.Format(CommonMessage.NullException, nameof(Customer)));
        }
    }
}
