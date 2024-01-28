using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ApplicationContextLayer.Migrations
{
    /// <inheritdoc />
    public partial class seedDataForTamLap : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Branches",
                columns: new[] { "Id", "BranchName" },
                values: new object[,]
                {
                    { new Guid("5b10514f-33ca-41d2-b6df-95fc1fd95e5a"), "Apple" },
                    { new Guid("d0426967-668d-4e34-a139-8b86c1defb46"), "Samsung" }
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CategoryName" },
                values: new object[,]
                {
                    { new Guid("61cff61b-a84a-4002-b8dc-029cafc60408"), "Smart Phone" },
                    { new Guid("d1f44292-46b0-4ca3-a38a-020ce138c88b"), "Laptop" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "BranchId", "CategoryId", "Description", "DiscountPercentage", "Images", "Price", "Stock", "Thumbnail", "Title" },
                values: new object[,]
                {
                    { new Guid("687832a9-e35c-4c31-b7a0-555810f670e1"), new Guid("5b10514f-33ca-41d2-b6df-95fc1fd95e5a"), new Guid("d1f44292-46b0-4ca3-a38a-020ce138c88b"), "MacBook Pro 2021 with mini-LED display may launch between September, November", 15f, "https://cdn.dummyjson.com/product-images/6/1.png,https://cdn.dummyjson.com/product-images/6/2.jpg", 2500m, 300, "https://cdn.dummyjson.com/product-images/6/thumbnail.png", "MacBook Pro" },
                    { new Guid("bbbcbd00-425b-47f5-83bf-1b4f83df5f37"), new Guid("d0426967-668d-4e34-a139-8b86c1defb46"), new Guid("61cff61b-a84a-4002-b8dc-029cafc60408"), "This is a best smart phone for bussiness in next 100 years", 10f, "https://cdn.dummyjson.com/product-images/3/1.jpg,https://cdn.dummyjson.com/product-images/2/3.jpg", 1000m, 400, "https://cdn.dummyjson.com/product-images/3/thumbnail.jpg", "Samsung Universe 9" },
                    { new Guid("cc234dc4-9833-49de-8e17-4d2794ccf444"), new Guid("5b10514f-33ca-41d2-b6df-95fc1fd95e5a"), new Guid("61cff61b-a84a-4002-b8dc-029cafc60408"), "This is a evoluntion in 2024 of smart phone ever", 5f, "https://cdn.dummyjson.com/product-images/2/1.jpg,https://cdn.dummyjson.com/product-images/2/2.jpg,https://cdn.dummyjson.com/product-images/2/3.jpg", 1500m, 200, "https://cdn.dummyjson.com/product-images/2/thumbnail.jpg", "Iphone X" },
                    { new Guid("e8639e69-430e-4275-b1d5-015e73358f1b"), new Guid("d0426967-668d-4e34-a139-8b86c1defb46"), new Guid("d1f44292-46b0-4ca3-a38a-020ce138c88b"), "Samsung Galaxy Book S (2020) Laptop With Intel Lakefield Chip, 8GB of RAM Launched", 20f, "https://cdn.dummyjson.com/product-images/7/1.jpg,https://cdn.dummyjson.com/product-images/7/2.jpg", 1499m, 100, "https://cdn.dummyjson.com/product-images/7/thumbnail.jpg", "Samsung Galaxy Book" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("687832a9-e35c-4c31-b7a0-555810f670e1"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("bbbcbd00-425b-47f5-83bf-1b4f83df5f37"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("cc234dc4-9833-49de-8e17-4d2794ccf444"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("e8639e69-430e-4275-b1d5-015e73358f1b"));

            migrationBuilder.DeleteData(
                table: "Branches",
                keyColumn: "Id",
                keyValue: new Guid("5b10514f-33ca-41d2-b6df-95fc1fd95e5a"));

            migrationBuilder.DeleteData(
                table: "Branches",
                keyColumn: "Id",
                keyValue: new Guid("d0426967-668d-4e34-a139-8b86c1defb46"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("61cff61b-a84a-4002-b8dc-029cafc60408"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("d1f44292-46b0-4ca3-a38a-020ce138c88b"));
        }
    }
}
