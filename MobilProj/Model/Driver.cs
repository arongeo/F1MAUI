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

        public override bool Equals(object? obj)
        {
            var other = obj as Driver;
            if (other == null) return false;
            return this.DriverId == other.DriverId && this.Name == other.Name;
        }

        public override int GetHashCode()
        {
            return this.DriverId.GetHashCode() ^ this.Name.GetHashCode();
        }
    }
}
