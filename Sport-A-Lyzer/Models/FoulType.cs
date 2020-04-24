using System;
using System.Collections.Generic;

namespace Sport_A_Lyzer.Models
{
    public partial class FoulType
    {
        public FoulType()
        {
            Foul = new HashSet<Foul>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Uikey { get; set; }

        public virtual ICollection<Foul> Foul { get; set; }
    }
}
