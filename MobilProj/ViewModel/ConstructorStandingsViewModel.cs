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
    public class ConstructorStanding
    {
        public int Position { get; set; }
        public Constructor Constructor { get; set; }
        public int Points { get; set; }
    }

    public class ConstructorStandingsViewModel
    {
        private RaceService _raceService;
        public ObservableCollection<ConstructorStanding> ConstructorStandings { get; set; } = new();

        public ConstructorStandingsViewModel(RaceService rs) 
        {
            _raceService = rs;
        }

        public async Task LoadStandings()
        {
            await _raceService.Load();

            ConstructorStandings.Clear();

            int pos = 1;

            foreach (var standing in _raceService.Races
                .SelectMany(x => x.Results)
                .GroupBy(x => x.Constructor, (d, r) => new { Constructor = d, Points = r })
                .Select(x => new { x.Constructor, Points = x.Points.Sum(x => x.Points) })
                .OrderByDescending(x => x.Points)
            )
                ConstructorStandings.Add(new ConstructorStanding
                {
                    Position = pos++,
                    Constructor = standing.Constructor,
                    Points = standing.Points
                });
        }

        public async Task RefreshStandings()
        {
            await _raceService.Refresh();
            await LoadStandings();
        }
    }
}
