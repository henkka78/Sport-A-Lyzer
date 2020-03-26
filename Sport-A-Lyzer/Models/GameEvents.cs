using System;
using System.Collections.Generic;

namespace Sport_A_Lyzer.Models
{
    public partial class GameEvents
    {
        public Guid Id { get; set; }
        public Guid EventTypeId { get; set; }
        public string Description { get; set; }
        public Guid PlayerId { get; set; }
        public Guid? TeamId { get; set; }
        public DateTime TimeStamp { get; set; }
        public Guid GameId { get; set; }

        public virtual GameEventTypes EventType { get; set; }
        public virtual Games Game { get; set; }
        public virtual Players Player { get; set; }
        public virtual Teams Team { get; set; }
    }
}
