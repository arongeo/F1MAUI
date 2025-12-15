using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobilProj.Model
{
    public class Result
    {
        public int Position { get; set; }
        public int Points { get; set; }
        public int Grid { get; set; }
        public string Status { get; set; }
        public Driver Driver { get; set; }
        public Constructor Constructor { get; set; }
    }
}
