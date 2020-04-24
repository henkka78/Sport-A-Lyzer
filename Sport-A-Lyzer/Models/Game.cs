using System;
using System.Collections.Generic;

namespace Sport_A_Lyzer.Models
{
    public partial class Game
    {
        public Game()
        {
            GameEvent = new HashSet<GameEvent>();
            GamePause = new HashSet<GamePause>();
            Goal = new HashSet<Goal>();
        }

        public Guid Id { get; set; }
        public Guid HomeTeamId { get; set; }
        public Guid AwayTeamId { get; set; }
        public Guid? TournamentId { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? GameDay { get; set; }
        public DateTime? ActualStartTime { get; set; }
        public DateTime? ActualEndTime { get; set; }
        public int? PlannedLength { get; set; }
        public string Description { get; set; }
        public string PitchName { get; set; }

        public virtual Team AwayTeam { get; set; }
        public virtual Team HomeTeam { get; set; }
        public virtual Tournament Tournament { get; set; }
        public virtual ICollection<GameEvent> GameEvent { get; set; }
        public virtual ICollection<GamePause> GamePause { get; set; }
        public virtual ICollection<Goal> Goal { get; set; }
    }
}
