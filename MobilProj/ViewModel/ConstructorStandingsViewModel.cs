using CommunityToolkit.Mvvm.ComponentModel;
using Microcharts;
using Microsoft.Maui.ApplicationModel.DataTransfer;
using MobilProj.Model;
using MobilProj.Services;
using Newtonsoft.Json;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MobilProj.ViewModel
{
    public class ConstructorStanding
    {
        public int Position { get; set; }
        public Constructor Constructor { get; set; }
        public int Points { get; set; }
    }

    public partial class ConstructorStandingsViewModel : ObservableObject
    {
        private RaceService _raceService;
        public ObservableCollection<ConstructorStanding> ConstructorStandings { get; set; } = new();

        [ObservableProperty]
        private Chart _chart;

        public ConstructorStandingsViewModel(RaceService rs) 
        {
            _raceService = rs;
            Chart = new BarChart();
        }

        public async Task LoadStandings()
        {
            await _raceService.Load();

            ConstructorStandings.Clear();

            List<ChartEntry> chartEntries = new();

            int pos = 1;

            foreach (var standing in _raceService.Races
                .SelectMany(x => x.Results)
                .GroupBy(x => x.Constructor, (d, r) => new { Constructor = d, Points = r })
                .Select(x => new { x.Constructor, Points = x.Points.Sum(x => x.Points) })
                .OrderByDescending(x => x.Points)
            ) {
                chartEntries.Add(new ChartEntry(standing.Points)
                {
                    Label = standing.Constructor.Name,
                    ValueLabel = $"{standing.Points}",
                    Color = SKColor.Parse("#326ba8")
                });

                ConstructorStandings.Add(new ConstructorStanding
                {
                    Position = pos++,
                    Constructor = standing.Constructor,
                    Points = standing.Points
                });
            }

            Chart = new BarChart()
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
            var file = Path.Combine(FileSystem.AppDataDirectory, "constructors.json");
            await File.WriteAllTextAsync(file, JsonConvert.SerializeObject(ConstructorStandings));
        }

        public async Task ShareStandings()
        {
            if (this.ConstructorStandings.Count == 0) return;

            var outp = "";
            foreach (var standing in ConstructorStandings)
                outp += $"{standing.Position}. {standing.Constructor.Name} - {standing.Points} points\n";

            await Share.Default.RequestAsync(new ShareTextRequest
            {
                Title = "Share the constructors' championship's standings",
                Text = outp.Trim()
            });
        }
    }
}
