using System;
using System.Collections.Generic;
using System.Text;

namespace ArmaOps.Domain.Coordinates
{
    public class Polar
    {
        public ForwardObserver ForwardObserver { get; }
        public double Azimuth { get; }
        public double Elevation { get; }
        public double Distance { get; }

        public Polar (
            ForwardObserver fo,
            double azimuth,
            double elevation,
            double distance)
        {
            ForwardObserver = fo;
            Azimuth = azimuth;
            Elevation = elevation;
            Distance = distance;
        }

        public Cartesian ToCartesian()
        {
            var dx = Distance * Math.Sin(Azimuth);
            var dy = Distance * Math.Cos(Azimuth);
            var dz = 0.0;
            return new Cartesian(dx, dy, dz).Add(ForwardObserver.Location);
        }

        public override bool Equals(object obj)
        {
            if (obj != null)
            {
                var other = obj as Polar;
                if (other != null)
                {
                    return
                        other.ForwardObserver.Equals(this.ForwardObserver)
                        && other.Azimuth == this.Azimuth
                        && other.Elevation == this.Elevation
                        && other.Distance == this.Distance;
                }
            }
            return false;
        }

        public override int GetHashCode()
        {
            return (ForwardObserver.GetHashCode(), Azimuth, Elevation, Distance).GetHashCode();
        }
    }
}
