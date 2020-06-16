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
    public class MonthlyDashboardController : ControllerBase
    {
        private readonly ILogger<MonthlyDashboardController> _logger;
        private readonly DatabaseContext _databaseContext;
        public MonthlyDashboardController(ILogger<MonthlyDashboardController> logger, DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
            _logger = logger;
        }
        [HttpGet("DIESELmoFillVolVal")]
        public async Task<IActionResult> DIESELmoFillVolVal()
        {
            try
            {
                var credentrials = new NetworkCredential("group2","inc.382");  // using username & password for login 
                HttpClientHandler clientHandler = new HttpClientHandler { Credentials = credentrials };
                clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }; // acess to https
                HttpClient client = new HttpClient(clientHandler);

                DateTime today = DateTime.Today;
                DateTime month = new DateTime(2018,3,31);
                TimeSpan value =  today.Subtract(month);
                
                string starttime = Convert.ToString(Convert.ToInt32(value.TotalDays));
                string endtime = Convert.ToString(Convert.ToInt32(value.TotalDays) - 1);

                string url = $@"https://202.44.12.146/piwebapi/streams/F1AbEP9i6VrUz70i0bz0vbTQKhwOw4HFTt_6hG4hVTudaRgyQSq6-ynwuZ027YrESOARmHgMjAyLjQ0LjEyLjE0NlxHUk9VUDJfNFxGQUNUT1JZXFNBTEUgT0ZGSUNFfE1PTlRITFkgQU1PVU5UIEZJTExJTkcgVk9MVU1FIChESUVTRUwp/recorded?starttime=*-{starttime}d&endtime=*-{endtime}d";

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

        [HttpGet("GAS95moFillVolVal")]
        public async Task<IActionResult> GAS95moFillVolVall()
        {
            try
            {
                var credentrials = new NetworkCredential("group2","inc.382");  // using username & password for login 
                HttpClientHandler clientHandler = new HttpClientHandler { Credentials = credentrials };
                clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }; // acess to https
                HttpClient client = new HttpClient(clientHandler);

                DateTime today = DateTime.Today;
                DateTime month = new DateTime(2018,3,31);
                TimeSpan value =  today.Subtract(month);
                
                string starttime = Convert.ToString(Convert.ToInt32(value.TotalDays));
                string endtime = Convert.ToString(Convert.ToInt32(value.TotalDays) - 1);

                string url = $@"https://202.44.12.146/piwebapi/streams/F1AbEP9i6VrUz70i0bz0vbTQKhwOw4HFTt_6hG4hVTudaRgyQp_Ugjr0CHUO8q8GrKmCpWgMjAyLjQ0LjEyLjE0NlxHUk9VUDJfNFxGQUNUT1JZXFNBTEUgT0ZGSUNFfE1PTlRITFkgQU1PVU5UIEZJTExJTkcgVk9MVU1FIChHQVNHSE9MOTUp/recorded?starttime=*-{starttime}d&endtime=*-{endtime}d";

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

        [HttpGet("DIESELmoAvgCycleTVal")]
        public async Task<IActionResult> DIESELmoAvgCycleTVal()
        {
            try
            {
                var credentrials = new NetworkCredential("group2","inc.382");  // using username & password for login 
                HttpClientHandler clientHandler = new HttpClientHandler { Credentials = credentrials };
                clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }; // acess to https
                HttpClient client = new HttpClient(clientHandler);

                DateTime today = DateTime.Today;
                DateTime month = new DateTime(2018,3,31);
                TimeSpan value =  today.Subtract(month);
                
                string starttime = Convert.ToString(Convert.ToInt32(value.TotalDays));
                string endtime = Convert.ToString(Convert.ToInt32(value.TotalDays) - 1);

                string url = $@"https://202.44.12.146/piwebapi/streams/F1AbEP9i6VrUz70i0bz0vbTQKhwny3Vcjt_6hG4hVTudaRgyQ78fculoS7EenUeUoxErJ6wMjAyLjQ0LjEyLjE0NlxHUk9VUDJfNFxGQUNUT1JZXERJRVNFTCBCQVl8RElFU0VMIEJBWSBNT05USExZIEFWRVJBR0UgQ1lDTEUgVElNRQ/recorded?starttime=*-{starttime}d&endtime=*-{endtime}d";

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

         [HttpGet("GAS95moAvgCycleTVal")]
        public async Task<IActionResult> GAS95moAvgCycleTVal()
        {
            try
            {
                var credentrials = new NetworkCredential("group2","inc.382");  // using username & password for login 
                HttpClientHandler clientHandler = new HttpClientHandler { Credentials = credentrials };
                clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }; // acess to https
                HttpClient client = new HttpClient(clientHandler);

                DateTime today = DateTime.Today;
                DateTime month = new DateTime(2018,3,31);
                TimeSpan value =  today.Subtract(month);
                
                string starttime = Convert.ToString(Convert.ToInt32(value.TotalDays));
                string endtime = Convert.ToString(Convert.ToInt32(value.TotalDays) - 1);

                string url = $@"https://202.44.12.146/piwebapi/streams/F1AbEP9i6VrUz70i0bz0vbTQKhwwBQnlDt_6hG4hVTudaRgyQi259JiczK0-PUMCwxPGVJgMjAyLjQ0LjEyLjE0NlxHUk9VUDJfNFxGQUNUT1JZXEdBU09IT0w5NSBCQVl8R0FTT0hPTDk1IEJBWSBNT05USExZIEFWRVJBR0UgQ1lDTEUgVElNRQ/recorded?starttime=*-{starttime}d&endtime=*-{endtime}d";

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

        [HttpGet("SOmoAvgWaitTVal")]
        public async Task<IActionResult> SOmoAvgWaitTVal()
        {
            try
            {
                var credentrials = new NetworkCredential("group2","inc.382");  // using username & password for login 
                HttpClientHandler clientHandler = new HttpClientHandler { Credentials = credentrials };
                clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }; // acess to https
                HttpClient client = new HttpClient(clientHandler);

                DateTime today = DateTime.Today;
                DateTime month = new DateTime(2018,3,31);
                TimeSpan value =  today.Subtract(month);
                
                string starttime = Convert.ToString(Convert.ToInt32(value.TotalDays));
                string endtime = Convert.ToString(Convert.ToInt32(value.TotalDays) - 1);

                string url = $@"https://202.44.12.146/piwebapi/streams/F1AbEP9i6VrUz70i0bz0vbTQKhwOw4HFTt_6hG4hVTudaRgyQb8aCx9Xoskm752qdOuaAywMjAyLjQ0LjEyLjE0NlxHUk9VUDJfNFxGQUNUT1JZXFNBTEUgT0ZGSUNFfFNBTEUgT0ZGSUNFIE1PTlRITFkgQVZFUkFHRSBXQUlUSU5HIFRJTUU/recorded?starttime=*-{starttime}d&endtime=*-{endtime}d";

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

        [HttpGet("IWBmoAvgWaitTVal")]
        public async Task<IActionResult> IWBmoAvgWaitTVal()
        {
            try
            {
                var credentrials = new NetworkCredential("group2","inc.382");  // using username & password for login 
                HttpClientHandler clientHandler = new HttpClientHandler { Credentials = credentrials };
                clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }; // acess to https
                HttpClient client = new HttpClient(clientHandler);

                DateTime today = DateTime.Today;
                DateTime month = new DateTime(2018,3,31);
                TimeSpan value =  today.Subtract(month);
                
                string starttime = Convert.ToString(Convert.ToInt32(value.TotalDays));
                string endtime = Convert.ToString(Convert.ToInt32(value.TotalDays) - 1);

                string url = $@"https://202.44.12.146/piwebapi/streams/F1AbEP9i6VrUz70i0bz0vbTQKhw3QL6Mjt_6hG4hVTudaRgyQq9Fn6jKFZEWE7n0mFLXfmgMjAyLjQ0LjEyLjE0NlxHUk9VUDJfNFxGQUNUT1JZXElOQk9VTkQgV0VJR0hUQlJJREdFfElOQk9VTkQgV0VJR0hCUklER0UgTU9OVEhMWSBBVkVSQUdFIFdBSVRJTkcgVElNRQ/recorded?starttime=*-{starttime}d&endtime=*-{endtime}d";

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

        [HttpGet("DIESELmoAvgWaitTVal")]
        public async Task<IActionResult> DIESELmoAvgWaitTVal()
        {
            try
            {
                var credentrials = new NetworkCredential("group2","inc.382");  // using username & password for login 
                HttpClientHandler clientHandler = new HttpClientHandler { Credentials = credentrials };
                clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }; // acess to https
                HttpClient client = new HttpClient(clientHandler);

                DateTime today = DateTime.Today;
                DateTime month = new DateTime(2018,3,31);
                TimeSpan value =  today.Subtract(month);
                
                string starttime = Convert.ToString(Convert.ToInt32(value.TotalDays));
                string endtime = Convert.ToString(Convert.ToInt32(value.TotalDays) - 1);

                string url = $@"https://202.44.12.146/piwebapi/streams/F1AbEP9i6VrUz70i0bz0vbTQKhwny3Vcjt_6hG4hVTudaRgyQ1MvOamkwMkaxPr0VdzCL6AMjAyLjQ0LjEyLjE0NlxHUk9VUDJfNFxGQUNUT1JZXERJRVNFTCBCQVl8RElFU0VMIEJBWSBNT05USExZIEFWRVJBR0UgV0FJVElORyBUSU1F/recorded?starttime=*-{starttime}d&endtime=*-{endtime}d";

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

        [HttpGet("GAS95moAvgWaitTVal")]
        public async Task<IActionResult> GAS95moAvgWaitTVal()
        {
            try
            {
                var credentrials = new NetworkCredential("group2","inc.382");  // using username & password for login 
                HttpClientHandler clientHandler = new HttpClientHandler { Credentials = credentrials };
                clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }; // acess to https
                HttpClient client = new HttpClient(clientHandler);

                DateTime today = DateTime.Today;
                DateTime month = new DateTime(2018,3,31);
                TimeSpan value =  today.Subtract(month);
                
                string starttime = Convert.ToString(Convert.ToInt32(value.TotalDays));
                string endtime = Convert.ToString(Convert.ToInt32(value.TotalDays) - 1);

                string url = $@"https://202.44.12.146/piwebapi/streams/F1AbEP9i6VrUz70i0bz0vbTQKhwwBQnlDt_6hG4hVTudaRgyQh7g9kA2zpkyT3RBHoL7bTwMjAyLjQ0LjEyLjE0NlxHUk9VUDJfNFxGQUNUT1JZXEdBU09IT0w5NSBCQVl8R0FTT0hPTDk1IEJBWSBNT05USExZIEFWRVJBR0UgV0FJVElORyBUSU1F/recorded?starttime=*-{starttime}d&endtime=*-{endtime}d";

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

        [HttpGet("OWBmoAvgWaitTVal")]
        public async Task<IActionResult> OWBmoAvgWaitTVal()
        {
            try
            {
                var credentrials = new NetworkCredential("group2","inc.382");  // using username & password for login 
                HttpClientHandler clientHandler = new HttpClientHandler { Credentials = credentrials };
                clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }; // acess to https
                HttpClient client = new HttpClient(clientHandler);

                DateTime today = DateTime.Today;
                DateTime month = new DateTime(2018,3,31);
                TimeSpan value =  today.Subtract(month);
                
                string starttime = Convert.ToString(Convert.ToInt32(value.TotalDays));
                string endtime = Convert.ToString(Convert.ToInt32(value.TotalDays) - 1);

                string url = $@"https://202.44.12.146/piwebapi/streams/F1AbEP9i6VrUz70i0bz0vbTQKhwpSJFnjt_6hG4hVTudaRgyQtOhLixf8X0OM5y52k23ZHQMjAyLjQ0LjEyLjE0NlxHUk9VUDJfNFxGQUNUT1JZXE9VVEJPVU5EIFdFSUdIVEJSSURHRXxPVVRCT1VORCBXRUlHSEJSSURHRSBNT05USExZIEFWRVJBR0UgV0FJVElORyBUSU1F/recorded?starttime=*-{starttime}d&endtime=*-{endtime}d";

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

        [HttpGet("moTruckInVal")]
        public async Task<IActionResult> moTruckInVal()
        {
            try
            {
                var credentrials = new NetworkCredential("group2","inc.382");  // using username & password for login 
                HttpClientHandler clientHandler = new HttpClientHandler { Credentials = credentrials };
                clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }; // acess to https
                HttpClient client = new HttpClient(clientHandler);

                DateTime today = DateTime.Today;
                DateTime month = new DateTime(2018,3,31);
                TimeSpan value =  today.Subtract(month);
                
                string starttime = Convert.ToString(Convert.ToInt32(value.TotalDays));
                string endtime = Convert.ToString(Convert.ToInt32(value.TotalDays) - 1);

                string url = $@"https://202.44.12.146/piwebapi/streams/F1AbEP9i6VrUz70i0bz0vbTQKhwOw4HFTt_6hG4hVTudaRgyQcM7SdYlB306OjK2wizzSaQMjAyLjQ0LjEyLjE0NlxHUk9VUDJfNFxGQUNUT1JZXFNBTEUgT0ZGSUNFfE1PTlRITFkgTlVNQkVSIE9GIFRSVUNLUyBJTg/recorded?starttime=*-{starttime}d&endtime=*-{endtime}d";

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

        [HttpGet("moTruckOutVal")]
        public async Task<IActionResult> moTruckOutVal()
        {
            try
            {
                var credentrials = new NetworkCredential("group2","inc.382");  // using username & password for login 
                HttpClientHandler clientHandler = new HttpClientHandler { Credentials = credentrials };
                clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }; // acess to https
                HttpClient client = new HttpClient(clientHandler);

                DateTime today = DateTime.Today;
                DateTime month = new DateTime(2018,3,31);
                TimeSpan value =  today.Subtract(month);
                
                string starttime = Convert.ToString(Convert.ToInt32(value.TotalDays));
                string endtime = Convert.ToString(Convert.ToInt32(value.TotalDays) - 1);

                string url = $@"https://202.44.12.146/piwebapi/streams/F1AbEP9i6VrUz70i0bz0vbTQKhw0f0eqzt_6hG4hVTudaRgyQt7urOVHmr02o78fIOzV5pQMjAyLjQ0LjEyLjE0NlxHUk9VUDJfNFxGQUNUT1JZXEVYSVQgR0FURXxNT05USExZIE5VTUJFUiBPRiBUUlVDS1MgT1VU/recorded?starttime=*-{starttime}d&endtime=*-{endtime}d";

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

        [HttpGet("SOmoAvgQVal")]
        public async Task<IActionResult> SOmoAvgQVal()
        {
            try
            {
                var credentrials = new NetworkCredential("group2","inc.382");  // using username & password for login 
                HttpClientHandler clientHandler = new HttpClientHandler { Credentials = credentrials };
                clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }; // acess to https
                HttpClient client = new HttpClient(clientHandler);

                DateTime today = DateTime.Today;
                DateTime month = new DateTime(2018,3,31);
                TimeSpan value =  today.Subtract(month);
                
                string starttime = Convert.ToString(Convert.ToInt32(value.TotalDays));
                string endtime = Convert.ToString(Convert.ToInt32(value.TotalDays) - 1);

                string url = $@"https://202.44.12.146/piwebapi/streams/F1AbEP9i6VrUz70i0bz0vbTQKhwOw4HFTt_6hG4hVTudaRgyQT-o9C6HmqESzrDhU-sn4jAMjAyLjQ0LjEyLjE0NlxHUk9VUDJfNFxGQUNUT1JZXFNBTEUgT0ZGSUNFfFNBTEUgT0ZGSUNFIE1PTlRITFkgQVZFUkFHRSBOVU1CRVIgT0YgUVVFVUU/recorded?starttime=*-{starttime}d&endtime=*-{endtime}d";

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

        [HttpGet("IWBmoAvgQVal")]
        public async Task<IActionResult> IWBmoAvgQVal()
        {
            try
            {
                var credentrials = new NetworkCredential("group2","inc.382");  // using username & password for login 
                HttpClientHandler clientHandler = new HttpClientHandler { Credentials = credentrials };
                clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }; // acess to https
                HttpClient client = new HttpClient(clientHandler);

                DateTime today = DateTime.Today;
                DateTime month = new DateTime(2018,3,31);
                TimeSpan value =  today.Subtract(month);
                
                string starttime = Convert.ToString(Convert.ToInt32(value.TotalDays));
                string endtime = Convert.ToString(Convert.ToInt32(value.TotalDays) - 1);

                string url = $@"https://202.44.12.146/piwebapi/streams/F1AbEP9i6VrUz70i0bz0vbTQKhw3QL6Mjt_6hG4hVTudaRgyQ153hLBU4LUm7jKvEcnUyYwMjAyLjQ0LjEyLjE0NlxHUk9VUDJfNFxGQUNUT1JZXElOQk9VTkQgV0VJR0hUQlJJREdFfElOQk9VTkQgV0VJR0hCUklER0UgTU9OVEhMWSBBVkVSQUdFIE5VTUJFUiBPRiBRVUVVRQ/recorded?starttime=*-{starttime}d&endtime=*-{endtime}d";

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

        [HttpGet("DIESELmoAvgQVal")]
        public async Task<IActionResult> DIESELmoAvgQVal()
        {
            try
            {
                var credentrials = new NetworkCredential("group2","inc.382");  // using username & password for login 
                HttpClientHandler clientHandler = new HttpClientHandler { Credentials = credentrials };
                clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }; // acess to https
                HttpClient client = new HttpClient(clientHandler);

                DateTime today = DateTime.Today;
                DateTime month = new DateTime(2018,3,31);
                TimeSpan value =  today.Subtract(month);
                
                string starttime = Convert.ToString(Convert.ToInt32(value.TotalDays));
                string endtime = Convert.ToString(Convert.ToInt32(value.TotalDays) - 1);

                string url = $@"https://202.44.12.146/piwebapi/streams/F1AbEP9i6VrUz70i0bz0vbTQKhwny3Vcjt_6hG4hVTudaRgyQ8Ad9yfsPVECdTrMR_fh2igMjAyLjQ0LjEyLjE0NlxHUk9VUDJfNFxGQUNUT1JZXERJRVNFTCBCQVl8RElFU0VMIEJBWSBNT05USExZIEFWRVJBR0UgTlVNQkVSIE9GIFFVRVVF/recorded?starttime=*-{starttime}d&endtime=*-{endtime}d";

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

        [HttpGet("GAS95moAvgQVal")]
        public async Task<IActionResult> GAS95moAvgQVal()
        {
            try
            {
                var credentrials = new NetworkCredential("group2","inc.382");  // using username & password for login 
                HttpClientHandler clientHandler = new HttpClientHandler { Credentials = credentrials };
                clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }; // acess to https
                HttpClient client = new HttpClient(clientHandler);

                DateTime today = DateTime.Today;
                DateTime month = new DateTime(2018,3,31);
                TimeSpan value =  today.Subtract(month);
                
                string starttime = Convert.ToString(Convert.ToInt32(value.TotalDays));
                string endtime = Convert.ToString(Convert.ToInt32(value.TotalDays) - 1);

                string url = $@"https://202.44.12.146/piwebapi/streams/F1AbEP9i6VrUz70i0bz0vbTQKhwwBQnlDt_6hG4hVTudaRgyQPmQL7nKeDket1wvLG4iZfQMjAyLjQ0LjEyLjE0NlxHUk9VUDJfNFxGQUNUT1JZXEdBU09IT0w5NSBCQVl8R0FTT0hPTDk1IEJBWSBNT05USExZIEFWRVJBR0UgTlVNQkVSIE9GIFFVRVVF/recorded?starttime=*-{starttime}d&endtime=*-{endtime}d";

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

        [HttpGet("OWBmoAvgQVal")]
        public async Task<IActionResult> OWBmoAvgQVal()
        {
            try
            {
                var credentrials = new NetworkCredential("group2","inc.382");  // using username & password for login 
                HttpClientHandler clientHandler = new HttpClientHandler { Credentials = credentrials };
                clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }; // acess to https
                HttpClient client = new HttpClient(clientHandler);

                DateTime today = DateTime.Today;
                DateTime month = new DateTime(2018,3,31);
                TimeSpan value =  today.Subtract(month);
                
                string starttime = Convert.ToString(Convert.ToInt32(value.TotalDays));
                string endtime = Convert.ToString(Convert.ToInt32(value.TotalDays) - 1);

                string url = $@"https://202.44.12.146/piwebapi/streams/F1AbEP9i6VrUz70i0bz0vbTQKhwpSJFnjt_6hG4hVTudaRgyQSAoahRteUE-GUXyrjQQm-AMjAyLjQ0LjEyLjE0NlxHUk9VUDJfNFxGQUNUT1JZXE9VVEJPVU5EIFdFSUdIVEJSSURHRXxPVVRCT1VORCBXRUlHSEJSSURHRSBNT05USExZIEFWRVJBR0UgTlVNQkVSIE9GIFFVRVVF/recorded?starttime=*-{starttime}d&endtime=*-{endtime}d";

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

        [HttpGet("DIESELmoAvgWIPVal")]
        public async Task<IActionResult> DIESELmoAvgWIPVal()
        {
            try
            {
                var credentrials = new NetworkCredential("group2","inc.382");  // using username & password for login 
                HttpClientHandler clientHandler = new HttpClientHandler { Credentials = credentrials };
                clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }; // acess to https
                HttpClient client = new HttpClient(clientHandler);

                DateTime today = DateTime.Today;
                DateTime month = new DateTime(2018,3,31);
                TimeSpan value =  today.Subtract(month);
                
                string starttime = Convert.ToString(Convert.ToInt32(value.TotalDays));
                string endtime = Convert.ToString(Convert.ToInt32(value.TotalDays) - 1);

                string url = $@"https://202.44.12.146/piwebapi/streams/F1AbEP9i6VrUz70i0bz0vbTQKhwny3Vcjt_6hG4hVTudaRgyQrN-naxcEfUyRsddgiG_2lQMjAyLjQ0LjEyLjE0NlxHUk9VUDJfNFxGQUNUT1JZXERJRVNFTCBCQVl8RElFU0VMIEJBWSBNT05USExZIEFWRVJBR0UgV0lQ/recorded?starttime=*-{starttime}d&endtime=*-{endtime}d";

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

        [HttpGet("GAS95moAvgWIPVal")]
        public async Task<IActionResult> GAS95moAvgWIPVal()
        {
            try
            {
                var credentrials = new NetworkCredential("group2","inc.382");  // using username & password for login 
                HttpClientHandler clientHandler = new HttpClientHandler { Credentials = credentrials };
                clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }; // acess to https
                HttpClient client = new HttpClient(clientHandler);

                DateTime today = DateTime.Today;
                DateTime month = new DateTime(2018,3,31);
                TimeSpan value =  today.Subtract(month);
                
                string starttime = Convert.ToString(Convert.ToInt32(value.TotalDays));
                string endtime = Convert.ToString(Convert.ToInt32(value.TotalDays) - 1);

                string url = $@"https://202.44.12.146/piwebapi/streams/F1AbEP9i6VrUz70i0bz0vbTQKhwwBQnlDt_6hG4hVTudaRgyQ01s0YgHk-0epu4FdGCqyHwMjAyLjQ0LjEyLjE0NlxHUk9VUDJfNFxGQUNUT1JZXEdBU09IT0w5NSBCQVl8R0FTT0hPTDk1IEJBWSBNT05USExZIEFWRVJBR0UgV0lQ/recorded?starttime=*-{starttime}d&endtime=*-{endtime}d";

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

        [HttpGet("SOmoAvgSchUVal")]
        public async Task<IActionResult> SOmoAvgSchUVal()
        {
            try
            {
                var credentrials = new NetworkCredential("group2","inc.382");  // using username & password for login 
                HttpClientHandler clientHandler = new HttpClientHandler { Credentials = credentrials };
                clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }; // acess to https
                HttpClient client = new HttpClient(clientHandler);

                DateTime today = DateTime.Today;
                DateTime month = new DateTime(2018,3,31);
                TimeSpan value =  today.Subtract(month);
                
                string starttime = Convert.ToString(Convert.ToInt32(value.TotalDays));
                string endtime = Convert.ToString(Convert.ToInt32(value.TotalDays) - 1);

                string url = $@"https://202.44.12.146/piwebapi/streams/F1AbEP9i6VrUz70i0bz0vbTQKhwOw4HFTt_6hG4hVTudaRgyQc0OiOk1YG0ee1ZP_tVrJIgMjAyLjQ0LjEyLjE0NlxHUk9VUDJfNFxGQUNUT1JZXFNBTEUgT0ZGSUNFfFNBTEUgT0ZGSUNFIE1PTlRITFkgQVZFUkFHRSBTQ0hfVVRJTElaQVRJT04/recorded?starttime=*-{starttime}d&endtime=*-{endtime}d";

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

        [HttpGet("IWBmoAvgSchUVal")]
        public async Task<IActionResult> IWBmoAvgSchUVal()
        {
            try
            {
                var credentrials = new NetworkCredential("group2","inc.382");  // using username & password for login 
                HttpClientHandler clientHandler = new HttpClientHandler { Credentials = credentrials };
                clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }; // acess to https
                HttpClient client = new HttpClient(clientHandler);

                DateTime today = DateTime.Today;
                DateTime month = new DateTime(2018,3,31);
                TimeSpan value =  today.Subtract(month);
                
                string starttime = Convert.ToString(Convert.ToInt32(value.TotalDays));
                string endtime = Convert.ToString(Convert.ToInt32(value.TotalDays) - 1);

                string url = $@"https://202.44.12.146/piwebapi/streams/F1AbEP9i6VrUz70i0bz0vbTQKhw3QL6Mjt_6hG4hVTudaRgyQnS1cS_MHy0uxgjBrHggIdwMjAyLjQ0LjEyLjE0NlxHUk9VUDJfNFxGQUNUT1JZXElOQk9VTkQgV0VJR0hUQlJJREdFfElOQk9VTkQgV0VJR0hCUklER0UgTU9OVEhMWSBBVkVSQUdFIFNDSF9VVElMSVpBVElPTg/recorded?starttime=*-{starttime}d&endtime=*-{endtime}d";

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

        [HttpGet("DIESELmoAvgSchUVal")]
        public async Task<IActionResult> DIESELmoAvgSchUVal()
        {
            try
            {
                var credentrials = new NetworkCredential("group2","inc.382");  // using username & password for login 
                HttpClientHandler clientHandler = new HttpClientHandler { Credentials = credentrials };
                clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }; // acess to https
                HttpClient client = new HttpClient(clientHandler);

                DateTime today = DateTime.Today;
                DateTime month = new DateTime(2018,3,31);
                TimeSpan value =  today.Subtract(month);
                
                string starttime = Convert.ToString(Convert.ToInt32(value.TotalDays));
                string endtime = Convert.ToString(Convert.ToInt32(value.TotalDays) - 1);

                string url = $@"https://202.44.12.146/piwebapi/streams/F1AbEP9i6VrUz70i0bz0vbTQKhwny3Vcjt_6hG4hVTudaRgyQKRLFA-aRWUOhjvnrPmkdtAMjAyLjQ0LjEyLjE0NlxHUk9VUDJfNFxGQUNUT1JZXERJRVNFTCBCQVl8RElFU0VMIEJBWSBNT05USExZIEFWRVJBR0UgU0NIX1VUSUxJWkFUSU9O/recorded?starttime=*-{starttime}d&endtime=*-{endtime}d";

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

        [HttpGet("GAS95moAvgSchUVal")]
        public async Task<IActionResult> GAS95moAvgSchUVal()
        {
            try
            {
                var credentrials = new NetworkCredential("group2","inc.382");  // using username & password for login 
                HttpClientHandler clientHandler = new HttpClientHandler { Credentials = credentrials };
                clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }; // acess to https
                HttpClient client = new HttpClient(clientHandler);

                DateTime today = DateTime.Today;
                DateTime month = new DateTime(2018,3,31);
                TimeSpan value =  today.Subtract(month);
                
                string starttime = Convert.ToString(Convert.ToInt32(value.TotalDays));
                string endtime = Convert.ToString(Convert.ToInt32(value.TotalDays) - 1);

                string url = $@"https://202.44.12.146/piwebapi/streams/F1AbEP9i6VrUz70i0bz0vbTQKhwwBQnlDt_6hG4hVTudaRgyQTJ0BtnvOVU-tkNUf7ZAvCgMjAyLjQ0LjEyLjE0NlxHUk9VUDJfNFxGQUNUT1JZXEdBU09IT0w5NSBCQVl8R0FTT0hPTDk1IEJBWSBNT05USExZIEFWRVJBR0UgU0NIX1VUSUxJWkFUSU9O/recorded?starttime=*-{starttime}d&endtime=*-{endtime}d";

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

        [HttpGet("OWBmoAvgSchUVal")]
        public async Task<IActionResult> OWBmoAvgSchUVal()
        {
            try
            {
                var credentrials = new NetworkCredential("group2","inc.382");  // using username & password for login 
                HttpClientHandler clientHandler = new HttpClientHandler { Credentials = credentrials };
                clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }; // acess to https
                HttpClient client = new HttpClient(clientHandler);

                DateTime today = DateTime.Today;
                DateTime month = new DateTime(2018,3,31);
                TimeSpan value =  today.Subtract(month);
                string starttime = Convert.ToString(Convert.ToInt32(value.TotalDays));
                string endtime = Convert.ToString(Convert.ToInt32(value.TotalDays) - 1);

                string url = $@"https://202.44.12.146/piwebapi/streams/F1AbEP9i6VrUz70i0bz0vbTQKhwpSJFnjt_6hG4hVTudaRgyQ_hnEZFqSBkWFjAhEszbEFgMjAyLjQ0LjEyLjE0NlxHUk9VUDJfNFxGQUNUT1JZXE9VVEJPVU5EIFdFSUdIVEJSSURHRXxPVVRCT1VORCBXRUlHSEJSSURHRSBNT05USExZIEFWRVJBR0UgU0NIX1VUSUxJWkFUSU9O/recorded?starttime=*-{starttime}d&endtime=*-{endtime}d";

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