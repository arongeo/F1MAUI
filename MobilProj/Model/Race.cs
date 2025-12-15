using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobilProj.Model
{
    public class Race
    {
        public int Round { get; set; }

        public string RaceName { get; set; }

        public DateTime Date { get; set; }

        public ObservableCollection<Result> Results { get; set; }
    }
}
