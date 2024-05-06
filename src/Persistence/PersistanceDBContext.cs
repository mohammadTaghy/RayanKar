﻿using Domain.WriteEntities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence
{
    public class PersistanceDBContext : DbContext
    {
        public PersistanceDBContext(DbContextOptions<PersistanceDBContext> options) : base(options)
        {

        }
        public DbSet<CustomerWrite> Customer { get; set; }
       
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("case_insensitive_collation");
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(PersistanceDBContext).Assembly);


        }
    }
}

