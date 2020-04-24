using System;
using System.Collections.Generic;

namespace Sport_A_Lyzer.Models
{
    public partial class Sport
    {
        public Sport()
        {
            GameEventType = new HashSet<GameEventType>();
            GoalType = new HashSet<GoalType>();
            Translation = new HashSet<Translation>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Uikey { get; set; }

        public virtual ICollection<GameEventType> GameEventType { get; set; }
        public virtual ICollection<GoalType> GoalType { get; set; }
        public virtual ICollection<Translation> Translation { get; set; }
    }
}
