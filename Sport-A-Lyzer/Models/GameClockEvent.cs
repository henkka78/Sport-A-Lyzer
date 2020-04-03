using System;
using System.Collections.Generic;

namespace Sport_A_Lyzer.Models
{
    public partial class GameClockEvent
    {
        public int Id { get; set; }
        public Guid GameId { get; set; }
        public DateTime TimeStamp { get; set; }
        public bool IsClockRunning { get; set; }
        public int SecondsSinceStart { get; set; }

        public virtual Game Game { get; set; }
    }
}
