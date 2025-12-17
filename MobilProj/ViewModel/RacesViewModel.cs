using Microcharts;
using MobilProj.Model;
using MobilProj.Services;
using Newtonsoft.Json;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobilProj.ViewModel
{
    public class RaceView
    {
        public int Round { get; set; }
        public string Name { get; set; }
        public string Date { get; set; }
    }

    public partial class RacesViewModel
    {
        public ObservableCollection<RaceView> Races { get; set; } = new();
        private RaceService _raceService;
        public RacesViewModel(RaceService rs) {
            _raceService = rs;
        }

        public async Task LoadRaces()
        {
            await _raceService.Load();

            Races.Clear();

            foreach (var race in _raceService.Races)
                Races.Add(new RaceView
                {
                    Round = race.Round,
                    Name = race.RaceName,
                    Date = race.Date.ToString("d"),
                });
        }

        public async Task RefreshRaces()
        {
            await _raceService.Refresh();
            await LoadRaces();
        }

        public async Task Export()
        {
            var file = Path.Combine(FileSystem.AppDataDirectory, "races.json");
            await File.WriteAllTextAsync(file, JsonConvert.SerializeObject(Races));
        }

        public async Task ShareRaces()
        {
            if (this.Races.Count == 0) return;

            var outp = "";
            foreach (var race in Races)
                outp += $"{race.Round}. {race.Name} - {race.Date} points\n";

            await Share.Default.RequestAsync(new ShareTextRequest
            {
                Title = "Share the races",
                Text = outp.Trim()
            });
        }
    }
}
