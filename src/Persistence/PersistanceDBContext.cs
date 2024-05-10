using Application.Common;
using Domain.WriteEntities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence
{
    public class PersistanceDBContext : DbContext, IPersistanceDBContext
    {
        public PersistanceDBContext(DbContextOptions<PersistanceDBContext> options) : base(options)
        {

        }
        public DbSet<CustomerWrite> Customer { get; set; }
       
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("SQL_Latin1_General_CP1_CI_AS");
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(PersistanceDBContext).Assembly);


        }
    }
}

