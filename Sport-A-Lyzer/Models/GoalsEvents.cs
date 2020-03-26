using System;
using System.Collections.Generic;

namespace Sport_A_Lyzer.Models
{
    public partial class GoalsEvents
    {
        public Guid GoalId { get; set; }
        public Guid GameEventId { get; set; }

        public virtual GameEvents GameEvent { get; set; }
        public virtual Goals Goal { get; set; }
    }
}
