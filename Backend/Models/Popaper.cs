using System;
using System.Collections.Generic;

namespace Backend.Models
{
    public partial class Popaper
    {
        public string PoNo { get; set; }
        public string PaymentNo { get; set; }
        public string InvoiceNo { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan Time { get; set; }
        public string CustomerId { get; set; }
        public string Item { get; set; }
        public string UnitPriceId { get; set; }
        public double Quantity { get; set; }
        public double? Amount { get; set; }
        public string TruckId { get; set; }
    }
}
