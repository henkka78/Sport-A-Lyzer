using System;
using System.Collections.Generic;

namespace Sport_A_Lyzer.Models
{
    public partial class GameEvents
    {
        public GameEvents()
        {
            GoalsEvents = new HashSet<GoalsEvents>();
        }

        public Guid Id { get; set; }
        public int EventTypeId { get; set; }
        public string Description { get; set; }
        public Guid PlayerId { get; set; }
        public Guid? TeamId { get; set; }
        public DateTime TimeStamp { get; set; }
        public Guid GameId { get; set; }

        public virtual GameEventTypes EventType { get; set; }
        public virtual Games Game { get; set; }
        public virtual Players Player { get; set; }
        public virtual Teams Team { get; set; }
        public virtual ICollection<GoalsEvents> GoalsEvents { get; set; }
    }
}
