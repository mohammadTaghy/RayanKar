using Application.IRepositoryWrite.Base;
using Domain.WriteEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Application.IRepositoryWrite
{
    public interface ICustomerWriteRepository : IRepositoryWriteBase<CustomerWrite>
    {
        Task<Tuple<bool, string>> IsExsists(CustomerWrite customer);
    }
}
