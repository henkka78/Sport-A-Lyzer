using System;
using System.Collections.Generic;

namespace Sport_A_Lyzer.Models
{
    public partial class Game
    {
        public Game()
        {
            GameClockEvent = new HashSet<GameClockEvent>();
            GameEvent = new HashSet<GameEvent>();
            Goal = new HashSet<Goal>();
        }

        public Guid Id { get; set; }
        public Guid HomeTeamId { get; set; }
        public Guid AwayTeamId { get; set; }
        public Guid? TournamentId { get; set; }
        public int? MinutesPlayed { get; set; }
        public bool? IsClockTicking { get; set; }
        public bool? GameEnded { get; set; }
        public DateTime? StartTime { get; set; }

        public virtual Team AwayTeam { get; set; }
        public virtual Team HomeTeam { get; set; }
        public virtual Tournament Tournament { get; set; }
        public virtual ICollection<GameClockEvent> GameClockEvent { get; set; }
        public virtual ICollection<GameEvent> GameEvent { get; set; }
        public virtual ICollection<Goal> Goal { get; set; }
    }
}
