using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.ResponseModel.Category
{
    public class CategoryViewModel
    {
        public Guid Id { get; set; }
        public string CategoryName { get; set; } = string.Empty;
    }
}
