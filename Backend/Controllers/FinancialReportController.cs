using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Backend.Data;
using Backend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using System.Net;
using Mapster;
using System.Linq;

namespace Backend.Controllers
{
    [ApiController]
    [Route("api")]
    public class FinancialReportController : ControllerBase
    {
        private readonly ILogger<FinancialReportController> _logger;
        private readonly DatabaseContext _databaseContext;
        public FinancialReportController(ILogger<FinancialReportController> logger, DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
            _logger = logger;
        }

        [HttpGet("getReconShtData")]
        public IActionResult getReconShtData()
        {
            try
            {
                // var _result = _databaseContext.Popaper.FromSqlRaw("SELECT * FROM _POPaper").ToList();
                var _result = (from p in _databaseContext.Popaper
                               join g in _databaseContext.Gas on p.Item equals g.GasId
                               join c in _databaseContext.CustomerInfo on p.CustomerId equals c.CustomerId
                               select new
                               {
                                   p.Date,
                                   p.PoNo,
                                   p.PaymentNo,
                                   p.InvoiceNo,
                                   p.TruckId,
                                   g.GasType,
                                   p.Quantity,
                                   p.Amount,
                                   c.Name
                               }).ToList();
                return Ok(new { result = _result, message = "success" });
            }
            catch (Exception ex)
            {
                return NotFound(new { result = ex, message = "fail" });
            }
        }

        [HttpGet("getPOData/{POno}")]
        public IActionResult getPOData(String POno)
        {
            try
            {
                // var _result = _databaseContext.Popaper.FromSqlRaw("SELECT * FROM _POPaper WHERE Date={0}",SelDate).ToList();
                var _result = (from p in _databaseContext.Popaper
                               join g in _databaseContext.Gas on p.Item equals g.GasId
                               join c in _databaseContext.CustomerInfo on p.CustomerId equals c.CustomerId
                               join cp in _databaseContext.CostPriceGas on p.UnitPriceId equals cp.GasPriceId
                               join t in _databaseContext.Truck on p.TruckId equals t.TruckId
                               select new
                               {
                                   p.Date,
                                   p.PoNo,
                                   p.PaymentNo,
                                   g.GasType,
                                   cp.Price,
                                   p.Quantity,
                                   p.Amount,
                                   p.CustomerId,
                                   c.Name,
                                   c.TaxPayerId,
                                   c.PhoneNo,
                                   p.TruckId,
                                   t.TruckDriverName
                               }).Where(o => o.PoNo == POno).ToList();
                return Ok(new { result = _result, message = "success" });
            }
            catch (Exception ex)
            {
                return NotFound(new { result = ex, message = "fail" });
            }
        }

        [HttpGet("getInvoiceData/{IVno}")]
        public IActionResult getInvoiceData(String IVno)
        {
            try
            {
                // var _result = _databaseContext.Popaper.FromSqlRaw("SELECT * FROM _POPaper WHERE Date={0}",SelDate).ToList();
                var _result = (from p in _databaseContext.Popaper
                               join g in _databaseContext.Gas on p.Item equals g.GasId
                               join c in _databaseContext.CustomerInfo on p.CustomerId equals c.CustomerId
                               join cp in _databaseContext.CostPriceGas on p.UnitPriceId equals cp.GasPriceId
                               join b in _databaseContext.BayData on p.PoNo equals b.PoNo
                               select new
                               {
                                   p.Date,
                                   p.InvoiceNo,
                                   p.PoNo,
                                   p.PaymentNo,
                                   b.ServiceTime,
                                   g.GasType,
                                   cp.Price,
                                   p.Quantity,
                                   p.Amount,
                                   p.CustomerId,
                                   c.Name,
                                   c.Address,
                                   c.TaxPayerId
                               }).Where(o => o.InvoiceNo == IVno).ToList();
                return Ok(new { result = _result, message = "success" });
            }
            catch (Exception ex)
            {
                return NotFound(new { result = ex, message = "fail" });
            }
        }

        // [HttpGet("testgetPOData/{po}")]
        // public IActionResult GetPOTest(string po)
        // {
        //     // step1 -> get data
        //     Popaper1 value = _databaseContext.Popaper1.SingleOrDefault(o => o.PoNo == po);
        //     IEnumerable<Popaper1> values = _databaseContext.Popaper1.ToList();
        //     // step2 -> change value ->
        //     Popaper result = new Popaper();
        //     if(a<10)
        //     {
        //         result.Amount = 1000;
        //     }
        //     else
        //     {
        //         result.Amount = 2000;
        //     }

