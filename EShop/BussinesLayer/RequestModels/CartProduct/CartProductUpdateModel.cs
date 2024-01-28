using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinesLayer.RequestModels.CartProduct
{
    public class CartProductUpdateModel
    {
        public Guid ProductId { get; set; }
        
        public int Quantity { get; set; }

        public bool IsDecrease { get; set; } = false;
    }
}
