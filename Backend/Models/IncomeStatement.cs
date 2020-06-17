using System;
using System.Collections.Generic;

namespace Backend.Models
{
    public partial class IncomeStatement
    {

        public double? SaleGAS95 { get; set; }
        public double? SaleDIESEL { get; set; }
        public double? TotalSale { get; set; }
        public double? COGSGAS95 { get; set; }
        public double? COGSDIESEL { get; set; }
        public double? TotalCOGS { get; set; }
        public double? GrossProfit { get; set; }
        public double? SalarySOS { get; set; }
        public double? SalaryGC { get; set; }
        public double? TotalSalary { get; set; }
        public double? UtilityExp { get; set; }
        public double? Depreciation { get; set; }
        public double? NetIncome { get; set; }
        public double? BeginGAS95 { get; set; }
        public double? BeginDIESEL { get; set; }
        public double? PurchaseGAS95 { get; set; }
        public double? PurchaseDIESEL { get; set; }
        public double? EndGAS95 { get; set; }
        public double? EndDIESEL { get; set; }
    }
}