using System;
using System.Collections.Generic;

namespace Sport_A_Lyzer.Models
{
    public partial class Translation
    {
        public int Id { get; set; }
        public int SportId { get; set; }
        public string Uikey { get; set; }
        public string Translation1 { get; set; }

        public virtual Sport Sport { get; set; }
    }
}
