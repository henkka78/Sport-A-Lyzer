using System;
using System.Collections.Generic;

namespace Sport_A_Lyzer.Models
{
    public partial class GameEvent
    {
        public Guid Id { get; set; }
        public Guid EventTypeId { get; set; }
        public string Description { get; set; }
        public Guid PlayerId { get; set; }
        public Guid? TeamId { get; set; }
        public DateTime TimeStamp { get; set; }
        public Guid GameId { get; set; }

        public virtual GameEventType EventType { get; set; }
        public virtual Game Game { get; set; }
        public virtual Player Player { get; set; }
        public virtual Team Team { get; set; }
    }
}
