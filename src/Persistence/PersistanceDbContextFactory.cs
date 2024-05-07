using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence
{
    internal class PersistanceDbContextFactory : DesignTimeDbContextFactoryBase<PersistanceDBContext>
    {
        protected override PersistanceDBContext CreateNewInstance(DbContextOptions<PersistanceDBContext> options)
        {
            return new PersistanceDBContext(options);
        }
    }
}
