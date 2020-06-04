using System;
using System.Collections.Generic;

namespace Backend.Models
{
    public partial class _ExitGateData
    {
        public string ServiceId { get; set; }
        public DateTime DateIn { get; set; }
        public TimeSpan TimeIn { get; set; }
        public string PoNo { get; set; }

    }
}