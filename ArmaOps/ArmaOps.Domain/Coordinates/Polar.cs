using System;
using System.Collections.Generic;
using System.Text;

namespace ArmaOps.Domain.Coordinates
{
    public class Polar
    {
        public Cartesian FO { get; }
        public double Azimuth { get; }
        public double Elevation { get; }
        public double Distance { get; }

        public Polar (
            Cartesian fo,
            double azimuth,
            double elevation,
            double distance)
        {
            FO = fo;
            Azimuth = azimuth;
            Elevation = elevation;
            Distance = distance;
        }

        public override bool Equals(object obj)
        {
            if (obj != null)
            {
                var other = obj as Polar;
                if (other != null)
                {
                    return
                        other.FO.Equals(this.FO)
                        && other.Azimuth == this.Azimuth
                        && other.Elevation == this.Elevation
                        && other.Distance == this.Distance;
                }
            }
            return false;
        }

        public override int GetHashCode()
        {
            (FO.GetHashCode(), Azimuth, Elevation, Distance).GetHashCode();
            //unchecked
            //{
            //    int hash = 17; // multiply 2 primes by the hash code of internal values
            //    hash = hash * 23 + FO.GetHashCode();
            //    hash = hash * 23 + Azimuth.GetHashCode();
            //    hash = hash * 23 + Elevation.GetHashCode();
            //    hash = hash * 23 + Distance.GetHashCode();
            //    return hash;
            //}
        }
    }
}
