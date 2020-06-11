using System;
using System.Collections.Generic;

namespace Backend.Models
{
    public partial class ExitGateData1
    {
        public string ServiceId { get; set; }
        public DateTime DateIn { get; set; }
        public TimeSpan TimeIn { get; set; }
        public string PoNo { get; set; }

        public virtual Popaper PoNoNavigation { get; set; }
    }
}
