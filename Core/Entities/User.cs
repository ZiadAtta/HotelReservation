using Core.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class User : BaseEntity
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string? Password { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Country { get; set; }    
        public string? UserName { get; set; }   
        public Role? Role { get; set; }
        public ICollection<Comment>? Comments { get; set; }
        public ICollection<Rate>? Rates { get; set; }
        public ICollection<Favorite>? Favorites { get; set; }
    }
}
