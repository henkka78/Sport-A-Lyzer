using System;
using System.Collections.Generic;

namespace Sport_A_Lyzer.Models
{
    public partial class Games
    {
        public Games()
        {
            GameEvents = new HashSet<GameEvents>();
            Goals = new HashSet<Goals>();
        }

        public Guid Id { get; set; }
        public Guid HomeTeamId { get; set; }
        public Guid AwayTeamId { get; set; }
        public Guid? TournamentId { get; set; }
        public int? MinutesPlayed { get; set; }
        public bool? IsClockTicking { get; set; }
        public bool? GameEnded { get; set; }
        public DateTime? StartTime { get; set; }

        public virtual Teams AwayTeam { get; set; }
        public virtual Teams HomeTeam { get; set; }
        public virtual Tournaments Tournament { get; set; }
        public virtual ICollection<GameEvents> GameEvents { get; set; }
        public virtual ICollection<Goals> Goals { get; set; }
    }
}
