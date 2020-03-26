using System;
using System.Collections.Generic;

namespace Sport_A_Lyzer.Models
{
    public partial class GoalTypes
    {
        public GoalTypes()
        {
            Goals = new HashSet<Goals>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int PointAmount { get; set; }
        public int SportId { get; set; }
        public string Uikey { get; set; }

        public virtual Sports Sport { get; set; }
        public virtual ICollection<Goals> Goals { get; set; }
    }
}
