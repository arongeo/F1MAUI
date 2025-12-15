using MobilProj.Model;
using MobilProj.Services;
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

    public class DriverStandingsViewModel
    {
        public ObservableCollection<DriverStanding> DriverStandings { get; private set; } = new();

        public DriverStandingsViewModel() {}

        public async Task LoadStandings()
        {
            await RaceService.Load();

            DriverStandings.Clear();

            int pos = 1;

            foreach (var standing in RaceService.Races
                .SelectMany(x => x.Results)
                .GroupBy(x => x.Driver, (d, r) => new { Driver = d, Points = r })
                .Select(x => new { Driver = x.Driver, Points = x.Points.Sum(x => x.Points) })
                .OrderByDescending(x => x.Points)
            )
                DriverStandings.Add(new DriverStanding
                {
                    Driver = standing.Driver,
                    Points = standing.Points,
                    Position = pos++
                });
        }

        public async Task RefreshStandings()
        {
            await RaceService.Refresh();
            await LoadStandings();
        }
    }
}
