using System;
using System.Collections.Generic;

namespace Sport_A_Lyzer.Models
{
    public partial class Foul
    {
        public int Id { get; set; }
        public int FoulTypeId { get; set; }
        public Guid PlayerId { get; set; }
        public Guid? TeamId { get; set; }
        public string Description { get; set; }
        public DateTime TimeStamp { get; set; }

        public virtual FoulType FoulType { get; set; }
        public virtual Player Player { get; set; }
        public virtual Team Team { get; set; }
    }
}
