using System;
using System.Collections.Generic;

namespace Sport_A_Lyzer.Models
{
    public partial class Player
    {
        public Player()
        {
            Foul = new HashSet<Foul>();
            GameEvent = new HashSet<GameEvent>();
            Goal = new HashSet<Goal>();
        }

        public Guid Id { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public int Number { get; set; }
        public Guid TeamId { get; set; }

        public virtual Team Team { get; set; }
        public virtual ICollection<Foul> Foul { get; set; }
        public virtual ICollection<GameEvent> GameEvent { get; set; }
        public virtual ICollection<Goal> Goal { get; set; }
    }
}
