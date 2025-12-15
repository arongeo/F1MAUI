using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobilProj.Model
{
    public class Constructor
    {
        public string ConstructorId { get; set; }
        public string Name { get; set; }
        public string Nationality { get; set; }

        public override bool Equals(object? obj)
        {
            var other = obj as Constructor;
            if (other == null) return false;
            return this.ConstructorId == other.ConstructorId;
        }

        public override int GetHashCode()
        {
            return this.ConstructorId.GetHashCode();
        }
    }
}
