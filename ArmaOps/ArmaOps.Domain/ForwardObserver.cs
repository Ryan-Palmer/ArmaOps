using ArmaOps.Domain.Coordinates;
using System;
using System.Collections.Generic;
using System.Text;

namespace ArmaOps.Domain
{
    public class ForwardObserver
    {
        public Cartesian Location { get; }
        public string Name { get; }

        public ForwardObserver (Cartesian location, string name)
        {
            Location = location;
            Name = name;
        }

        public override bool Equals(object obj)
        {
            if (obj != null)
            {
                var other = obj as ForwardObserver;
                if (other != null)
                {
                    return
                        other.Location.Equals(this.Location);
                }
            }
            return false;
        }

        public override int GetHashCode()
        {
            return Location.GetHashCode();
        }
    }
}
