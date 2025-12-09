using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobilProj.Model
{
    public class Driver
    {
        public string DriverId { get; set; }
        public int PermanentNumber { get; set; }
        public string Code { get; set; }
        public string Url { get; set; }
        public string GivenName { get; set; }
        public string FamilyName { get; set; }
      
        public string Name { 
            get
            {
                return $"{GivenName} {FamilyName}";
            } 
        }

        public DateTime DateOfBirth { get; set; }
        public string Nationality { get; set; }

        public Driver() { }
    }
}
