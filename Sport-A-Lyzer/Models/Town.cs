using System;
using System.Collections.Generic;

namespace Sport_A_Lyzer.Models
{
    public partial class Town
    {
        public Town()
        {
            Organization = new HashSet<Organization>();
            Team = new HashSet<Team>();
        }

        public string Id { get; set; }
        public string Name { get; set; }
        public string PostalCode { get; set; }

        public virtual ICollection<Organization> Organization { get; set; }
        public virtual ICollection<Team> Team { get; set; }
    }
}
