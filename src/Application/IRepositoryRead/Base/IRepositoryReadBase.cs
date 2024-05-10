using Domain;
using Domain.ReadEntitis;
using Microsoft.AspNetCore.OData.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.IRepositoryRead
{
    public interface IRepositoryReadBase<T> where T : class, IEntity
    {
        #region Manipulate
        Task<bool> Delete(T entity, CancellationToken cancellationToken);
        Task<bool> Delete(int id, CancellationToken cancellationToken);
        Task<bool> Add(T entity, CancellationToken cancellationToken);
        Task<bool> AddMeny(IEnumerable<T> entities, CancellationToken cancellationToken);
        Task<bool> Update(T entity, CancellationToken cancellationToken);
        #endregion
        Task<T?> FindOne(int id);
        Task<Tuple<List<T>, int>> ItemList(ODataQueryOptions<T> oDataQuery);
    }
}
