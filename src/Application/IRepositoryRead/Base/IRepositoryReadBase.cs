using Domain;
using Domain.ReadEntitis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.IRepositoryRead
{
    public interface IRepositoryReadBase<T> where T : class, IEntity
    {
        Task<T?> FindOne(int id);
    }
}
