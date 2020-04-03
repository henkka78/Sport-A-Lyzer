using System;
using System.Collections.Generic;

namespace Sport_A_Lyzer.Models
{
    public partial class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public Guid RoleId { get; set; }
        public Guid? OrganizationId { get; set; }

        public virtual Organization Organization { get; set; }
    }
}
