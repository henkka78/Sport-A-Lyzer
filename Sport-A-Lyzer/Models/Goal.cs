using System;
using System.Collections.Generic;

namespace Sport_A_Lyzer.Models
{
    public partial class Goal
    {
        public Guid Id { get; set; }
        public Guid TeamId { get; set; }
        public Guid GameId { get; set; }
        public Guid PlayerId { get; set; }
        public int GoalTypeId { get; set; }
        public int MinuteOfGame { get; set; }

        public virtual Game Game { get; set; }
        public virtual GoalType GoalType { get; set; }
        public virtual Player Player { get; set; }
        public virtual Team Team { get; set; }
    }
}
