using MobilProj.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobilProj.Services
{
    public class RaceService
    {
        private readonly HttpClient _httpClient;
        private readonly string URL = $"https://api.jolpi.ca/ergast/f1/{DateTime.Now.Year}/results/";

        public List<Race> Races { get; private set; } = new List<Race>();

        public RaceService(HttpClient hc)
        {
            _httpClient = hc;
        } 

        public async Task Refresh()
        {
            Races.Clear();
            await Load();
        }

        public async Task Load()
        {
            if (Races.Count != 0) return;

            int limit = 100, offset = 0, total = 0;

            do
            {
                var response = await _httpClient.GetStringAsync(URL + $"?limit={limit}&offset={offset}");
                JObject o = JObject.Parse(response);
                try
                {
                    limit = int.Parse(o["MRData"]["limit"].ToString());
                    offset += limit;
                    total = int.Parse(o["MRData"]["total"].ToString());

                    List<Race>? races = JsonConvert.DeserializeObject<List<Race>>(o["MRData"]["RaceTable"]["Races"].ToString());

                    if (races == null) continue;

                    foreach (var race in races)
                    {
                        var existingRace = Races.FirstOrDefault(x => x.Round == race.Round);

                        if (existingRace == null)
                        {
                            Races.Add(race);
                            continue;
                        }

                        foreach (var result in race.Results)
                            existingRace.Results.Add(result);
                    }
                } catch (Exception)
                {
                    break;
                }
            } while (offset < total);
        }
    }
}
