using Domain;
using Domain.WriteEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Application.IRepositoryWrite.Base
{
    public interface IRepositoryWriteBase<T> where T : class, IEntity
    {
        Task Insert(T entity);
        Task Update(T entity);
        Task DeleteItem(T entity);

        Task<T?> Find(int id);
        Task<T?> Find(Expression<Func<T, bool>> expression);

    }
}
