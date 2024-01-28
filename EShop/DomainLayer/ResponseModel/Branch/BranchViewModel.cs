using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.ResponseModel.Branch
{
    public class BranchViewModel
    {
        public Guid Id { get; set; }
        public string BranchName { get; set; } = string.Empty;
    }
}
