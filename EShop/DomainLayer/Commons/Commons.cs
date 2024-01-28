using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Commons
{
    public enum ROLES
    {
        ADMIN = 1,
        CUSTOMER = 2
    }

    public static class Commons
    {
        public static string ADMIN = "Admin";
        public static string CUSTOMER = "Customer";
    }
}
