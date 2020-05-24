using System;
using System.Collections.Generic;

namespace Sport_A_Lyzer.Models
{
    public sealed partial class Organization
    {
        public Organization()
        {
            User = new HashSet<User>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string HometownId { get; set; }
        public string Description { get; set; }
        public string Options { get; set; }

        public Town Hometown { get; set; }
        public ICollection<User> User { get; set; }
    }
}
