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
        Task Insert(CustomerWrite customer);
        Task Update(CustomerWrite customer);
        Task DeleteItem(CustomerWrite customer);

        CustomerWrite? Find(int id);
        T? Find(Expression<Func<T, bool>> expression);

    }
}
