using System;
using System.Collections.Generic;

namespace Backend.Models
{
    public partial class Transactions
    {
        public int TransactionId { get; set; }
        public DateTime? Date { get; set; }
        public string Description { get; set; }
        public string RefNo { get; set; }
        public string PoNo { get; set; }
        public double? Amount { get; set; }
        public string Type { get; set; }
    }
}
