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
        public Driver Driver { get; set; }
        public int Points { get; set; }
    }

    public class DriverStandingsViewModel
    {
        public ObservableCollection<DriverStanding> DriverStandings { get; private set; } = new();

        public DriverStandingsViewModel() {}

        public async Task LoadStandings()
        {
            await RaceService.LoadRaces();

            DriverStandings = new ObservableCollection<DriverStanding>(
                RaceService.Races
                    .SelectMany(x => x.Results)
                    .GroupBy(x => x.Driver, (d, r) => new DriverStanding { Driver = d, Points = r.Sum(x => x.Points) })
                    .ToList()
            );
        }
    }
}
