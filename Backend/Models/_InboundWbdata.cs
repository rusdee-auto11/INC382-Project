using System;
using System.Collections.Generic;

namespace Backend.Models
{
    public partial class _InboundWbdata
    {
        public string ServiceId { get; set; }
        public DateTime DateIn { get; set; }
        public TimeSpan TimeIn { get; set; }
        public string PoNo { get; set; }
        public DateTime DateOut { get; set; }
        public TimeSpan TimeOut { get; set; }
    }
}