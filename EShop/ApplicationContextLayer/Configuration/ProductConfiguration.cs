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
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder
                .Property(e => e.Id)
                .ValueGeneratedOnAdd();

            builder
                .HasOne(product => product.Branch)
                .WithMany(branch => branch.Products)
                .HasForeignKey(product => product.BranchId);

            builder
                .HasOne(product => product.Category)
                .WithMany(category => category.Products)
                .HasForeignKey(product => product.CategoryId);

            builder
                .HasData(
                    new Product()
                    {
                        Id = Guid.Parse("cc234dc4-9833-49de-8e17-4d2794ccf444"),
                        BranchId = Guid.Parse("5b10514f-33ca-41d2-b6df-95fc1fd95e5a"),//apple
                        CategoryId = Guid.Parse("61cff61b-a84a-4002-b8dc-029cafc60408"),//smart phone
                        Description = "This is a evoluntion in 2024 of smart phone ever",
                        DiscountPercentage = 5,
                        Images = "https://cdn.dummyjson.com/product-images/2/1.jpg,https://cdn.dummyjson.com/product-images/2/2.jpg,https://cdn.dummyjson.com/product-images/2/3.jpg",
                        Price = 1500,
                        Stock = 200,
                        Rating = 4.5f,
                        Thumbnail = "https://cdn.dummyjson.com/product-images/2/thumbnail.jpg",
                        Title = "Iphone X",
                    },
                    new Product()
                    {
                        Id = Guid.Parse("bbbcbd00-425b-47f5-83bf-1b4f83df5f37"),
                        BranchId = Guid.Parse("d0426967-668d-4e34-a139-8b86c1defb46"),//samsung
                        CategoryId = Guid.Parse("61cff61b-a84a-4002-b8dc-029cafc60408"),//smart phone
                        Description = "This is a best smart phone for bussiness in next 100 years",
                        DiscountPercentage = 10,
                        Images = "https://cdn.dummyjson.com/product-images/3/1.jpg,https://cdn.dummyjson.com/product-images/2/3.jpg",
                        Price = 1000,
                        Stock = 400,
                        Rating = 4.7f,
                        Thumbnail = "https://cdn.dummyjson.com/product-images/3/thumbnail.jpg",
                        Title = "Samsung Universe 9",
                    },
                    new Product()
                    {
                        Id = Guid.Parse("687832a9-e35c-4c31-b7a0-555810f670e1"),
                        BranchId = Guid.Parse("5b10514f-33ca-41d2-b6df-95fc1fd95e5a"),//apple
                        CategoryId = Guid.Parse("d1f44292-46b0-4ca3-a38a-020ce138c88b"),//laptop
                        Description = "MacBook Pro 2021 with mini-LED display may launch between September, November",
                        DiscountPercentage = 15,
                        Images = "https://cdn.dummyjson.com/product-images/6/1.png,https://cdn.dummyjson.com/product-images/6/2.jpg",
                        Price = 2500,
                        Stock = 300,
                        Rating = 4.3f,
                        Thumbnail = "https://cdn.dummyjson.com/product-images/6/thumbnail.png",
                        Title = "MacBook Pro",
                    },
                    new Product()
                    {
                        Id = Guid.Parse("e8639e69-430e-4275-b1d5-015e73358f1b"),
                        BranchId = Guid.Parse("d0426967-668d-4e34-a139-8b86c1defb46"),//samsung
                        CategoryId = Guid.Parse("d1f44292-46b0-4ca3-a38a-020ce138c88b"),//laptop
                        Description = "Samsung Galaxy Book S (2020) Laptop With Intel Lakefield Chip, 8GB of RAM Launched",
                        DiscountPercentage = 20,
                        Images = "https://cdn.dummyjson.com/product-images/7/1.jpg,https://cdn.dummyjson.com/product-images/7/2.jpg",
                        Price = 1499,
                        Stock = 100,
                        Rating = 5f,
                        Thumbnail = "https://cdn.dummyjson.com/product-images/7/thumbnail.jpg",
                        Title = "Samsung Galaxy Book",
                    }
                );
        }
    }
}
