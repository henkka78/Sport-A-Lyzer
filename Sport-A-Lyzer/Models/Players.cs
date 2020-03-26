using System;
using System.Collections.Generic;

namespace Sport_A_Lyzer.Models
{
    public partial class Players
    {
        public Players()
        {
            Fouls = new HashSet<Fouls>();
            GameEvents = new HashSet<GameEvents>();
            Goals = new HashSet<Goals>();
        }

        public Guid Id { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public int Number { get; set; }
        public Guid TeamId { get; set; }

        public virtual Teams Team { get; set; }
        public virtual ICollection<Fouls> Fouls { get; set; }
        public virtual ICollection<GameEvents> GameEvents { get; set; }
        public virtual ICollection<Goals> Goals { get; set; }
    }
}