        //     // list ?
        //     Popaper _value = value.Adapt<Popaper>();
        //     IEnumerable<Popaper> _value1 = values.Adapt<IEnumerable<Popaper>>();

        //     //step3 -> save PoNo
        //     _databaseContext.Popaper.Add(result);
        //     _databaseContext.SaveChanges(); // Check point

        //     return Ok(new {result = result});
        // }

        // [HttpGet("createJournal")]
        public IActionResult createJournal()
        {
            try
            {
                // IEnumerable<Popaper> values = _databaseContext.Popaper.ToList();
                List<Transactions> result = new List<Transactions>();

                double DIESELTank = 30000;
                double GASOHOL95Tank = 40000;
                int DISELLots = 25000;
                int GASOHOL95Lots = 20000;

                var values = (from p in _databaseContext.Popaper
                              join g in _databaseContext.Gas on p.Item equals g.GasId
                              join c in _databaseContext.CustomerInfo on p.CustomerId equals c.CustomerId
                              join cp in _databaseContext.CostPriceGas on p.UnitPriceId equals cp.GasPriceId
                              join t in _databaseContext.Truck on p.TruckId equals t.TruckId
                              select new
                              {
                                  p.Date,
                                  p.PoNo,
                                  g.GasType,
                                  cp.Price,
                                  cp.Cost,
                                  p.Quantity,
                                  p.Amount,
                                  p.TruckId
                              }).ToList();

                int i = 0;
                int count = values.Count();
                DateTime bmo = new DateTime(2018, 3, 1);
                result.Add(new Transactions());
                // result[i].TransactionId = i;
                result[i].Date = bmo;
                result[i].Description = "Beginning Cash";
                result[i].Amount = 300000;
                result[i].RefNo = "101";
                result[i].Type = "Debit";
                _databaseContext.Transactions.Add(result[i]);
                _databaseContext.SaveChanges();
                ++i;

                result.Add(new Transactions());
                // result[i].TransactionId = i;
                result[i].Date = bmo;
                result[i].Description = "Share Capital-Ordinary";
                result[i].Amount = 300000;
                result[i].RefNo = "311";
                result[i].Type = "Credit";
                _databaseContext.Transactions.Add(result[i]);
                _databaseContext.SaveChanges();
                ++i;

                result.Add(new Transactions());
                // result[i].TransactionId = i;
                result[i].Date = bmo;
                result[i].Description = "Beginning Inventory";
                result[i].Amount = (DIESELTank * 27.08) + (GASOHOL95Tank * 28.08);
                result[i].RefNo = "157";
                result[i].Type = "Debit";
                _databaseContext.Transactions.Add(result[i]);
                _databaseContext.SaveChanges();
                ++i;

                result.Add(new Transactions());
                // result[i].TransactionId = i;
                result[i].Date = bmo;
                result[i].Description = "Account Payable";
                result[i].Amount = (DIESELTank * 27.08) + (GASOHOL95Tank * 28.08);
                result[i].RefNo = "201";
                result[i].Type = "Credit";
                _databaseContext.Transactions.Add(result[i]);
                _databaseContext.SaveChanges();
                ++i;

                foreach (var item in values)
                {

                    if (DIESELTank < 20000)
                    {
                        result.Add(new Transactions());
                        // result[i].TransactionId = i;
                        result[i].Date = item.Date;
                        result[i].Description = "Inventory - DIESEL";
                        result[i].Amount = item.Cost * DISELLots;
                        result[i].RefNo = "157";
                        result[i].Type = "Debit";
                        _databaseContext.Transactions.Add(result[i]);
                        _databaseContext.SaveChanges();
                        ++i;

                        result.Add(new Transactions());
                        // result[i].TransactionId = i;
                        result[i].Date = item.Date;
                        result[i].Description = "Account Payable";
                        result[i].Amount = item.Cost * DISELLots;
                        result[i].RefNo = "201";
                        result[i].Type = "Credit";
                        _databaseContext.Transactions.Add(result[i]);
                        _databaseContext.SaveChanges();
                        ++i;
                        DIESELTank += 25000;
                    }
                    if (GASOHOL95Tank < 10000)
                    {
                        result.Add(new Transactions());
                        // result[i].TransactionId = i;
                        result[i].Date = item.Date;
                        result[i].Description = "Inventory - GASOHOL95";
                        result[i].Amount = item.Cost * GASOHOL95Lots;
                        result[i].RefNo = "157";
                        result[i].Type = "Debit";
                        _databaseContext.Transactions.Add(result[i]);
                        _databaseContext.SaveChanges();
                        ++i;

                        result.Add(new Transactions());
                        // result[i].TransactionId = i;
                        result[i].Date = item.Date;
                        result[i].Description = "Account Payable";
                        result[i].Amount = item.Cost * GASOHOL95Lots;
                        result[i].RefNo = "201";
                        result[i].Type = "Credit";
                        _databaseContext.Transactions.Add(result[i]);
                        _databaseContext.SaveChanges();
                        ++i;
                        GASOHOL95Tank += 20000;
                    }
                    result.Add(new Transactions());
                    // result[i].TransactionId = i;
                    result[i].Date = item.Date;
                    result[i].Description = "Account Receivable" + " (" + item.TruckId + ")";
                    result[i].Amount = item.Amount;
                    result[i].RefNo = "112";
                    result[i].Type = "Debit";
                    result[i].PoNo = item.PoNo;
                    _databaseContext.Transactions.Add(result[i]);
                    _databaseContext.SaveChanges();
                    ++i;

                    result.Add(new Transactions());
                    // result[i].TransactionId = i;
                    result[i].Date = item.Date;
                    result[i].Description = "Sale Revenue";
                    result[i].Amount = item.Amount;
                    result[i].RefNo = "400";
                    result[i].Type = "Credit";
                    result[i].PoNo = item.PoNo;
                    _databaseContext.Transactions.Add(result[i]);
                    _databaseContext.SaveChanges();
                    ++i;


                    if (item.GasType == "DIESEL")
                    {
                        result.Add(new Transactions());
                        // result[i].TransactionId = i;
                        result[i].Date = item.Date;
                        result[i].Description = "Cost of Goods Sold - DIESEL";
                        result[i].Amount = item.Cost * item.Quantity;
                        result[i].RefNo = "730";
                        result[i].Type = "Debit";
                        result[i].PoNo = item.PoNo;
                        _databaseContext.Transactions.Add(result[i]);
                        _databaseContext.SaveChanges();
                        ++i;


                        result.Add(new Transactions());
                        // result[i].TransactionId = i;
                        result[i].Date = item.Date;
                        result[i].Description = "Inventory - DIESEL";
                        result[i].Amount = item.Cost * item.Quantity;
                        result[i].RefNo = "157";
                        result[i].Type = "Credit";
                        result[i].PoNo = item.PoNo;
                        _databaseContext.Transactions.Add(result[i]);
                        _databaseContext.SaveChanges();
                        ++i;

                        DIESELTank -= item.Quantity;
                    }
                    else if (item.GasType == "GASOHOL95")
                    {
                        result.Add(new Transactions());
                        // result[i].TransactionId = i;
                        result[i].Date = item.Date;
                        result[i].Description = "Cost of Goods Sold - GASOHOL95";
                        result[i].Amount = item.Cost * item.Quantity;
                        result[i].RefNo = "730";
                        result[i].Type = "Debit";
                        result[i].PoNo = item.PoNo;
                        _databaseContext.Transactions.Add(result[i]);
                        _databaseContext.SaveChanges();
                        ++i;

                        result.Add(new Transactions());
                        // result[i].TransactionId = i;
                        result[i].Date = item.Date;
                        result[i].Description = "Inventory - GASOHOL95";
                        result[i].Amount = item.Cost * item.Quantity;
                        result[i].RefNo = "157";
                        result[i].Type = "Credit";
                        result[i].PoNo = item.PoNo;
                        _databaseContext.Transactions.Add(result[i]);
                        _databaseContext.SaveChanges();
                        ++i;

                        GASOHOL95Tank -= item.Quantity;
                    }
                    // _databaseContext.SaveChanges();

                }
                DateTime mo = new DateTime(2018, 03, 31);
                result.Add(new Transactions());
                // result[i].TransactionId = i;
                result[i].Date = mo;
                result[i].Description = "Salaries and Wages Expense - Sale Office Staffs";
                result[i].Amount = ((500 + 400) * 2) * 31;
                result[i].RefNo = "726";
                result[i].Type = "Debit";
                _databaseContext.Transactions.Add(result[i]);
                _databaseContext.SaveChanges();
                ++i;

                result.Add(new Transactions());
                // result[i].TransactionId = i;
                result[i].Date = mo;
                result[i].Description = "Cash";
                result[i].Amount = ((500 + 400) * 2) * 31;
                result[i].RefNo = "101";
                result[i].Type = "Credit";
                _databaseContext.Transactions.Add(result[i]);
                _databaseContext.SaveChanges();
                ++i;

                result.Add(new Transactions());
                // result[i].TransactionId = i;
                result[i].Date = mo;
                result[i].Description = "Salaries and Wages Expense - Gate Controller";
                result[i].Amount = (700 + 550) * 31;
                result[i].RefNo = "726";
                result[i].Type = "Debit";
                _databaseContext.Transactions.Add(result[i]);
                _databaseContext.SaveChanges();
                ++i;

                result.Add(new Transactions());
                // result[i].TransactionId = i;
                result[i].Date = mo;
                result[i].Description = "Cash";
                result[i].Amount = (700 + 550) * 31;
                result[i].RefNo = "101";
                result[i].Type = "Credit";
                _databaseContext.Transactions.Add(result[i]);
                _databaseContext.SaveChanges();
                ++i;

                result.Add(new Transactions());
                // result[i].TransactionId = i;
                result[i].Date = mo;
                result[i].Description = "Utility Expense";
                result[i].Amount = 1500 * 31;
                result[i].RefNo = "728";
                result[i].Type = "Debit";
                _databaseContext.Transactions.Add(result[i]);
                _databaseContext.SaveChanges();
                ++i;

                result.Add(new Transactions());
                // result[i].TransactionId = i;
                result[i].Date = mo;
                result[i].Description = "Cash";
                result[i].Amount = 1500 * 31;
                result[i].RefNo = "101";
                result[i].Type = "Credit";
                _databaseContext.Transactions.Add(result[i]);
                _databaseContext.SaveChanges();
                ++i;

                result.Add(new Transactions());
                // result[i].TransactionId = i;
                result[i].Date = mo;
                result[i].Description = "Depreciation of Gas Dispenser Machine - DIESEL";
                result[i].Amount = (15000 / 12) * 4;
                result[i].RefNo = "840";
                result[i].Type = "Debit";
                // result[i].PoNo = Convert.ToString(DIESELTank);
                _databaseContext.Transactions.Add(result[i]);
                _databaseContext.SaveChanges();
                ++i;

                result.Add(new Transactions());
                // result[i].TransactionId = i;
                result[i].Date = mo;
                result[i].Description = "Accumulated Depreciation of Gas Dispenser Machine - DIESEL";
                result[i].Amount = (15000 / 12) * 4;
                result[i].RefNo = "280";
                result[i].Type = "Credit";
                _databaseContext.Transactions.Add(result[i]);
                _databaseContext.SaveChanges();
                ++i;

                result.Add(new Transactions());
                // result[i].TransactionId = i;
                result[i].Date = mo;
                result[i].Description = "Depreciation of Gas Dispenser Machine - GASOHOL95";
                result[i].Amount = (15000 / 12) * 2;
                result[i].RefNo = "840";
                result[i].Type = "Debit";
                result[i].PoNo = Convert.ToString(GASOHOL95Tank);
                _databaseContext.Transactions.Add(result[i]);
                _databaseContext.SaveChanges();
                ++i;

                result.Add(new Transactions());
                // result[i].TransactionId = i;
                result[i].Date = mo;
                result[i].Description = "Accumulated Depreciation of Gas Dispenser Machine - GASOHOL95";
                result[i].Amount = (15000 / 12) * 2;
                result[i].RefNo = "280";
                result[i].Type = "Credit";
                _databaseContext.Transactions.Add(result[i]);
                _databaseContext.SaveChanges();
                ++i;


                return Ok(new { result = result, message = "success" });
            }
            catch (Exception ex)
            {
                return NotFound(new { result = ex, message = "fail" });
            }


        }

