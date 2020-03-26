using System;
using System.Collections.Generic;

namespace Sport_A_Lyzer.Models
{
    public partial class FoulTypes
    {
        public FoulTypes()
        {
            Fouls = new HashSet<Fouls>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Uikey { get; set; }

        public virtual ICollection<Fouls> Fouls { get; set; }
    }
}
