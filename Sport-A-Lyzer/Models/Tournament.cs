using System;
using System.Collections.Generic;

namespace Sport_A_Lyzer.Models
{
    public partial class Tournament
    {
        public Tournament()
        {
            Game = new HashSet<Game>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

        public virtual ICollection<Game> Game { get; set; }
    }
}
