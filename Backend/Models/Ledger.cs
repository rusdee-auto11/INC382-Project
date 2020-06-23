using System;
using System.Collections.Generic;

namespace Backend.Models
{
    public partial class Ledger
    {
        public DateTime? Date { get; set; }
        public string Description { get; set; }
        public string RefNo { get; set; }
        public string PoNo { get; set; }
        public double? Amount { get; set; }
        public string Type { get; set; }
        public string JRefNo { get; set; }
        public double? Balance { get; set; }
    }
}