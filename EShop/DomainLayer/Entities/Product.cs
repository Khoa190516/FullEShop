using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Entities
{
    public class Product : BaseEntity
    {
        public string Title { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public decimal Price { get; set; } = 0;

        public int Stock { get; set; } = 0;

        public float DiscountPercentage { get; set; } = 0;

        public float Rating { get; set; } = 0;

        public string Thumbnail { get; set; } = string.Empty;

        public Guid CategoryId { get; set; }

        public Category Category { get; set; } = null!;

        public Guid BranchId { get; set; }

        public Branch Branch { get; set; } = null!;

        public List<CartProduct>? CartProducts { get; set; }

        public string Images { get; set; } = string.Empty;
    }
}
