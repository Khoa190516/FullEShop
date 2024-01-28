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
    public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder
               .Property(e => e.Id)
               .ValueGeneratedOnAdd();

            builder.HasOne(customer => customer.Cart)
                .WithOne(cart => cart.Customer)
                .HasForeignKey<Customer>(customer => customer.CartId);
        }
    }
}