        [HttpGet("GetJournal/{SelDate}")]
        public IActionResult GetJournal(DateTime SelDate)
        {
            try
            {
                var _result = (from j in _databaseContext.Transactions
                               select new
                               {
                                   j.Date,
                                   j.Description,
                                   j.RefNo,
                                   j.Amount,
                                   j.Type
                               }).Where(o => o.Date == SelDate).ToList();
                return Ok(new { result = _result, message = "success" });
            }
            catch (Exception ex)
            {
                return NotFound(new { result = ex, message = "fail" });
            }
        }

        [HttpGet("getLedger/{account}")]
        public IActionResult getLedger(string account)
        {
            try
            {
                List<Ledger> result = new List<Ledger>();
                var values = (from j in _databaseContext.Transactions
                              select new
                              {
                                  j.Date,
                                  j.Description,
                                  j.RefNo,
                                  j.Amount,
                                  j.Type
                              }).ToList();

                if (account == "101" || account == "157" || account == "112" || account == "201")
                {
                    int i = 0;
                    for (int a = 0; a < values.Count(); a++)
                    {
                        if (account == "101" || account == "157")
                        {
                            if ("Credit" == values[a].Type && account == values[a].RefNo)
                            {
                                result.Add(new Ledger());
                                result[i].Date = values[a].Date;
                                result[i].Description = values[a - 1].Description;
                                result[i].Amount = values[a].Amount;
                                result[i].RefNo = values[a].RefNo;
                                result[i].Type = values[a].Type;
                                result[i].JRefNo = "J" + Convert.ToString(Convert.ToDateTime(values[a].Date).Day);
                                ++i;
                            }
                            else if ("Debit" == values[a].Type && account == values[a].RefNo)
                            {
                                result.Add(new Ledger());
                                result[i].Date = values[a].Date;
                                result[i].Description = values[a].Description;
                                result[i].Amount = values[a].Amount;
                                result[i].RefNo = values[a].RefNo;
                                result[i].Type = values[a].Type;
                                result[i].JRefNo = "J" + Convert.ToString(Convert.ToDateTime(values[a].Date).Day);
                                ++i;
                            }
                        }
                        else
                        {
                            if (account == values[a].RefNo)
                            {
                                result.Add(new Ledger());
                                result[i].Date = values[a].Date;
                                result[i].Description = values[a].Description;
                                result[i].Amount = values[a].Amount;
                                result[i].RefNo = values[a].RefNo;
                                result[i].Type = values[a].Type;
                                result[i].JRefNo = "J" + Convert.ToString(Convert.ToDateTime(values[a].Date).Day);
                                ++i;
                            }

                        }

                    }

                    int n = 0;
                    double? b = 0;
                    foreach (var item in result)
                    {
                        if (item.RefNo == "101" || item.RefNo == "112" || item.RefNo == "157")
                        {
                            b = item.Type == "Debit" ? b + item.Amount : b - item.Amount;
                            result[n].Date = item.Date;
                            result[n].Description = item.Description;
                            result[n].Amount = item.Amount;
                            result[n].RefNo = item.RefNo;
                            result[n].Type = item.Type;
                            result[n].JRefNo = item.JRefNo;
                            result[n].Balance = b;
                        }
                        else if (item.RefNo == "201")
                        {
                            b = item.Type == "Credit" ? b + item.Amount : b - item.Amount;

                            result[n].Date = item.Date;
                            result[n].Description = item.Description;
                            result[n].Amount = item.Amount;
                            result[n].RefNo = item.RefNo;
                            result[n].Type = item.Type;
                            result[n].JRefNo = item.JRefNo;
                            result[n].Balance = b;
                        }
                        ++n;
                    }
                }
                return Ok(new { result = result, message = "success" });

            }
            catch (Exception ex)
            {
                return NotFound(new { result = ex, message = "fail" });
            }
        }

