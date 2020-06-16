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
    public class TruckTrackingController : ControllerBase
    {
        private readonly ILogger<TruckTrackingController> _logger;
        private readonly DatabaseContext _databaseContext;
        public TruckTrackingController(ILogger<TruckTrackingController> logger, DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
            _logger = logger;
        }

        [HttpGet("getSOData/Din/{SelDate}")]
        public IActionResult getSODataDateIn(DateTime SelDate)
        {
            try
            {
                // var _result = _databaseContext.SaleOfficeData.FromSqlRaw("SELECT * FROM _SaleOfficeData WHERE Date_In={0}",SelDate).ToList();
                var _result = (from s in _databaseContext.SaleOfficeData
                               join p in _databaseContext.Popaper
                               on s.PoNo equals p.PoNo
                               select new {
                                   s.DateIn,
                                   s.DateOut,
                                   s.TimeIn,
                                   s.TimeOut,
                                   s.PoNo,
                                   s.ServiceTime,
                                   p.TruckId
                               }).Where( o => o.DateIn == SelDate).ToList();
                return Ok( new{result=_result, message="success"});
            }
            catch (Exception ex)
            {
                return NotFound( new{result=ex, message="fail"});
            }
        }

        [HttpGet("getSOData/Dout/{SelDate}")]
        public IActionResult getSODataDateOut(DateTime SelDate)
        {
            try
            {
                // var _result = _databaseContext.SaleOfficeData.FromSqlRaw("SELECT * FROM _SaleOfficeData WHERE Date_Out={0}",SelDate).ToList();
                var _result = (from s in _databaseContext.SaleOfficeData
                               join p in _databaseContext.Popaper
                               on s.PoNo equals p.PoNo
                               select new {
                                   s.DateIn,
                                   s.DateOut,
                                   s.TimeIn,
                                   s.TimeOut,
                                   s.PoNo,
                                   s.ServiceTime,
                                   p.TruckId
                               }).Where( o => o.DateOut == SelDate).ToList();
                return Ok( new{result=_result, message="success"});
            }
            catch (Exception ex)
            {
                return NotFound( new{result=ex, message="fail"});
            }
        }

        [HttpGet("getSOData/Po/{POno}")]
        public IActionResult getSODataPO(string POno)
        {
            try
            {
                // var _result = _databaseContext.SaleOfficeData.FromSqlRaw($"SELECT* FROM _SaleOfficeData WHERE PO_no={POno}").ToList();
                var _result = (from s in _databaseContext.SaleOfficeData
                               join p in _databaseContext.Popaper
                               on s.PoNo equals p.PoNo
                               select new {
                                   s.DateIn,
                                   s.DateOut,
                                   s.TimeIn,
                                   s.TimeOut,
                                   s.PoNo,
                                   s.ServiceTime,
                                   p.TruckId
                               }).Where( o => o.PoNo == POno).ToList();

                return Ok( new{result=_result, message="success"});
            }
            catch (Exception ex)
            {
                return NotFound( new{result=ex, message="fail"});
            }
        }

        [HttpGet("getSOData/Tid/{id}")]
        public IActionResult getSODataTruckID(string id)
        {
            try
            {
                // var _result = _databaseContext.SaleOfficeData.FromSqlRaw("SELECT* FROM _SaleOfficeData WHERE Truck_ID={0}",id).ToList();
                var _result = (from s in _databaseContext.SaleOfficeData
                               join p in _databaseContext.Popaper
                               on s.PoNo equals p.PoNo
                               select new {
                                   s.DateIn,
                                   s.DateOut,
                                   s.TimeIn,
                                   s.TimeOut,
                                   s.PoNo,
                                   s.ServiceTime,
                                   p.TruckId
                               }).Where( o => o.TruckId == id).ToList();
                return Ok( new{result=_result, message="success"});
            }
            catch (Exception ex)
            {
                return NotFound( new{result=ex, message="fail"});
            }
        }

        [HttpGet("getIWBData/Din/{SelDate}")]
        public IActionResult getIWBDataDateIn(DateTime SelDate)
        {
            try
            {
                // var _result = _databaseContext.InboundWbdata.FromSqlRaw("SELECT * FROM _InboundWBData WHERE Date_In={0}",SelDate).ToList();
                var _result = (from i in _databaseContext.InboundWbdata
                               join p in _databaseContext.Popaper
                               on i.PoNo equals p.PoNo
                               select new {
                                   i.DateIn,
                                   i.DateOut,
                                   i.TimeIn,
                                   i.TimeOut,
                                   i.PoNo,
                                   i.ServiceTime,
                                   p.TruckId
                               }).Where( o => o.DateIn == SelDate).ToList();
                return Ok( new{result=_result, message="success"});
            }
            catch (Exception ex)
            {
                return NotFound( new{result=ex, message="fail"});
            }
        }

        [HttpGet("getIWBData/Dout/{SelDate}")]
        public IActionResult getIWBDataDateOut(DateTime SelDate)
        {
            try
            {
                // var _result = _databaseContext.InboundWbdata.FromSqlRaw("SELECT * FROM _InboundWBData WHERE Date_Out={0}",SelDate).ToList();
                var _result = (from i in _databaseContext.InboundWbdata
                               join p in _databaseContext.Popaper
                               on i.PoNo equals p.PoNo
                               select new {
                                   i.DateIn,
                                   i.DateOut,
                                   i.TimeIn,
                                   i.TimeOut,
                                   i.PoNo,
                                   i.ServiceTime,
                                   p.TruckId
                               }).Where( o => o.DateOut == SelDate).ToList();
                return Ok( new{result=_result, message="success"});
            }
            catch (Exception ex)
            {
                return NotFound( new{result=ex, message="fail"});
            }
        }

        [HttpGet("getIWBData/Po/{POno}")]
        public IActionResult getIWBDataPO(string POno)
        {
            try
            {
                // var _result = _databaseContext.InboundWbdata.FromSqlRaw("SELECT * FROM _InboundWBData WHERE PO_no={0}",POno).ToList();
                var _result = (from i in _databaseContext.InboundWbdata
                               join p in _databaseContext.Popaper
                               on i.PoNo equals p.PoNo
                               select new {
                                   i.DateIn,
                                   i.DateOut,
                                   i.TimeIn,
                                   i.TimeOut,
                                   i.PoNo,
                                   i.ServiceTime,
                                   p.TruckId
                               }).Where( o => o.PoNo == POno).ToList();
                return Ok( new{result=_result, message="success"});
            }
            catch (Exception ex)
            {
                return NotFound( new{result=ex, message="fail"});
            }
        }

        [HttpGet("getIWBData/Tid/{id}")]
        public IActionResult getIWBDataTruckID(string id)
        {
            try
            {
                // var _result = _databaseContext.InboundWbdata.FromSqlRaw("SELECT * FROM _InboundWBData WHERE Truck_ID={0}",id).ToList();
                var _result = (from i in _databaseContext.InboundWbdata
                               join p in _databaseContext.Popaper
                               on i.PoNo equals p.PoNo
                               select new {
                                   i.DateIn,
                                   i.DateOut,
                                   i.TimeIn,
                                   i.TimeOut,
                                   i.PoNo,
                                   i.ServiceTime,
                                   p.TruckId
                               }).Where( o => o.TruckId == id).ToList();
                return Ok( new{result=_result, message="success"});
            }
            catch (Exception ex)
            {
                return NotFound( new{result=ex, message="fail"});
            }
        }

        [HttpGet("getBayData/Din/{SelDate}")]
        public IActionResult getBayDataDateIn(DateTime SelDate)
        {
            try
            {
                // var _result = _databaseContext.BayData.FromSqlRaw("SELECT * FROM BayData WHERE Date_In={0}",SelDate).ToList();
                var _result = (from b in _databaseContext.BayData
                               join p in _databaseContext.Popaper on b.PoNo equals p.PoNo
                               join g in _databaseContext.Gas on p.Item equals g.GasId
                               select new {
                                   b.DateIn,
                                   b.DateOut,
                                   b.TimeIn,
                                   b.TimeOut,
                                   b.PoNo,
                                   b.ServiceTime,
                                   p.TruckId,
                                   g.GasType,
                                   p.Amount
                               }).Where( o => o.DateIn == SelDate).ToList();
                return Ok( new{result=_result, message="success"});
            }
            catch (Exception ex)
            {
                return NotFound( new{result=ex, message="fail"});
            }
        }

        [HttpGet("getBayData/Dout/{SelDate}")]
        public IActionResult getBayDataDateOut(DateTime SelDate)
        {
            try
            {
                // var _result = _databaseContext.BayData.FromSqlRaw("SELECT * FROM BayData WHERE Date_Out={0}",SelDate).ToList();
                var _result = (from b in _databaseContext.BayData
                               join p in _databaseContext.Popaper on b.PoNo equals p.PoNo
                               join g in _databaseContext.Gas on p.Item equals g.GasId
                               select new {
                                   b.DateIn,
                                   b.DateOut,
                                   b.TimeIn,
                                   b.TimeOut,
                                   b.PoNo,
                                   b.ServiceTime,
                                   p.TruckId,
                                   g.GasType,
                                   p.Amount
                               }).Where( o => o.DateOut == SelDate).ToList();
                return Ok( new{result=_result, message="success"});
            }
            catch (Exception ex)
            {
                return NotFound( new{result=ex, message="fail"});
            }
        }

        [HttpGet("getBayData/Po/{POno}")]
        public IActionResult getBayDataDatePO(string POno)
        {
            try
            {
                // var _result = _databaseContext.BayData.FromSqlRaw("SELECT * FROM _BayData WHERE PO_no={0}",POno).ToList();
                var _result = (from b in _databaseContext.BayData
                               join p in _databaseContext.Popaper on b.PoNo equals p.PoNo
                               join g in _databaseContext.Gas on p.Item equals g.GasId
                               select new {
                                   b.DateIn,
                                   b.DateOut,
                                   b.TimeIn,
                                   b.TimeOut,
                                   b.PoNo,
                                   b.ServiceTime,
                                   p.TruckId,
                                   g.GasType,
                                   p.Amount
                               }).Where( o => o.PoNo == POno).ToList();
                return Ok( new{result=_result, message="success"});
            }
            catch (Exception ex)
            {
                return NotFound( new{result=ex, message="fail"});
            }
        }

        [HttpGet("getBayData/Tid/{id}")]
        public IActionResult getBayDataTruckID(string id)
        {
            try
            {
                // var _result = _databaseContext.BayData.FromSqlRaw("SELECT * FROM _BayData WHERE Truck_ID={0}",id).ToList();
                var _result = (from b in _databaseContext.BayData
                               join p in _databaseContext.Popaper on b.PoNo equals p.PoNo
                               join g in _databaseContext.Gas on p.Item equals g.GasId
                               select new {
                                   b.DateIn,
                                   b.DateOut,
                                   b.TimeIn,
                                   b.TimeOut,
                                   b.PoNo,
                                   b.ServiceTime,
                                   p.TruckId,
                                   g.GasType,
                                   p.Amount
                               }).Where( o => o.TruckId == id).ToList();
                return Ok( new{result=_result, message="success"});
            }
            catch (Exception ex)
            {
                return NotFound( new{result=ex, message="fail"});
            }
        }

        [HttpGet("getOWBData/Din/{SelDate}")]
        public IActionResult getOWBDataDateIn(DateTime SelDate)
        {
            try
            {
                // var _result = _databaseContext.OutboundWbdata.FromSqlRaw("SELECT * FROM _OutboundWbdata WHERE Date_In={0}",SelDate).ToList();
                var _result = (from o in _databaseContext.OutboundWbdata
                               join p in _databaseContext.Popaper
                               on o.PoNo equals p.PoNo
                               select new {
                                   o.DateIn,
                                   o.DateOut,
                                   o.TimeIn,
                                   o.TimeOut,
                                   o.PoNo,
                                   o.ServiceTime,
                                   p.TruckId
                               }).Where( o => o.DateIn == SelDate).ToList();
                return Ok( new{result=_result, message="success"});
            }
            catch (Exception ex)
            {
                return NotFound( new{result=ex, message="fail"});
            }
        }

        [HttpGet("getOWBData/Dout/{SelDate}")]
        public IActionResult getOWBDataDateOut(DateTime SelDate)
        {
            try
            {
                // var _result = _databaseContext.OutboundWbdata.FromSqlRaw("SELECT * FROM _OutboundWbdata WHERE Date_Out={0}",SelDate).ToList();
                var _result = (from o in _databaseContext.OutboundWbdata
                               join p in _databaseContext.Popaper
                               on o.PoNo equals p.PoNo
                               select new {
                                   o.DateIn,
                                   o.DateOut,
                                   o.TimeIn,
                                   o.TimeOut,
                                   o.PoNo,
                                   o.ServiceTime,
                                   p.TruckId
                               }).Where( o => o.DateOut == SelDate).ToList();
                return Ok( new{result=_result, message="success"});
            }
            catch (Exception ex)
            {
                return NotFound( new{result=ex, message="fail"});
            }
        }

        [HttpGet("getOWBData/Po/{POno}")]
        public IActionResult getOWBDataPO(string POno)
        {
            try
            {
                // var _result = _databaseContext.OutboundWbdata.FromSqlRaw("SELECT * FROM _OutboundWBData WHERE PO_no={0}",POno).ToList();
                var _result = (from o in _databaseContext.OutboundWbdata
                               join p in _databaseContext.Popaper
                               on o.PoNo equals p.PoNo
                               select new {
                                   o.DateIn,
                                   o.DateOut,
                                   o.TimeIn,
                                   o.TimeOut,
                                   o.PoNo,
                                   o.ServiceTime,
                                   p.TruckId
                               }).Where( o => o.PoNo == POno).ToList();
                return Ok( new{result=_result, message="success"});
            }
            catch (Exception ex)
            {
                return NotFound( new{result=ex, message="fail"});
            }
        }

        [HttpGet("getOWBData/Tid/{id}")]
        public IActionResult getOWBDataTruckID(string id)
        {
            try
            {
                // var _result = _databaseContext.OutboundWbdata.FromSqlRaw("SELECT * FROM _OutboundWbdata WHERE Truck_ID={0}",id).ToList();
                var _result = (from o in _databaseContext.OutboundWbdata
                               join p in _databaseContext.Popaper
                               on o.PoNo equals p.PoNo
                               select new {
                                   o.DateIn,
                                   o.DateOut,
                                   o.TimeIn,
                                   o.TimeOut,
                                   o.PoNo,
                                   o.ServiceTime,
                                   p.TruckId
                               }).Where( o => o.TruckId == id).ToList();
                return Ok( new{result=_result, message="success"});
            }
            catch (Exception ex)
            {
                return NotFound( new{result=ex, message="fail"});
            }
        }

        [HttpGet("getEGData/Din/{SelDate}")]
        public IActionResult getEGDataDateIn(DateTime SelDate)
        {
            try
            {
                // var _result = _databaseContext.ExitGateData.FromSqlRaw("SELECT * FROM _ExitGateData WHERE Date_In={0}",SelDate).ToList();
                var _result = (from e in _databaseContext.ExitGateData
                               join p in _databaseContext.Popaper
                               on e.PoNo equals p.PoNo
                               select new {
                                   e.DateIn,
                                   e.TimeIn,
                                   e.PoNo,
                                   p.TruckId
                               }).Where( o => o.DateIn == SelDate).ToList();
                return Ok( new{result=_result, message="success"});
            }
            catch (Exception ex)
            {
                return NotFound( new{result=ex, message="fail"});
            }
        }

        [HttpGet("getEGData/Po/{POno}")]
        public IActionResult getEGDataPO(string POno)
        {
            try
            {
                // var _result = _databaseContext.ExitGateData.FromSqlRaw("SELECT * FROM _ExitGateData WHERE PO_no={0}",POno).ToList();
                var _result = (from e in _databaseContext.ExitGateData
                               join p in _databaseContext.Popaper
                               on e.PoNo equals p.PoNo
                               select new {
                                   e.DateIn,
                                   e.TimeIn,
                                   e.PoNo,
                                   p.TruckId
                               }).Where( o => o.PoNo == POno).ToList();
                return Ok( new{result=_result, message="success"});
            }
            catch (Exception ex)
            {
                return NotFound( new{result=ex, message="fail"});
            }
        }

        [HttpGet("getEGData/Tid/{id}")]
        public IActionResult getEGDataTruckID(string id)
        {
            try
            {
                // var _result = _databaseContext.ExitGateData.FromSqlRaw("SELECT * FROM _ExitGateData WHERE Truck_ID={0}",id).ToList();
                var _result = (from e in _databaseContext.ExitGateData
                               join p in _databaseContext.Popaper
                               on e.PoNo equals p.PoNo
                               select new {
                                   e.DateIn,
                                   e.TimeIn,
                                   e.PoNo,
                                   p.TruckId
                               }).Where( o => o.TruckId == id).ToList();
                return Ok( new{result=_result, message="success"});
            }
            catch (Exception ex)
            {
                return NotFound( new{result=ex, message="fail"});
            }
        }
    }
}