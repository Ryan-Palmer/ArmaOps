using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;

namespace ArmaOps.Domain.Coordinates
{
    public class Polar
    {
        public Cartesian Origin { get; }
        public double Azimuth { get; }
        public double Elevation { get; }
        public double Distance { get; }
        public double HDist => Distance * Math.Cos(Elevation);
        public double VDist => Distance * Math.Sin(Elevation);

        public Polar (
            Cartesian origin,
            double azimuth,
            double elevation,
            double distance)
        {
            Origin = origin;
            Azimuth = azimuth;
            Elevation = elevation;
            Distance = distance;
        }

        public Cartesian ToCartesian()
        {
            var dx = Distance * Math.Sin(Azimuth) * Math.Cos(Elevation);
            var dy = Distance * Math.Sin(Elevation);
            var dz = Distance * Math.Cos(Azimuth) * Math.Cos(Elevation);
            return new Cartesian(dx, dy, dz).Add(Origin);
        }

        public override bool Equals(object obj)
        {
            if (obj != null)
            {
                var other = obj as Polar;
                if (other != null)
                {
                    return
                        other.Origin.Equals(this.Origin)
                        && other.Azimuth == this.Azimuth
                        && other.Elevation == this.Elevation
                        && other.Distance == this.Distance;
                }
            }
            return false;
        }

        public override int GetHashCode()
        {
            return (Origin.GetHashCode(), Azimuth, Elevation, Distance).GetHashCode();
        }

        public override string ToString()
        {
            return $"Origin:{Origin} Azimuth:{Azimuth} Elevation:{Elevation} Distance:{Distance}";
        }
    }
}
