using Domain.Entities;
using Domain.WriteEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Configuration
{
    public class CustomerConfiguration : IEntityTypeConfiguration<CustomerWrite>
    {
        public void Configure(EntityTypeBuilder<CustomerWrite> builder)
        {
            builder.ToTable(nameof(Customer));
            builder.Property(p => p.Id).ValueGeneratedNever();
            builder.Property(p => p.Firstname).HasMaxLength(128);
            builder.Property(p => p.LastName).HasMaxLength(128);
            builder.OwnsOne(p => p.PhoneNumber).Property(p=>p.Phone).HasMaxLength(17).IsUnicode(false);
            builder.OwnsOne(p => p.BankAccountNumber).Property(p => p.BankAccount).HasMaxLength(16).IsUnicode(false); ;
            //builder.Property(p => p.BankAccountNumber).HasMaxLength(16).IsUnicode(false);
            builder.Property(p => p.Email).HasMaxLength(512);
            builder.HasIndex(p => p.Email)
                .IsUnique();
            builder.HasIndex(p => new { p.Firstname, p.LastName, p.DateOfBirth })
                .IsUnique();
        }
    }
}
