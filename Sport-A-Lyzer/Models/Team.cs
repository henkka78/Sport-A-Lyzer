using System;
using System.Collections.Generic;

namespace Sport_A_Lyzer.Models
{
    public partial class Team
    {
        public Team()
        {
            Foul = new HashSet<Foul>();
            GameAwayTeam = new HashSet<Game>();
            GameEvent = new HashSet<GameEvent>();
            GameHomeTeam = new HashSet<Game>();
            Goal = new HashSet<Goal>();
            Player = new HashSet<Player>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string HomeTownId { get; set; }

        public virtual Town HomeTown { get; set; }
        public virtual ICollection<Foul> Foul { get; set; }
        public virtual ICollection<Game> GameAwayTeam { get; set; }
        public virtual ICollection<GameEvent> GameEvent { get; set; }
        public virtual ICollection<Game> GameHomeTeam { get; set; }
        public virtual ICollection<Goal> Goal { get; set; }
        public virtual ICollection<Player> Player { get; set; }
    }
}
