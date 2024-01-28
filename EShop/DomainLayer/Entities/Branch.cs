using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Entities
{
    public class Branch : BaseEntity
    {
        public string BranchName { get; set; } = string.Empty;

        public List<Product>? Products { get; set; }
    }
}
