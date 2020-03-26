using System;
using System.Collections.Generic;

namespace Sport_A_Lyzer.Models
{
    public partial class Translations
    {
        public int Id { get; set; }
        public int SportId { get; set; }
        public string Uikey { get; set; }
        public string Translation { get; set; }

        public virtual Sports Sport { get; set; }
    }
}
