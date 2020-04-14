﻿using System;
using System.Collections.Generic;
using System.Text;

namespace ArmaOps.Domain.Coordinates
{
    public class Polar
    {
        public Cartesian Origin { get; }
        public double Azimuth { get; }
        public double Elevation { get; }
        public double Distance { get; }

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
            var dx = Distance * Math.Sin(Azimuth);
            var dy = Distance * Math.Cos(Azimuth);
            var dz = 0.0;
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
    }
}
