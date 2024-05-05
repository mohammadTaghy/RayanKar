using Domain;
using Domain.WriteEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.IRepositoryWrite.Base
{
    public interface IRepositoryWriteBase<T> where T : class, IEntity
    {
        Task Insert(CustomerWrite customer);

    }
}
