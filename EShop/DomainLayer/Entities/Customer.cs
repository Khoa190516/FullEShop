using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Entities
{
    public class Customer : BaseEntity
    {   
        public string FirstName { get; set; } = string.Empty;
        
        public string LastName { get; set; } = string.Empty;
        
        public string Email { get; set; } = string.Empty;

        public string UserName { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;
        
        public string Image { get; set; } = string.Empty;
        
        public string Phone { get; set; } = string.Empty;

        public bool Gender { get; set; } = false;

        public DateTime BirthDate { get; set; } = DateTime.Now;

        public string Address { get; set; } = string.Empty;

        public Guid? CartId { get; set; }

        public Cart? Cart { get; set; }
    }
}
