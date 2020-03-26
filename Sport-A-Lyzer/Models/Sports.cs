using System;
using System.Collections.Generic;

namespace Sport_A_Lyzer.Models
{
    public partial class Sports
    {
        public Sports()
        {
            GameEventTypes = new HashSet<GameEventTypes>();
            GoalTypes = new HashSet<GoalTypes>();
            Translations = new HashSet<Translations>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Uikey { get; set; }

        public virtual ICollection<GameEventTypes> GameEventTypes { get; set; }
        public virtual ICollection<GoalTypes> GoalTypes { get; set; }
        public virtual ICollection<Translations> Translations { get; set; }
    }
}