        [HttpGet("getIncomeStm")]
        public IActionResult getIncomeStm()
        {
            try
            {
                List<IncomeStatement> result = new List<IncomeStatement>();
                var values = (from j in _databaseContext.Transactions
                              join p in _databaseContext.Popaper on j.PoNo equals p.PoNo
                              join g in _databaseContext.Gas on p.Item equals g.GasId
                              select new
                              {
                                  j.Date,
                                  j.Description,
                                  j.RefNo,
                                  j.Amount,
                                  j.Type,
                                  g.GasType
                              }).ToList();
                var values1 = (from j in _databaseContext.Transactions
                               select new
                               {
                                   j.Date,
                                   j.Description,
                                   j.RefNo,
                                   j.PoNo,
                                   j.Amount,
                                   j.Type,
                               }).Where(o => o.PoNo == null).ToList();
                double? SG = 0, SD = 0, COGSG = 0, COGSD = 0, SSOS = 0, SGC = 0, UE = 0, DP = 0, PG = 0, PD = 0;
                foreach (var item in values)
                {
                    if (item.RefNo == "400" && item.GasType == "GASOHOL95" && item.Type == "Credit")
                    {
                        SG = SG + item.Amount;
                    }
                    else if (item.RefNo == "400" && item.GasType == "DIESEL" && item.Type == "Credit")
                    {
                        SD = SD + item.Amount;
                    }
                    else if (item.RefNo == "730" && item.GasType == "GASOHOL95" && item.Type == "Debit")
                    {
                        COGSG = COGSG + item.Amount;
                    }
                    else if (item.RefNo == "730" && item.GasType == "DIESEL" && item.Type == "Debit")
                    {
                        COGSD = COGSD + item.Amount;
                    }
                }
                foreach (var item1 in values1)
                {
                    if (item1.RefNo == "728")
                    {
                        UE = UE + item1.Amount;
                    }
                    else if (item1.Description == "Salaries and Wages Expense - Sale Office Staffs")
                    {
                        SSOS = SSOS + item1.Amount;
                    }
                    else if (item1.Description == "Salaries and Wages Expense - Gate Controller")
                    {
                        SGC = SGC + item1.Amount;
                    }
                    else if (item1.RefNo == "840")
                    {
                        DP = DP + item1.Amount;
                    }
                    else if (item1.RefNo == "157" && item1.Description == "Inventory - GASOHOL95" && item1.Type == "Debit")
                    {
                        PG = PG + item1.Amount;
                    }
                    else if (item1.RefNo == "157" && item1.Description == "Inventory - DIESEL" && item1.Type == "Debit")
                    {
                        PD = PD + item1.Amount;
                    }
                }

                var Dcost = (from c in _databaseContext.CostPriceGas
                            select new
                            {
                                c.Date,
                                c.GasId,
                                c.Cost
                            }).Where(o => o.GasId == "A01").ToList();
                var Gcost = (from c in _databaseContext.CostPriceGas
                            select new
                            {
                                c.Date,
                                c.GasId,
                                c.Cost
                            }).Where(o => o.GasId == "B01").ToList();

                double? Ds4avg = 0, Gs4avg = 0;
                foreach(var i in Dcost)
                {
                    Ds4avg += i.Cost;
                }
                foreach(var j in Gcost)
                {
                    Gs4avg += j.Cost;
                }

                result.Add(new IncomeStatement());
                result[0].SaleGAS95 = SG;
                result[0].SaleDIESEL = SD;
                result[0].TotalSale = SG + SD;
                result[0].COGSGAS95 = COGSG;
                result[0].COGSDIESEL = COGSD;
                result[0].TotalCOGS = COGSG + COGSD;
                result[0].GrossProfit = result[0].TotalSale - result[0].TotalCOGS;
                result[0].SalarySOS = SSOS;
                result[0].SalaryGC = SGC;
                result[0].TotalSalary = SSOS + SGC;
                result[0].UtilityExp = UE;
                result[0].Depreciation = DP;
                result[0].NetIncome = result[0].GrossProfit - SSOS - SGC - UE - DP;
                result[0].BeginGAS95 = 40000 * Gcost[0].Cost;
                result[0].BeginDIESEL = 30000 * Dcost[0].Cost;
                result[0].PurchaseGAS95 = PG;
                result[0].PurchaseDIESEL = PD;
                result[0].EndGAS95 = 23332.8 * (Gs4avg/Dcost.Count());
                result[0].EndDIESEL = 35060.56 * (Ds4avg/Gcost.Count());

                return Ok(new { result = result, message = "success" });
            }
            catch (Exception ex)
            {
                return NotFound(new { result = ex, message = "fail" });
            }
        }


    }
}