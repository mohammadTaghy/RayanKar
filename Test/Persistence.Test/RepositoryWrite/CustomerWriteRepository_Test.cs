﻿using Application.Common;
using Domain.WriteEntities;
using Moq;
using Persistence.RepositoryWrite;
using Persistence.Test.RepositoryWrite.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Test.RepositoryWrite
{
    public class CustomerWriteRepository_Test : RepositoryWriteBase_Test<CustomerWrite, CustomerWriteRepository>, IDisposable
    {
        public CustomerWriteRepository_Test() :
            base(new CustomerWriteRepository(_DbContext, new Mock<IRabbitMQUtility>().Object),
                new CustomerWrite(
                                        "Mohammad",
                                        "taghy@gmail.com",
                                        "Yami",
                                        DateTime.Now.AddYears(-1),
                                        "+989384563280",
                                        "IR830120010000001387998021"
                                    )
                                   
                )
        {

        }

        public void Dispose()
        {
            foreach (CustomerWrite customer_Write in _DbContext.Customer.ToList())
            {
                _DbContext.Remove(customer_Write);
                _DbContext.SaveChanges();
            }
        }
    }
}
