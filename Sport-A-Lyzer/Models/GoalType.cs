using System;
using System.Collections.Generic;

namespace Sport_A_Lyzer.Models
{
    public partial class GoalType
    {
        public GoalType()
        {
            Goal = new HashSet<Goal>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int PointAmount { get; set; }
        public int SportId { get; set; }
        public string Uikey { get; set; }

        public virtual Sport Sport { get; set; }
        public virtual ICollection<Goal> Goal { get; set; }
    }
}
