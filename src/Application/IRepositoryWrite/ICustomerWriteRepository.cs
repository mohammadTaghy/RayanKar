using Application.IRepositoryWrite.Base;
using Domain.WriteEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.IRepositoryWrite
{
    public interface ICustomerWriteRepository : IRepositoryWriteBase<CustomerWrite>
    {
        CustomerWrite? Find(int id);
        Tuple<bool, string> IsExsists(CustomerWrite customer);
        Task Update(CustomerWrite customer);
    }
}
