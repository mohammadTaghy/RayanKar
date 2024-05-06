using Domain.ReadEntitis;
using Microsoft.AspNet.OData.Query;

namespace Application.IRepositoryRead
{
    public interface ICustomerReadRepository : IRepositoryReadBase<CustomerRead>
    {
        Task<Tuple<List<CustomerRead>, int>> ItemList(ODataQueryOptions<CustomerRead> oDataQuery);
    }
}
