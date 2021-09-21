using System;
using System.Collections.Generic;
using System.Text;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EntityFramework.Configurations
{
    public class StudentConfig: IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> entity)
        {
            entity.HasOne<Address>(p => p.Address)
                .WithOne()
                .HasForeignKey<Student>(p => p.AddressId);
        }
    }
}
