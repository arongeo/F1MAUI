using MobilProj.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace MobilProj.Services
{
    public class DriverService
    {
        private readonly HttpClient _httpClient = new HttpClient();
        private string URL = $"https://api.jolpi.ca/ergast/f1/{DateTime.Now.Year}/drivers/";

        public async Task<List<Driver>> LoadDrivers()
        {
            
            var response = _httpClient.GetStringAsync(URL).Result;
            JObject o = JObject.Parse(response);
            try
            {
                var ret = JsonConvert.DeserializeObject<List<Driver>>(o["MRData"]["DriverTable"]["Drivers"].ToString());
                return ret == null ? new List<Driver>() : ret;
            } catch (Exception)
            {
                return new List<Driver>();
            }
        }
    }
}
