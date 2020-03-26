using System;
using System.Collections.Generic;

namespace Sport_A_Lyzer.Models
{
    public partial class Teams
    {
        public Teams()
        {
            Fouls = new HashSet<Fouls>();
            GameEvents = new HashSet<GameEvents>();
            GamesAwayTeam = new HashSet<Games>();
            GamesHomeTeam = new HashSet<Games>();
            Goals = new HashSet<Goals>();
            Players = new HashSet<Players>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Fouls> Fouls { get; set; }
        public virtual ICollection<GameEvents> GameEvents { get; set; }
        public virtual ICollection<Games> GamesAwayTeam { get; set; }
        public virtual ICollection<Games> GamesHomeTeam { get; set; }
        public virtual ICollection<Goals> Goals { get; set; }
        public virtual ICollection<Players> Players { get; set; }
    }
}
