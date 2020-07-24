using System;
using System.Collections.Generic;
using System.Text;

namespace CRMSystem.Domains
{
    public class User:BaseEntity
    {
        public string Name { get; set; }
        public string Post { get; set; }
        public string Phone { get; set; }
        public string Gender { get; set; }
        public string Image { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public int RoleID { get; set; }
        public Role Role { get; set; }

    }
}
