using DataAccessLayer.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BussinesLayer.ResponseModels.Cart
{
    public class CartResponseModel
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public decimal Total { get; set; }

        public decimal DiscountedTotal { get; set; }

        public int TotalProducts { get; set; }

        public int TotalQuantity { get; set; }

        public List<CartItemResponseModel> Products { get; set; } = new ();
    }

    public class CartItemResponseModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public decimal Total { get; set; } = 0;
        public float DiscountPercentage { get; set; }
        public decimal DiscountPrice { get; set; }
        public string Thumbnail { get; set; } = string.Empty;

    }
}
