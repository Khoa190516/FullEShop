using DomainLayer.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationContextLayer.Configuration
{
    public class BranchConfiguration : IEntityTypeConfiguration<Branch>
    {
        public void Configure(EntityTypeBuilder<Branch> builder)
        {
            builder
                .Property(e => e.Id)
                .ValueGeneratedOnAdd();

            builder
                .HasData(
                    new Branch()
                    {
                        Id = Guid.Parse("5b10514f-33ca-41d2-b6df-95fc1fd95e5a"),
                        BranchName = "Apple",
                    },
                    new Branch()
                    {
                        Id = Guid.Parse("d0426967-668d-4e34-a139-8b86c1defb46"),
                        BranchName = "Samsung",
                    }
                );
        }
    }
}
