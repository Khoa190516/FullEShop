using DomainLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.RequestModels.Product
{
    public class ProductAddModel
    {
        public string Title { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public decimal Price { get; set; } = 0;

        public int Stock { get; set; } = 0;

        public float DiscountPercentage { get; set; } = 0;

        public float Rating { get; set; } = 0;

        public string Thumbnail { get; set; } = string.Empty;

        public Guid CategoryId { get; set; }

        public Guid BranchId { get; set; }

        public string Images { get; set; } = string.Empty;
    }
}
