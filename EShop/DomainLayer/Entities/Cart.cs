using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Entities
{
    public class Cart : BaseEntity
    {
        public Customer Customer { get; set; } = null!;

        public List<CartProduct>? CartProducts { get; set; }
    }
}
