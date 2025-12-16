using CommunityToolkit.Mvvm.ComponentModel;
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
    public class DriverStanding
    {
        public int Position { get; set; }
        public Driver Driver { get; set; }
        public int Points { get; set; }
    }

    public partial class DriverStandingsViewModel : ObservableObject
    {
        public ObservableCollection<DriverStanding> DriverStandings { get; private set; } = new();
        private RaceService _raceService;

        [ObservableProperty]
        private Chart _chart;
        
        public DriverStandingsViewModel(RaceService rs) {
            _raceService = rs;
            Chart = new BarChart();
        }

        public async Task LoadStandings()
        {
            await _raceService.Load();

            DriverStandings.Clear();

            List<ChartEntry> chartEntries = new();

            int pos = 1;

            foreach (var standing in _raceService.Races
                .SelectMany(x => x.Results)
                .GroupBy(x => x.Driver, (d, r) => new { Driver = d, Points = r })
                .Select(x => new { x.Driver, Points = x.Points.Sum(x => x.Points) })
                .OrderByDescending(x => x.Points)
            )
            {
                if (chartEntries.Count < 10)
                {
                    chartEntries.Add(new ChartEntry(standing.Points)
                    {
                        Label = standing.Driver.Name,
                        ValueLabel = $"{standing.Points}",
                        Color = SKColor.Parse("#326ba8")
                    });
                }

                DriverStandings.Add(new DriverStanding
                {
                    Driver = standing.Driver,
                    Points = standing.Points,
                    Position = pos++
                });
            }

            Chart = new BarChart
            {
                Entries = chartEntries
            };
        }

        public async Task RefreshStandings()
        {
            await _raceService.Refresh();
            await LoadStandings();
        }

        public async Task Export()
        {
            var file = Path.Combine(FileSystem.AppDataDirectory, "drivers.json");
            await File.WriteAllTextAsync(file, JsonConvert.SerializeObject(DriverStandings));
        }

        public async Task ShareStandings()
        {
            if (this.DriverStandings.Count == 0) return;

            var outp = "";
            foreach (var standing in DriverStandings)
                outp += $"{standing.Position}. {standing.Driver.Name} - {standing.Points} points\n";

            await Share.Default.RequestAsync(new ShareTextRequest
            {
                Title = "Share the drivers' championship's standings",
                Text = outp.Trim()
            });
        }
    }
}
