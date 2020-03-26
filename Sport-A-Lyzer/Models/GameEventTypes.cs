using System;
using System.Collections.Generic;

namespace Sport_A_Lyzer.Models
{
    public partial class GameEventTypes
    {
        public GameEventTypes()
        {
            GameEvents = new HashSet<GameEvents>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Uikey { get; set; }
        public int SportId { get; set; }

        public virtual Sports Sport { get; set; }
        public virtual ICollection<GameEvents> GameEvents { get; set; }
    }
}
