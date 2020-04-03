using System;
using System.Collections.Generic;

namespace Sport_A_Lyzer.Models
{
    public partial class GameEventType
    {
        public GameEventType()
        {
            GameEvent = new HashSet<GameEvent>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Uikey { get; set; }
        public int SportId { get; set; }

        public virtual Sport Sport { get; set; }
        public virtual ICollection<GameEvent> GameEvent { get; set; }
    }
}
