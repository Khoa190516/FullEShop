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
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder
                .Property(e => e.Id)
                .ValueGeneratedOnAdd();

            builder
                .HasData(
                    new Category()
                    {
                        Id = Guid.Parse("61cff61b-a84a-4002-b8dc-029cafc60408"),
                        CategoryName = "Smart Phone"
                    },
                    new Category()
                    {
                        Id = Guid.Parse("d1f44292-46b0-4ca3-a38a-020ce138c88b"),
                        CategoryName = "Laptop"
                    }
                );
        }
    }
}
