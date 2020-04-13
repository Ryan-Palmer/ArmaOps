using System;
using System.Collections.Generic;
using System.Text;

namespace ArmaOps.Domain.Coordinates
{
    public class Polar
    {
        public Cartesian FO { get; private set; }
        public double Azimuth { get; private set; }
        public double Elevation { get; private set; }
        public double Distance { get; private set; }

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
    }
}
