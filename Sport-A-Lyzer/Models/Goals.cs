using System;
using System.Collections.Generic;

namespace Sport_A_Lyzer.Models
{
    public partial class Goals
    {
        public Guid Id { get; set; }
        public Guid TeamId { get; set; }
        public Guid GameId { get; set; }
        public Guid PlayerId { get; set; }
        public int GoalTypeId { get; set; }
        public int MinuteOfGame { get; set; }

        public virtual Games Game { get; set; }
        public virtual GoalTypes GoalType { get; set; }
        public virtual Players Player { get; set; }
        public virtual Teams Team { get; set; }
    }
}
