﻿using System;
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
    public class DailyDashboardController : ControllerBase
    {
        private readonly ILogger<DailyDashboardController> _logger;
        private readonly DatabaseContext _databaseContext;

        public DailyDashboardController(ILogger<DailyDashboardController> logger, DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
            _logger = logger;
        }

        // CRUD
        // Create
        // https://localhost:5001/api/postdata
        // [HttpPost("postdata")]
        // public IActionResult PostData(TagValue result)
        // {
        //     try
        //     {
        //         var results = _databaseContext.TagValue.Add(result);
        //         _databaseContext.SaveChanges();
        //         return Ok( new{result=result, message="success"});
        //     }
        //     catch (Exception ex)
        //     {
        //         return StatusCode(500, new{result=ex, message="fail"});
        //     }
        // }

        // Read
        // https://localhost:5001/api/getdata
        // [HttpGet("getdata")]
        // public IActionResult GetData()
        // {
        //     try
        //     {
        //         var tagValueData = _databaseContext.TagValue.ToList();
        //         return Ok( new{result=tagValueData, message="success"});
        //     }
        //     catch (Exception ex)
        //     {
        //         return NotFound( new{result=ex, message="fail"});
        //     }
        // }
        
        // [HttpGet]
        // public IEnumerable<WeatherForecast> Get()
        // {
        //     var rng = new Random();
        //     return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        //     {
        //         Date = DateTime.Now.AddDays(index),
        //         TemperatureC = rng.Next(-20, 55),
        //         Summary = Summaries[rng.Next(Summaries.Length)]
        //     })
        //     .ToArray();
        // }

        // Update
        // https://localhost:5001/api/updatedata
        // [HttpPut("updatedata")]
        // public async Task<IActionResult> UpdateData(TagValue result)
        // {
        //     try
        //     {
        //         var results = await _databaseContext.TagValue.SingleOrDefaultAsync(o => o.Id == result.Id);
        //         if(result != null)
        //         {
        //             // update value
        //             results.Value = result.Value;
        //             results.Tagname = result.Tagname;

        //             _databaseContext.Update(results);
        //             await _databaseContext.SaveChangesAsync();
        //         }
        //         return Ok( new{result=result, message="success"});
        //     }
        //     catch (Exception ex)
        //     {
        //         return NotFound( new{result=ex, message="fail"});
        //     }
        // }

        // Delete
        // https://localhost:5001/api/deletedata
        // [HttpDelete("deletedata/{tagname}")]
        // public IActionResult DeleteData(string tagname)
        // {
        //     try
        //     {
        //         var result = _databaseContext.TagValue.SingleOrDefault(o => o.Tagname == tagname);
        //         if(result != null)
        //         {
        //             _databaseContext.Remove(result);
        //             _databaseContext.SaveChanges();
        //         }
        //         return Ok( new{result=result, message="success"});
        //     }
        //     catch (Exception ex)
        //     {
        //         return NotFound( new{result=ex, message="fail"});
        //     }
        // }

        [HttpGet("getTT01Value")]
        public async Task<IActionResult> getTT01Value()
        {
            try
            {
                // HttpClientHandler clientHandler = ( new HttpClientHandler() { UseDefaultCredentials = true });
                // clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }; //access to https
                // HttpClient client = new HttpClient(clientHandler);
                var credentrials = new NetworkCredential("group2","inc.382");  // using username & password for login 
                HttpClientHandler clientHandler = new HttpClientHandler { Credentials = credentrials };
                clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }; // acess to https
                HttpClient client = new HttpClient(clientHandler);

                string TT01url = @"https://202.44.12.146/piwebapi/streams/F1AbEP9i6VrUz70i0bz0vbTQKhwArAGOnh16hGVqQAMKaRzTQCemkhmq4GUS67Jlev1VN-gMjAyLjQ0LjEyLjE0NlxHUk9VUDJfNlxGQUNUT1JZXEFSTTF8VFQtMDE/recorded?starttime=*-40d&endtime=*";

                HttpResponseMessage response = await client.GetAsync(TT01url);

                string content = await response.Content.ReadAsStringAsync();

                var data = (JArray)JObject.Parse(content)["Items"];
                var result = new JArray();

                foreach(var item in data)
                {
                    if(item["Good"].Value<bool>() == true)
                    {
                        var dataPair = new JObject();
                        dataPair.Add("Timestamp",item["Timestamp"].Value<string>());
                        dataPair.Add("value", item["Value"].Value<double>());
                        result.Add(dataPair);
                    }
                }

                return Ok(new {result=result, message="success"});
           } 
            catch (Exception ex)
            {
                return StatusCode(500, new{result=ex, message="fail"});
            }
        }


        [HttpGet("DIESELdailyFillVolVal/{selDate}")]
        public async Task<IActionResult> DIESELdailyFillVolVal(DateTime selDate)
        {
            try
            {
                var credentrials = new NetworkCredential("group2","inc.382");  // using username & password for login 
                HttpClientHandler clientHandler = new HttpClientHandler { Credentials = credentrials };
                clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }; // acess to https
                HttpClient client = new HttpClient(clientHandler);

                DateTime today = DateTime.Now;
                //DateTime seletedDate = new DateTime();
                TimeSpan value =  today.Subtract(selDate);
                
                string starttime = Convert.ToString(Convert.ToInt32(value.TotalDays));
                string endtime = Convert.ToString(Convert.ToInt32(value.TotalDays) - 1);

                string DDFVurl = $@"https://202.44.12.146/piwebapi/streams/F1AbEP9i6VrUz70i0bz0vbTQKhwOw4HFTt_6hG4hVTudaRgyQmlrltdQkjUKd9XFOXW_NVgMjAyLjQ0LjEyLjE0NlxHUk9VUDJfNFxGQUNUT1JZXFNBTEUgT0ZGSUNFfERBSUxZIEFNT1VOVCBGSUxMSU5HIFZPTFVNRSAoRElFU0VMKQ/recorded?starttime=*-{starttime}d&endtime=*-{endtime}d";

                HttpResponseMessage response = await client.GetAsync(DDFVurl);

                string content = await response.Content.ReadAsStringAsync();

                var data = (JArray)JObject.Parse(content)["Items"];
                var result = new JArray();

                foreach(var item in data)
                {
                    if(item["Good"].Value<bool>() == true)
                    {
                        var dataPair = new JObject();
                        dataPair.Add("Timestamp",item["Timestamp"].Value<string>());
                        dataPair.Add("value", item["Value"].Value<double>());
                        result.Add(dataPair);
                    }
                }

                return Ok(new {result=result, message="success"});
           } 
            catch (Exception ex)
            {
                return StatusCode(500, new{result=ex, message="fail"});
            }
        }

        [HttpGet("GAS95dailyFillVolVal/{selDate}")]
        public async Task<IActionResult> GAS95dailyFillVolVal(DateTime selDate)
        {
            try
            {
                var credentrials = new NetworkCredential("group2","inc.382");  // using username & password for login 
                HttpClientHandler clientHandler = new HttpClientHandler { Credentials = credentrials };
                clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }; // acess to https
                HttpClient client = new HttpClient(clientHandler);

                DateTime today = DateTime.Now;
                TimeSpan value =  today.Subtract(selDate);
                
                string starttime = Convert.ToString(Convert.ToInt32(value.TotalDays));
                string endtime = Convert.ToString(Convert.ToInt32(value.TotalDays) - 1);

                string url = $@"https://202.44.12.146/piwebapi/streams/F1AbEP9i6VrUz70i0bz0vbTQKhwOw4HFTt_6hG4hVTudaRgyQeEkyS9gKbkm7tuK-b_9ZmgMjAyLjQ0LjEyLjE0NlxHUk9VUDJfNFxGQUNUT1JZXFNBTEUgT0ZGSUNFfERBSUxZIEFNT1VOVCBGSUxMSU5HIFZPTFVNRSAoR0FTR0hPTDk1KQ/recorded?starttime=*-{starttime}d&endtime=*-{endtime}d";

                HttpResponseMessage response = await client.GetAsync(url);

                string content = await response.Content.ReadAsStringAsync();

                var data = (JArray)JObject.Parse(content)["Items"];
                var result = new JArray();

                foreach(var item in data)
                {
                    if(item["Good"].Value<bool>() == true)
                    {
                        var dataPair = new JObject();
                        dataPair.Add("Timestamp",item["Timestamp"].Value<string>());
                        dataPair.Add("value", item["Value"].Value<double>());
                        result.Add(dataPair);
                    }
                }

                return Ok(new {result=result, message="success"});
           } 
            catch (Exception ex)
            {
                return StatusCode(500, new{result=ex, message="fail"});
            }
        }
        
        [HttpGet("DIESELavgCycleTVal/{selDate}")]
        public async Task<IActionResult> DIESELavgCycleTVal(DateTime selDate)
        {
            try
            {
                var credentrials = new NetworkCredential("group2","inc.382");  // using username & password for login 
                HttpClientHandler clientHandler = new HttpClientHandler { Credentials = credentrials };
                clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }; // acess to https
                HttpClient client = new HttpClient(clientHandler);

                DateTime today = DateTime.Now;
                TimeSpan value =  today.Subtract(selDate);
                
                string starttime = Convert.ToString(Convert.ToInt32(value.TotalDays));
                string endtime = Convert.ToString(Convert.ToInt32(value.TotalDays) - 1);

                string url = $@"https://202.44.12.146/piwebapi/streams/F1AbEP9i6VrUz70i0bz0vbTQKhwny3Vcjt_6hG4hVTudaRgyQDxYI2MtknUekXOTzxhWuSAMjAyLjQ0LjEyLjE0NlxHUk9VUDJfNFxGQUNUT1JZXERJRVNFTCBCQVl8RElFU0VMIEJBWSBEQUlMWSBBVkVSQUdFIENZQ0xFIFRJTUU/recorded?starttime=*-{starttime}d&endtime=*-{endtime}d";

                HttpResponseMessage response = await client.GetAsync(url);

                string content = await response.Content.ReadAsStringAsync();

                var data = (JArray)JObject.Parse(content)["Items"];
                var result = new JArray();

                foreach(var item in data)
                {
                    if(item["Good"].Value<bool>() == true)
                    {
                        var dataPair = new JObject();
                        dataPair.Add("Timestamp",item["Timestamp"].Value<string>());
                        dataPair.Add("value", item["Value"].Value<double>());
                        result.Add(dataPair);
                    }
                }

                return Ok(new {result=result, message="success"});
           } 
            catch (Exception ex)
            {
                return StatusCode(500, new{result=ex, message="fail"});
            }
        }

        [HttpGet("GAS95avgCycleTVal/{selDate}")]
        public async Task<IActionResult> GAS95avgCycleTVal(DateTime selDate)
        {
            try
            {
                var credentrials = new NetworkCredential("group2","inc.382");  // using username & password for login 
                HttpClientHandler clientHandler = new HttpClientHandler { Credentials = credentrials };
                clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }; // acess to https
                HttpClient client = new HttpClient(clientHandler);

                DateTime today = DateTime.Now;
                TimeSpan value =  today.Subtract(selDate);
                
                string starttime = Convert.ToString(Convert.ToInt32(value.TotalDays));
                string endtime = Convert.ToString(Convert.ToInt32(value.TotalDays) - 1);

                string url = $@"https://202.44.12.146/piwebapi/streams/F1AbEP9i6VrUz70i0bz0vbTQKhwwBQnlDt_6hG4hVTudaRgyQqoYYOAfpfEiVOWL4oRoFxAMjAyLjQ0LjEyLjE0NlxHUk9VUDJfNFxGQUNUT1JZXEdBU09IT0w5NSBCQVl8R0FTT0hPTDk1IEJBWSBEQUlMWSBBVkVSQUdFIENZQ0xFIFRJTUU/recorded?starttime=*-{starttime}d&endtime=*-{endtime}d";

                HttpResponseMessage response = await client.GetAsync(url);

                string content = await response.Content.ReadAsStringAsync();

                var data = (JArray)JObject.Parse(content)["Items"];
                var result = new JArray();

                foreach(var item in data)
                {
                    if(item["Good"].Value<bool>() == true)
                    {
                        var dataPair = new JObject();
                        dataPair.Add("Timestamp",item["Timestamp"].Value<string>());
                        dataPair.Add("value", item["Value"].Value<double>());
                        result.Add(dataPair);
                    }
                }

                return Ok(new {result=result, message="success"});
           } 
            catch (Exception ex)
            {
                return StatusCode(500, new{result=ex, message="fail"});
            }
        }

        [HttpGet("SOdailyavgWaitTVal/{selDate}")]
        public async Task<IActionResult> SOdailyAvgWaitTVal(DateTime selDate)
        {
            try
            {
                var credentrials = new NetworkCredential("group2","inc.382");  // using username & password for login 
                HttpClientHandler clientHandler = new HttpClientHandler { Credentials = credentrials };
                clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }; // acess to https
                HttpClient client = new HttpClient(clientHandler);

                DateTime today = DateTime.Now;
                TimeSpan value =  today.Subtract(selDate);
                
                string starttime = Convert.ToString(Convert.ToInt32(value.TotalDays));
                string endtime = Convert.ToString(Convert.ToInt32(value.TotalDays) - 1);

                string url = $@"https://202.44.12.146/piwebapi/streams/F1AbEP9i6VrUz70i0bz0vbTQKhwOw4HFTt_6hG4hVTudaRgyQtMZrpKeMDke_MQ-2GaHVHgMjAyLjQ0LjEyLjE0NlxHUk9VUDJfNFxGQUNUT1JZXFNBTEUgT0ZGSUNFfFNBTEUgT0ZGSUNFIERBSUxZIEFWRVJBR0UgV0FJVElORyBUSU1F/recorded?starttime=*-{starttime}d&endtime=*-{endtime}d";

                HttpResponseMessage response = await client.GetAsync(url);

                string content = await response.Content.ReadAsStringAsync();

                var data = (JArray)JObject.Parse(content)["Items"];
                var result = new JArray();

                foreach(var item in data)
                {
                    if(item["Good"].Value<bool>() == true)
                    {
                        var dataPair = new JObject();
                        dataPair.Add("Timestamp",item["Timestamp"].Value<string>());
                        dataPair.Add("value", item["Value"].Value<double>());
                        result.Add(dataPair);
                    }
                }

                return Ok(new {result=result, message="success"});
           } 
            catch (Exception ex)
            {
                return StatusCode(500, new{result=ex, message="fail"});
            }
        }

        [HttpGet("IWBdailyAvgWaitTVal/{selDate}")]
        public async Task<IActionResult> IWBdailyAvgWaitTVal(DateTime selDate)
        {
            try
            {
                var credentrials = new NetworkCredential("group2","inc.382");  // using username & password for login 
                HttpClientHandler clientHandler = new HttpClientHandler { Credentials = credentrials };
                clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }; // acess to https
                HttpClient client = new HttpClient(clientHandler);

                DateTime today = DateTime.Now;
                TimeSpan value =  today.Subtract(selDate);
                
                string starttime = Convert.ToString(Convert.ToInt32(value.TotalDays));
                string endtime = Convert.ToString(Convert.ToInt32(value.TotalDays) - 1);

                string url = $@"https://202.44.12.146/piwebapi/streams/F1AbEP9i6VrUz70i0bz0vbTQKhw3QL6Mjt_6hG4hVTudaRgyQvWLBAFJ2TUWlf-WK9vpFHwMjAyLjQ0LjEyLjE0NlxHUk9VUDJfNFxGQUNUT1JZXElOQk9VTkQgV0VJR0hUQlJJREdFfElOQk9VTkQgV0VJR0hCUklER0UgREFJTFkgQVZFUkFHRSBXQUlUSU5HIFRJTUU/recorded?starttime=*-{starttime}d&endtime=*-{endtime}d";

                HttpResponseMessage response = await client.GetAsync(url);

                string content = await response.Content.ReadAsStringAsync();

                var data = (JArray)JObject.Parse(content)["Items"];
                var result = new JArray();

                foreach(var item in data)
                {
                    if(item["Good"].Value<bool>() == true)
                    {
                        var dataPair = new JObject();
                        dataPair.Add("Timestamp",item["Timestamp"].Value<string>());
                        dataPair.Add("value", item["Value"].Value<double>());
                        result.Add(dataPair);
                    }
                }

                return Ok(new {result=result, message="success"});
           } 
            catch (Exception ex)
            {
                return StatusCode(500, new{result=ex, message="fail"});
            }
        }

        [HttpGet("DIESELdailyAvgWaitTVal/{selDate}")]
        public async Task<IActionResult> DIESELdailyAvgWaitTVal(DateTime selDate)
        {
            try
            {
                var credentrials = new NetworkCredential("group2","inc.382");  // using username & password for login 
                HttpClientHandler clientHandler = new HttpClientHandler { Credentials = credentrials };
                clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }; // acess to https
                HttpClient client = new HttpClient(clientHandler);

                DateTime today = DateTime.Now;
                TimeSpan value =  today.Subtract(selDate);
                
                string starttime = Convert.ToString(Convert.ToInt32(value.TotalDays));
                string endtime = Convert.ToString(Convert.ToInt32(value.TotalDays) - 1);

                string url = $@"https://202.44.12.146/piwebapi/streams/F1AbEP9i6VrUz70i0bz0vbTQKhwny3Vcjt_6hG4hVTudaRgyQEgikTfxvRESnesr4fjxtxwMjAyLjQ0LjEyLjE0NlxHUk9VUDJfNFxGQUNUT1JZXERJRVNFTCBCQVl8RElFU0VMIEJBWSBEQUlMWSBBVkVSQUdFIFdBSVRJTkcgVElNRQ/recorded?starttime=*-{starttime}d&endtime=*-{endtime}d";

                HttpResponseMessage response = await client.GetAsync(url);

                string content = await response.Content.ReadAsStringAsync();

                var data = (JArray)JObject.Parse(content)["Items"];
                var result = new JArray();

                foreach(var item in data)
                {
                    if(item["Good"].Value<bool>() == true)
                    {
                        var dataPair = new JObject();
                        dataPair.Add("Timestamp",item["Timestamp"].Value<string>());
                        dataPair.Add("value", item["Value"].Value<double>());
                        result.Add(dataPair);
                    }
                }

                return Ok(new {result=result, message="success"});
           } 
            catch (Exception ex)
            {
                return StatusCode(500, new{result=ex, message="fail"});
            }
        }

        [HttpGet("GAS95dailyAvgWaitTVal/{selDate}")]
        public async Task<IActionResult> GAS95dailyAvgWaitTVal(DateTime selDate)
        {
            try
            {
                var credentrials = new NetworkCredential("group2","inc.382");  // using username & password for login 
                HttpClientHandler clientHandler = new HttpClientHandler { Credentials = credentrials };
                clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }; // acess to https
                HttpClient client = new HttpClient(clientHandler);

                DateTime today = DateTime.Now;
                TimeSpan value =  today.Subtract(selDate);
                
                string starttime = Convert.ToString(Convert.ToInt32(value.TotalDays));
                string endtime = Convert.ToString(Convert.ToInt32(value.TotalDays) - 1);

                string url = $@"https://202.44.12.146/piwebapi/streams/F1AbEP9i6VrUz70i0bz0vbTQKhwwBQnlDt_6hG4hVTudaRgyQEUq7d1pTYkeLV6ga4uj7VQMjAyLjQ0LjEyLjE0NlxHUk9VUDJfNFxGQUNUT1JZXEdBU09IT0w5NSBCQVl8R0FTT0hPTDk1IEJBWSBEQUlMWSBBVkVSQUdFIFdBSVRJTkcgVElNRQ/recorded?starttime=*-{starttime}d&endtime=*-{endtime}d";

                HttpResponseMessage response = await client.GetAsync(url);

                string content = await response.Content.ReadAsStringAsync();

                var data = (JArray)JObject.Parse(content)["Items"];
                var result = new JArray();

                foreach(var item in data)
                {
                    if(item["Good"].Value<bool>() == true)
                    {
                        var dataPair = new JObject();
                        dataPair.Add("Timestamp",item["Timestamp"].Value<string>());
                        dataPair.Add("value", item["Value"].Value<double>());
                        result.Add(dataPair);
                    }
                }

                return Ok(new {result=result, message="success"});
           } 
            catch (Exception ex)
            {
                return StatusCode(500, new{result=ex, message="fail"});
            }
        }

        [HttpGet("OWBdailyAvgWaitTVal/{selDate}")]
        public async Task<IActionResult> OWBdailyAvgWaitTVal(DateTime selDate)
        {
            try
            {
                var credentrials = new NetworkCredential("group2","inc.382");  // using username & password for login 
                HttpClientHandler clientHandler = new HttpClientHandler { Credentials = credentrials };
                clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }; // acess to https
                HttpClient client = new HttpClient(clientHandler);

                DateTime today = DateTime.Now;
                TimeSpan value =  today.Subtract(selDate);
                
                string starttime = Convert.ToString(Convert.ToInt32(value.TotalDays));
                string endtime = Convert.ToString(Convert.ToInt32(value.TotalDays) - 1);

                string url = $@"https://202.44.12.146/piwebapi/streams/F1AbEP9i6VrUz70i0bz0vbTQKhwpSJFnjt_6hG4hVTudaRgyQqbtERL5W3U-gwc7iicbzjwMjAyLjQ0LjEyLjE0NlxHUk9VUDJfNFxGQUNUT1JZXE9VVEJPVU5EIFdFSUdIVEJSSURHRXxPVVRCT1VORCBXRUlHSEJSSURHRSBEQUlMWSBBVkVSQUdFIFdBSVRJTkcgVElNRQ/recorded?starttime=*-{starttime}d&endtime=*-{endtime}d";

                HttpResponseMessage response = await client.GetAsync(url);

                string content = await response.Content.ReadAsStringAsync();

                var data = (JArray)JObject.Parse(content)["Items"];
                var result = new JArray();

                foreach(var item in data)
                {
                    if(item["Good"].Value<bool>() == true)
                    {
                        var dataPair = new JObject();
                        dataPair.Add("Timestamp",item["Timestamp"].Value<string>());
                        dataPair.Add("value", item["Value"].Value<double>());
                        result.Add(dataPair);
                    }
                }

                return Ok(new {result=result, message="success"});
           } 
            catch (Exception ex)
            {
                return StatusCode(500, new{result=ex, message="fail"});
            }
        }

        [HttpGet("dailyTruckInVal/{selDate}")]
        public async Task<IActionResult> dailyTruckInVal(DateTime selDate)
        {
            try
            {
                var credentrials = new NetworkCredential("group2","inc.382");  // using username & password for login 
                HttpClientHandler clientHandler = new HttpClientHandler { Credentials = credentrials };
                clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }; // acess to https
                HttpClient client = new HttpClient(clientHandler);

                DateTime today = DateTime.Now;
                TimeSpan value =  today.Subtract(selDate);
                
                string starttime = Convert.ToString(Convert.ToInt32(value.TotalDays));
                string endtime = Convert.ToString(Convert.ToInt32(value.TotalDays) - 1);

                string url = $@"https://202.44.12.146/piwebapi/streams/F1AbEP9i6VrUz70i0bz0vbTQKhwOw4HFTt_6hG4hVTudaRgyQL_OscsyLX0eWtxZ14Ua6xwMjAyLjQ0LjEyLjE0NlxHUk9VUDJfNFxGQUNUT1JZXFNBTEUgT0ZGSUNFfERBSUxZIE5VTUJFUiBPRiBUUlVDS1MgSU4/recorded?starttime=*-{starttime}d&endtime=*-{endtime}d";

                HttpResponseMessage response = await client.GetAsync(url);

                string content = await response.Content.ReadAsStringAsync();

                var data = (JArray)JObject.Parse(content)["Items"];
                var result = new JArray();

                foreach(var item in data)
                {
                    if(item["Good"].Value<bool>() == true)
                    {
                        var dataPair = new JObject();
                        dataPair.Add("Timestamp",item["Timestamp"].Value<string>());
                        dataPair.Add("value", item["Value"].Value<double>());
                        result.Add(dataPair);
                    }
                }

                return Ok(new {result=result, message="success"});
           } 
            catch (Exception ex)
            {
                return StatusCode(500, new{result=ex, message="fail"});
            }
        }

        [HttpGet("dailyTruckOutVal/{selDate}")]
        public async Task<IActionResult> dailyTruckOutVal(DateTime selDate)
        {
            try
            {
                var credentrials = new NetworkCredential("group2","inc.382");  // using username & password for login 
                HttpClientHandler clientHandler = new HttpClientHandler { Credentials = credentrials };
                clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }; // acess to https
                HttpClient client = new HttpClient(clientHandler);

                DateTime today = DateTime.Now;
                TimeSpan value =  today.Subtract(selDate);
                
                string starttime = Convert.ToString(Convert.ToInt32(value.TotalDays));
                string endtime = Convert.ToString(Convert.ToInt32(value.TotalDays) - 1);

                string url = $@"https://202.44.12.146/piwebapi/streams/F1AbEP9i6VrUz70i0bz0vbTQKhw0f0eqzt_6hG4hVTudaRgyQakbfH4mtk0K7bfkI2WqtEgMjAyLjQ0LjEyLjE0NlxHUk9VUDJfNFxGQUNUT1JZXEVYSVQgR0FURXxEQUlMWSBOVU1CRVIgT0YgVFJVQ0tTIE9VVA/recorded?starttime=*-{starttime}d&endtime=*-{endtime}d";

                HttpResponseMessage response = await client.GetAsync(url);

                string content = await response.Content.ReadAsStringAsync();

                var data = (JArray)JObject.Parse(content)["Items"];
                var result = new JArray();

                foreach(var item in data)
                {
                    if(item["Good"].Value<bool>() == true)
                    {
                        var dataPair = new JObject();
                        dataPair.Add("Timestamp",item["Timestamp"].Value<string>());
                        dataPair.Add("value", item["Value"].Value<double>());
                        result.Add(dataPair);
                    }
                }

                return Ok(new {result=result, message="success"});
           } 
            catch (Exception ex)
            {
                return StatusCode(500, new{result=ex, message="fail"});
            }
        }

        [HttpGet("SOdailyAvgQVal/{selDate}")]
        public async Task<IActionResult> SOdailyAvgQVal(DateTime selDate)
        {
            try
            {
                var credentrials = new NetworkCredential("group2","inc.382");  // using username & password for login 
                HttpClientHandler clientHandler = new HttpClientHandler { Credentials = credentrials };
                clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }; // acess to https
                HttpClient client = new HttpClient(clientHandler);

                DateTime today = DateTime.Now;
                TimeSpan value =  today.Subtract(selDate);
                
                string starttime = Convert.ToString(Convert.ToInt32(value.TotalDays));
                string endtime = Convert.ToString(Convert.ToInt32(value.TotalDays) - 1);

                string url = $@"https://202.44.12.146/piwebapi/streams/F1AbEP9i6VrUz70i0bz0vbTQKhwOw4HFTt_6hG4hVTudaRgyQX_mCw5ZM70q7toyh3At2mwMjAyLjQ0LjEyLjE0NlxHUk9VUDJfNFxGQUNUT1JZXFNBTEUgT0ZGSUNFfFNBTEUgT0ZGSUNFIERBSUxZIEFWRVJBR0UgTlVNQkVSIE9GIFFVRVVF/recorded?starttime=*-{starttime}d&endtime=*-{endtime}d";

                HttpResponseMessage response = await client.GetAsync(url);

                string content = await response.Content.ReadAsStringAsync();

                var data = (JArray)JObject.Parse(content)["Items"];
                var result = new JArray();

                foreach(var item in data)
                {
                    if(item["Good"].Value<bool>() == true)
                    {
                        var dataPair = new JObject();
                        dataPair.Add("Timestamp",item["Timestamp"].Value<string>());
                        dataPair.Add("value", item["Value"].Value<double>());
                        result.Add(dataPair);
                    }
                }

                return Ok(new {result=result, message="success"});
           } 
            catch (Exception ex)
            {
                return StatusCode(500, new{result=ex, message="fail"});
            }
        }

        [HttpGet("IWBdailyAvgQVal/{selDate}")]
        public async Task<IActionResult> IWBdailyAvgQVal(DateTime selDate)
        {
            try
            {
                var credentrials = new NetworkCredential("group2","inc.382");  // using username & password for login 
                HttpClientHandler clientHandler = new HttpClientHandler { Credentials = credentrials };
                clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }; // acess to https
                HttpClient client = new HttpClient(clientHandler);

                DateTime today = DateTime.Now;
                TimeSpan value =  today.Subtract(selDate);
                
                string starttime = Convert.ToString(Convert.ToInt32(value.TotalDays));
                string endtime = Convert.ToString(Convert.ToInt32(value.TotalDays) - 1);

                string url = $@"https://202.44.12.146/piwebapi/streams/F1AbEP9i6VrUz70i0bz0vbTQKhw3QL6Mjt_6hG4hVTudaRgyQvBfMURpMYkaVbKFKIpaIuQMjAyLjQ0LjEyLjE0NlxHUk9VUDJfNFxGQUNUT1JZXElOQk9VTkQgV0VJR0hUQlJJREdFfElOQk9VTkQgV0VJR0hCUklER0UgREFJTFkgQVZFUkFHRSBOVU1CRVIgT0YgUVVFVUU/recorded?starttime=*-{starttime}d&endtime=*-{endtime}d";

                HttpResponseMessage response = await client.GetAsync(url);

                string content = await response.Content.ReadAsStringAsync();

                var data = (JArray)JObject.Parse(content)["Items"];
                var result = new JArray();

                foreach(var item in data)
                {
                    if(item["Good"].Value<bool>() == true)
                    {
                        var dataPair = new JObject();
                        dataPair.Add("Timestamp",item["Timestamp"].Value<string>());
                        dataPair.Add("value", item["Value"].Value<double>());
                        result.Add(dataPair);
                    }
                }

                return Ok(new {result=result, message="success"});
           } 
            catch (Exception ex)
            {
                return StatusCode(500, new{result=ex, message="fail"});
            }
        }

        [HttpGet("DIESELdailyAvgQVal/{selDate}")]
        public async Task<IActionResult> DIESELdailyAvgQVal(DateTime selDate)
        {
            try
            {
                var credentrials = new NetworkCredential("group2","inc.382");  // using username & password for login 
                HttpClientHandler clientHandler = new HttpClientHandler { Credentials = credentrials };
                clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }; // acess to https
                HttpClient client = new HttpClient(clientHandler);

                DateTime today = DateTime.Now;
                TimeSpan value =  today.Subtract(selDate);
                
                string starttime = Convert.ToString(Convert.ToInt32(value.TotalDays));
                string endtime = Convert.ToString(Convert.ToInt32(value.TotalDays) - 1);

                string url = $@"https://202.44.12.146/piwebapi/streams/F1AbEP9i6VrUz70i0bz0vbTQKhwny3Vcjt_6hG4hVTudaRgyQyyGzd5Z_B0Sf7C0YpVl5sAMjAyLjQ0LjEyLjE0NlxHUk9VUDJfNFxGQUNUT1JZXERJRVNFTCBCQVl8RElFU0VMIEJBWSBEQUlMWSBBVkVSQUdFIE5VTUJFUiBPRiBRVUVVRQ/recorded?starttime=*-{starttime}d&endtime=*-{endtime}d";

                HttpResponseMessage response = await client.GetAsync(url);

                string content = await response.Content.ReadAsStringAsync();

                var data = (JArray)JObject.Parse(content)["Items"];
                var result = new JArray();

                foreach(var item in data)
                {
                    if(item["Good"].Value<bool>() == true)
                    {
                        var dataPair = new JObject();
                        dataPair.Add("Timestamp",item["Timestamp"].Value<string>());
                        dataPair.Add("value", item["Value"].Value<double>());
                        result.Add(dataPair);
                    }
                }

                return Ok(new {result=result, message="success"});
           } 
            catch (Exception ex)
            {
                return StatusCode(500, new{result=ex, message="fail"});
            }
        }

        [HttpGet("GAS95dailyAvgQVal/{selDate}")]
        public async Task<IActionResult> GAS95dailyAvgQVal(DateTime selDate)
        {
            try
            {
                var credentrials = new NetworkCredential("group2","inc.382");  // using username & password for login 
                HttpClientHandler clientHandler = new HttpClientHandler { Credentials = credentrials };
                clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }; // acess to https
                HttpClient client = new HttpClient(clientHandler);

                DateTime today = DateTime.Now;
                TimeSpan value =  today.Subtract(selDate);

                string starttime = Convert.ToString(Convert.ToInt32(value.TotalDays));
                string endtime = Convert.ToString(Convert.ToInt32(value.TotalDays) - 1);

                string url = $@"https://202.44.12.146/piwebapi/streams/F1AbEP9i6VrUz70i0bz0vbTQKhwwBQnlDt_6hG4hVTudaRgyQ_-itW4CBpECG0I1jd--QLAMjAyLjQ0LjEyLjE0NlxHUk9VUDJfNFxGQUNUT1JZXEdBU09IT0w5NSBCQVl8R0FTT0hPTDk1IEJBWSBEQUlMWSBBVkVSQUdFIE5VTUJFUiBPRiBRVUVVRQ/recorded?starttime=*-{starttime}d&endtime=*-{endtime}d";

                HttpResponseMessage response = await client.GetAsync(url);

                string content = await response.Content.ReadAsStringAsync();

                var data = (JArray)JObject.Parse(content)["Items"];
                var result = new JArray();

                foreach(var item in data)
                {
                    if(item["Good"].Value<bool>() == true)
                    {
                        var dataPair = new JObject();
                        dataPair.Add("Timestamp",item["Timestamp"].Value<string>());
                        dataPair.Add("value", item["Value"].Value<double>());
                        result.Add(dataPair);
                    }
                }

                return Ok(new {result=result, message="success"});
            } 
            catch (Exception ex)
            {
                return StatusCode(500, new{result=ex, message="fail"});
            }
        }

        [HttpGet("OWBdailyAvgQVal/{selDate}")]
        public async Task<IActionResult> OWBdailyAvgQVal(DateTime selDate)
        {
            try
            {
                var credentrials = new NetworkCredential("group2","inc.382");  // using username & password for login 
                HttpClientHandler clientHandler = new HttpClientHandler { Credentials = credentrials };
                clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }; // acess to https
                HttpClient client = new HttpClient(clientHandler);

                DateTime today = DateTime.Now;
                TimeSpan value =  today.Subtract(selDate);
                
                string starttime = Convert.ToString(Convert.ToInt32(value.TotalDays));
                string endtime = Convert.ToString(Convert.ToInt32(value.TotalDays) - 1);

                string url = $@"https://202.44.12.146/piwebapi/streams/F1AbEP9i6VrUz70i0bz0vbTQKhwpSJFnjt_6hG4hVTudaRgyQDQepp_AFMU6McMb3i-j7dgMjAyLjQ0LjEyLjE0NlxHUk9VUDJfNFxGQUNUT1JZXE9VVEJPVU5EIFdFSUdIVEJSSURHRXxPVVRCT1VORCBXRUlHSEJSSURHRSBEQUlMWSBBVkVSQUdFIE5VTUJFUiBPRiBRVUVVRQ/recorded?starttime=*-{starttime}d&endtime=*-{endtime}d";

                HttpResponseMessage response = await client.GetAsync(url);

                string content = await response.Content.ReadAsStringAsync();

                var data = (JArray)JObject.Parse(content)["Items"];
                var result = new JArray();

                foreach(var item in data)
                {
                    if(item["Good"].Value<bool>() == true)
                    {
                        var dataPair = new JObject();
                        dataPair.Add("Timestamp",item["Timestamp"].Value<string>());
                        dataPair.Add("value", item["Value"].Value<double>());
                        result.Add(dataPair);
                    }
                }

                return Ok(new {result=result, message="success"});
           } 
            catch (Exception ex)
            {
                return StatusCode(500, new{result=ex, message="fail"});
            }
        }

        [HttpGet("DIESELdailyAvgWIPVal/{selDate}")]
        public async Task<IActionResult> DIESELdailyAvgWIPVal(DateTime selDate)
        {
            try
            {
                var credentrials = new NetworkCredential("group2","inc.382");  // using username & password for login 
                HttpClientHandler clientHandler = new HttpClientHandler { Credentials = credentrials };
                clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }; // acess to https
                HttpClient client = new HttpClient(clientHandler);

                DateTime today = DateTime.Now;
                TimeSpan value =  today.Subtract(selDate);
                
                string starttime = Convert.ToString(Convert.ToInt32(value.TotalDays));
                string endtime = Convert.ToString(Convert.ToInt32(value.TotalDays) - 1);

                string url = $@"https://202.44.12.146/piwebapi/streams/F1AbEP9i6VrUz70i0bz0vbTQKhwny3Vcjt_6hG4hVTudaRgyQ3g2In2D_20mTm96BjY_jwwMjAyLjQ0LjEyLjE0NlxHUk9VUDJfNFxGQUNUT1JZXERJRVNFTCBCQVl8RElFU0VMIEJBWSBEQUlMWSBBVkVSQUdFIFdJUA/recorded?starttime=*-{starttime}d&endtime=*-{endtime}d";

                HttpResponseMessage response = await client.GetAsync(url);

                string content = await response.Content.ReadAsStringAsync();

                var data = (JArray)JObject.Parse(content)["Items"];
                var result = new JArray();

                foreach(var item in data)
                {
                    if(item["Good"].Value<bool>() == true)
                    {
                        var dataPair = new JObject();
                        dataPair.Add("Timestamp",item["Timestamp"].Value<string>());
                        dataPair.Add("value", item["Value"].Value<double>());
                        result.Add(dataPair);
                    }
                }

                return Ok(new {result=result, message="success"});
           } 
            catch (Exception ex)
            {
                return StatusCode(500, new{result=ex, message="fail"});
            }
        }

        [HttpGet("GAS95dailyAvgWIPVal/{selDate}")]
        public async Task<IActionResult> GAS95dailyAvgWIPVal(DateTime selDate)
        {
            try
            {
                var credentrials = new NetworkCredential("group2","inc.382");  // using username & password for login 
                HttpClientHandler clientHandler = new HttpClientHandler { Credentials = credentrials };
                clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }; // acess to https
                HttpClient client = new HttpClient(clientHandler);

                DateTime today = DateTime.Now;
                TimeSpan value =  today.Subtract(selDate);
                
                string starttime = Convert.ToString(Convert.ToInt32(value.TotalDays));
                string endtime = Convert.ToString(Convert.ToInt32(value.TotalDays) - 1);

                string url = $@"https://202.44.12.146/piwebapi/streams/F1AbEP9i6VrUz70i0bz0vbTQKhwwBQnlDt_6hG4hVTudaRgyQSBstWfe-f0qNBPRMLUJ0wQMjAyLjQ0LjEyLjE0NlxHUk9VUDJfNFxGQUNUT1JZXEdBU09IT0w5NSBCQVl8R0FTT0hPTDk1IEJBWSBEQUlMWSBBVkVSQUdFIFdJUA/recorded?starttime=*-{starttime}d&endtime=*-{endtime}d";

                HttpResponseMessage response = await client.GetAsync(url);

                string content = await response.Content.ReadAsStringAsync();

                var data = (JArray)JObject.Parse(content)["Items"];
                var result = new JArray();

                foreach(var item in data)
                {
                    if(item["Good"].Value<bool>() == true)
                    {
                        var dataPair = new JObject();
                        dataPair.Add("Timestamp",item["Timestamp"].Value<string>());
                        dataPair.Add("value", item["Value"].Value<double>());
                        result.Add(dataPair);
                    }
                }

                return Ok(new {result=result, message="success"});
           } 
            catch (Exception ex)
            {
                return StatusCode(500, new{result=ex, message="fail"});
            }
        }

         [HttpGet("SOdailyAvgSchUVal/{selDate}")]
        public async Task<IActionResult> SOdailyAvgSchUVal(DateTime selDate)
        {
            try
            {
                var credentrials = new NetworkCredential("group2","inc.382");  // using username & password for login 
                HttpClientHandler clientHandler = new HttpClientHandler { Credentials = credentrials };
                clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }; // acess to https
                HttpClient client = new HttpClient(clientHandler);

                DateTime today = DateTime.Now;
                TimeSpan value =  today.Subtract(selDate);
                
                string starttime = Convert.ToString(Convert.ToInt32(value.TotalDays));
                string endtime = Convert.ToString(Convert.ToInt32(value.TotalDays) - 1);

                string url = $@"https://202.44.12.146/piwebapi/streams/F1AbEP9i6VrUz70i0bz0vbTQKhwOw4HFTt_6hG4hVTudaRgyQgwkApjFuLEm5dHSOTpMV4gMjAyLjQ0LjEyLjE0NlxHUk9VUDJfNFxGQUNUT1JZXFNBTEUgT0ZGSUNFfFNBTEUgT0ZGSUNFIERBSUxZIEFWRVJBR0UgU0NIX1VUSUxJWkFUSU9O/recorded?starttime=*-{starttime}d&endtime=*-{endtime}d";

                HttpResponseMessage response = await client.GetAsync(url);

                string content = await response.Content.ReadAsStringAsync();

                var data = (JArray)JObject.Parse(content)["Items"];
                var result = new JArray();

                foreach(var item in data)
                {
                    if(item["Good"].Value<bool>() == true)
                    {
                        var dataPair = new JObject();
                        dataPair.Add("Timestamp",item["Timestamp"].Value<string>());
                        dataPair.Add("value", item["Value"].Value<double>());
                        result.Add(dataPair);
                    }
                }

                return Ok(new {result=result, message="success"});
           } 
            catch (Exception ex)
            {
                return StatusCode(500, new{result=ex, message="fail"});
            }
        }

        [HttpGet("IWBdailyAvgSchUVal/{selDate}")]
        public async Task<IActionResult> IWBdailyAvgSchUVal(DateTime selDate)
        {
            try
            {
                var credentrials = new NetworkCredential("group2","inc.382");  // using username & password for login 
                HttpClientHandler clientHandler = new HttpClientHandler { Credentials = credentrials };
                clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }; // acess to https
                HttpClient client = new HttpClient(clientHandler);

                DateTime today = DateTime.Now;
                TimeSpan value =  today.Subtract(selDate);
                
                string starttime = Convert.ToString(Convert.ToInt32(value.TotalDays));
                string endtime = Convert.ToString(Convert.ToInt32(value.TotalDays) - 1);

                string url = $@"https://202.44.12.146/piwebapi/streams/F1AbEP9i6VrUz70i0bz0vbTQKhw3QL6Mjt_6hG4hVTudaRgyQuGl69bRgKkGm2_WTBtwKkgMjAyLjQ0LjEyLjE0NlxHUk9VUDJfNFxGQUNUT1JZXElOQk9VTkQgV0VJR0hUQlJJREdFfElOQk9VTkQgV0VJR0hCUklER0UgREFJTFkgQVZFUkFHRSBTQ0hfVVRJTElaQVRJT04/recorded?starttime=*-{starttime}d&endtime=*-{endtime}d";

                HttpResponseMessage response = await client.GetAsync(url);

                string content = await response.Content.ReadAsStringAsync();

                var data = (JArray)JObject.Parse(content)["Items"];
                var result = new JArray();

                foreach(var item in data)
                {
                    if(item["Good"].Value<bool>() == true)
                    {
                        var dataPair = new JObject();
                        dataPair.Add("Timestamp",item["Timestamp"].Value<string>());
                        dataPair.Add("value", item["Value"].Value<double>());
                        result.Add(dataPair);
                    }
                }

                return Ok(new {result=result, message="success"});
           } 
            catch (Exception ex)
            {
                return StatusCode(500, new{result=ex, message="fail"});
            }
        }

        [HttpGet("DIESELdailyAvgSchUVal/{selDate}")]
        public async Task<IActionResult> DIESELdailyAvgSchUVal(DateTime selDate)
        {
            try
            {
                var credentrials = new NetworkCredential("group2","inc.382");  // using username & password for login 
                HttpClientHandler clientHandler = new HttpClientHandler { Credentials = credentrials };
                clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }; // acess to https
                HttpClient client = new HttpClient(clientHandler);

                DateTime today = DateTime.Now;
                TimeSpan value =  today.Subtract(selDate);
                
                string starttime = Convert.ToString(Convert.ToInt32(value.TotalDays));
                string endtime = Convert.ToString(Convert.ToInt32(value.TotalDays) - 1);

                string url = $@"https://202.44.12.146/piwebapi/streams/F1AbEP9i6VrUz70i0bz0vbTQKhwny3Vcjt_6hG4hVTudaRgyQHT_vByThs0uqV9drwYFHNwMjAyLjQ0LjEyLjE0NlxHUk9VUDJfNFxGQUNUT1JZXERJRVNFTCBCQVl8RElFU0VMIEJBWSBEQUlMWSBBVkVSQUdFIFNDSF9VVElMSVpBVElPTg/recorded?starttime=*-{starttime}d&endtime=*-{endtime}d";

                HttpResponseMessage response = await client.GetAsync(url);

                string content = await response.Content.ReadAsStringAsync();

                var data = (JArray)JObject.Parse(content)["Items"];
                var result = new JArray();

                foreach(var item in data)
                {
                    if(item["Good"].Value<bool>() == true)
                    {
                        var dataPair = new JObject();
                        dataPair.Add("Timestamp",item["Timestamp"].Value<string>());
                        dataPair.Add("value", item["Value"].Value<double>());
                        result.Add(dataPair);
                    }
                }

                return Ok(new {result=result, message="success"});
           } 
            catch (Exception ex)
            {
                return StatusCode(500, new{result=ex, message="fail"});
            }
        }

        [HttpGet("GAS95dailyAvgSchUVal/{selDate}")]
        public async Task<IActionResult> GAS95dailyAvgSchUVal(DateTime selDate)
        {
            try
            {
                var credentrials = new NetworkCredential("group2","inc.382");  // using username & password for login 
                HttpClientHandler clientHandler = new HttpClientHandler { Credentials = credentrials };
                clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }; // acess to https
                HttpClient client = new HttpClient(clientHandler);

                DateTime today = DateTime.Now;
                TimeSpan value =  today.Subtract(selDate);
                
                string starttime = Convert.ToString(Convert.ToInt32(value.TotalDays));
                string endtime = Convert.ToString(Convert.ToInt32(value.TotalDays) - 1);

                string url = $@"https://202.44.12.146/piwebapi/streams/F1AbEP9i6VrUz70i0bz0vbTQKhwwBQnlDt_6hG4hVTudaRgyQwVFMF7crK0-2lxA8HObuFAMjAyLjQ0LjEyLjE0NlxHUk9VUDJfNFxGQUNUT1JZXEdBU09IT0w5NSBCQVl8R0FTT0hPTDk1IEJBWSBEQUlMWSBBVkVSQUdFIFNDSF9VVElMSVpBVElPTg/recorded?starttime=*-{starttime}d&endtime=*-{endtime}d";

                HttpResponseMessage response = await client.GetAsync(url);

                string content = await response.Content.ReadAsStringAsync();

                var data = (JArray)JObject.Parse(content)["Items"];
                var result = new JArray();

                foreach(var item in data)
                {
                    if(item["Good"].Value<bool>() == true)
                    {
                        var dataPair = new JObject();
                        dataPair.Add("Timestamp",item["Timestamp"].Value<string>());
                        dataPair.Add("value", item["Value"].Value<double>());
                        result.Add(dataPair);
                    }
                }

                return Ok(new {result=result, message="success"});
           } 
            catch (Exception ex)
            {
                return StatusCode(500, new{result=ex, message="fail"});
            }
        }

        [HttpGet("OWBdailyAvgSchUVal/{selDate}")]
        public async Task<IActionResult> OWBdailyAvgSchUVal(DateTime selDate)
        {
            try
            {
                var credentrials = new NetworkCredential("group2","inc.382");  // using username & password for login 
                HttpClientHandler clientHandler = new HttpClientHandler { Credentials = credentrials };
                clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }; // acess to https
                HttpClient client = new HttpClient(clientHandler);

                DateTime today = DateTime.Now;
                TimeSpan value =  today.Subtract(selDate);
                
                string starttime = Convert.ToString(Convert.ToInt32(value.TotalDays));
                string endtime = Convert.ToString(Convert.ToInt32(value.TotalDays) - 1);

                string url = $@"https://202.44.12.146/piwebapi/streams/F1AbEP9i6VrUz70i0bz0vbTQKhwpSJFnjt_6hG4hVTudaRgyQNGiQoZDhcES7qpjbxLW-fQMjAyLjQ0LjEyLjE0NlxHUk9VUDJfNFxGQUNUT1JZXE9VVEJPVU5EIFdFSUdIVEJSSURHRXxPVVRCT1VORCBXRUlHSEJSSURHRSBEQUlMWSBBVkVSQUdFIFNDSF9VVElMSVpBVElPTg/recorded?starttime=*-{starttime}d&endtime=*-{endtime}d";

                HttpResponseMessage response = await client.GetAsync(url);

                string content = await response.Content.ReadAsStringAsync();

                var data = (JArray)JObject.Parse(content)["Items"];
                var result = new JArray();

                foreach(var item in data)
                {
                    if(item["Good"].Value<bool>() == true)
                    {
                        var dataPair = new JObject();
                        dataPair.Add("Timestamp",item["Timestamp"].Value<string>());
                        dataPair.Add("value", item["Value"].Value<double>());
                        result.Add(dataPair);
                    }
                }

                return Ok(new {result=result, message="success"});
           } 
            catch (Exception ex)
            {
                return StatusCode(500, new{result=ex, message="fail"});
            }
        }


    }
}
